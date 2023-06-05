using Microsoft.EntityFrameworkCore;
using MontyHall.Common.Types;
using MontyHallDataAccess.Database;
using MontyHallDataAccess.Database.Entities;
using MontyHallDataAccess.Interfaces;
using MontyHallDataAccess.Interfaces.Models;

namespace MontyHallDataAccess
{
    public class MontyHallAccess : IMontyHallAccess
    {
        private readonly MontyHallContext context;

        public MontyHallAccess(MontyHallContext context)
        {
            this.context = context;
        }

        public async Task<GameRecord?> GetGameAsync(Guid gameId)
        {
            var game = await context
                .MontyHallGames
                .Where(g => g.Id == gameId)
                .Select(g => new GameRecord
                {
                    Door1 = g.Door1,
                    Door2 = g.Door2,
                    Door3 = g.Door3,
                    PlayerDoor = g.PlayerDoor,
                    FirstDoor = g.FirstDoor,
                    SecondDoor = g.SecondDoor,
                    CanChangeDoor = g.CanChangeDoor,
                    GameId = g.Id,
                    BatchId = g.BatchId
                })
                .SingleOrDefaultAsync();

            return game;
        }

        public async Task<List<GameRecord>?> GetBatchAsync(Guid batchId)
        {
            return await context
                .MontyHallGames
                .Where(g => g.BatchId == batchId)
                .Select(game => new GameRecord
                {
                    Door1 = game.Door1,
                    Door2 = game.Door2,
                    Door3 = game.Door3,
                    PlayerDoor = game.PlayerDoor,
                    FirstDoor = game.FirstDoor,
                    SecondDoor = game.SecondDoor,
                    CanChangeDoor = game.CanChangeDoor,
                    GameId = game.Id,
                    BatchId = game.BatchId
                })
                .ToListAsync();
        }

        public async Task<Guid> AddGameAsync(Guid batchId, bool canChangeDoor)
        {
            context.EnableChanges();

            var random = new Random();
            var doorNo = Math.Abs(random.Next(1, 4));

            var montyHallGame = new MontyHallGame
            {
                BatchId = batchId,
                CanChangeDoor = canChangeDoor,
                Door1 = doorNo == 1 ? DoorContents.Car : DoorContents.Goat,
                Door2 = doorNo == 2 ? DoorContents.Car : DoorContents.Goat,
                Door3 = doorNo == 3 ? DoorContents.Car : DoorContents.Goat
            };

            await context.MontyHallGames.AddAsync(montyHallGame);

            await context.SaveChangesAsync();

            return montyHallGame.Id;
        }

        public async Task<GameRecord?> UpdateChoosenDoorAsync(Guid gameId, int playerDoor)
        {
            context.EnableChanges();

            var game = await context.MontyHallGames.FindAsync(gameId);

            if (game == null)
            {
                return null;
            }

            if (!game.CanChangeDoor && game.PlayerDoor != null)
            {
                return null;
            }

            game.DoorChangedOn = game.DoorChoosenOn != null ? DateTime.UtcNow : null;
            game.DoorChoosenOn = game.DoorChoosenOn ?? DateTime.UtcNow;
            game.PlayerDoor = playerDoor;

            await context.SaveChangesAsync();

            return new GameRecord
            {
                BatchId = game.BatchId,
                CanChangeDoor = game.CanChangeDoor,
                Door1 = game.Door1,
                Door2 = game.Door2,
                Door3 = game.Door3,
                FirstDoor = game.FirstDoor,
                SecondDoor= game.SecondDoor,
                PlayerDoor = game.PlayerDoor,
                GameId = game.Id
            };
        }

        public async Task OpenFirstDoorAsync(Guid gameId, int firstDoor)
        {
            context.EnableChanges();

            var game = await context.MontyHallGames.FindAsync(gameId);

            if (game != null)
            {
                game.FirstDoor = firstDoor;
                game.FirstDoorOpenedOn = DateTime.UtcNow;
            }

            await context.SaveChangesAsync();
        }

        public async Task OpenSecondDoorAsync(Guid gameId, int secondDoor)
        {
            context.EnableChanges();

            var game = await context.MontyHallGames.FindAsync(gameId);

            if (game != null)
            {
                game.SecondDoor = secondDoor;
                game.SecondDoorOpenedOn = DateTime.UtcNow;
            }

            await context.SaveChangesAsync();
        }

    }
}
