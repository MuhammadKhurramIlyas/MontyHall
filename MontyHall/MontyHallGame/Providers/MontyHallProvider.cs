using MassTransit;
using MontyHallDataAccess.Interfaces;
using MontyHallGame.Interfaces.Events;
using MontyHallGame.Interfaces.Models.Requests;
using MontyHallGame.Interfaces.Models.Responses;
using MontyHall.Common.Types;
using MontyHallDataAccess.Interfaces.Models;

namespace MontyHallGame.Providers
{
    public class MontyHallProvider : IMontyHallProvider
    {
        private readonly IMontyHallAccess montyHallAccess;
        private readonly IPublishEndpoint publishEndPoint;

        public MontyHallProvider(IMontyHallAccess montyHallAccess, IPublishEndpoint publishEndPoint)
        {
            this.montyHallAccess = montyHallAccess;
            this.publishEndPoint = publishEndPoint;
        }

        public async Task<CreateGameResponse> CreateGameAsync(CreateGameRequest request)
        {
            var newGameId = await montyHallAccess.AddGameAsync(request.BatchId, request.CanChangeDoor);

            await publishEndPoint.Publish(new GameCreatedEvent
            {
                GameId = newGameId,
                CanChangeDoor = request.CanChangeDoor
            });

            return new CreateGameResponse
            {
                GameId = newGameId,
                CanChangeDoor = request.CanChangeDoor
            };
        }

        public async Task ChooseDoorAsync(ChooseDoorRequest request)
        {
            var result = await montyHallAccess.UpdateChoosenDoorAsync(request.GameId, request.DoorNo);

            if (result == null)
            {
                return;
            }

            if (result.FirstDoor == null)
            {
                await publishEndPoint.Publish(new DoorChoosenEvent
                {
                    GameId = request.GameId
                });

                return;
            }

            await publishEndPoint.Publish(new DoorChangedEvent
            {
                GameId = request.GameId
            });
        }

        public async Task<ShowDoorResponse> ShowDoorAsync(ShowDoorRequest request)
        {
            var result = new ShowDoorResponse()
            {
                GameId = request.GameId
            };

            var game = await montyHallAccess.GetGameAsync(request.GameId);

            if (game == null)
            {
                return result;
            }

            if (game!.FirstDoor == null)
            {
                var lstDoorsToChoose = Enumerable
                    .Range(1, 3)
                    .ToList();

                lstDoorsToChoose
                    .Remove(game.PlayerDoor!.Value);

                var random = new Random();
                var doorNo = lstDoorsToChoose[random.Next(0, 2)];
                await montyHallAccess.OpenFirstDoorAsync(request.GameId, doorNo);

                result.DoorNo = doorNo;
                result.CanChangeDoor = game.CanChangeDoor;

                await publishEndPoint.Publish(new FirstDoorOpenedEvent
                {
                    CanChangeDoor = game.CanChangeDoor,
                    GameId = game.GameId,
                    FirstDoor = result.DoorNo
                });

                return result;
            }

            if (game.SecondDoor == null)
            {
                var lstDoorsToChoose = Enumerable
                    .Range(1, 3)
                    .ToList();

                lstDoorsToChoose
                    .Remove(game.PlayerDoor!.Value);

                lstDoorsToChoose
                    .Remove(game.FirstDoor!.Value);

                var doorNo = lstDoorsToChoose.Single();
                await montyHallAccess.OpenSecondDoorAsync(request.GameId, doorNo);

                result.DoorNo = doorNo;
                result.CanChangeDoor = false;

                await publishEndPoint.Publish(new SecondDoorOpenedEvent
                {
                    GameId = game.GameId
                });

                return result;
            }

            return result;
        }

        public async Task ChangeDoorAsync(ChooseDoorRequest request)
        {
            await montyHallAccess.UpdateChoosenDoorAsync(request.GameId, request.DoorNo);
        }

        public async Task<GameResult?> GetGameResultAsync(Guid gameId)
        {
            var game = await montyHallAccess.GetGameAsync(gameId);

            if (game == null)
            {
                return null;
            }

            return new GameResult
            {
                BatchId = game.BatchId,
                GameId = game.GameId,
                IsWin = isWin(game)
            };

            static bool isWin(GameRecord game)
            {
                return (game.PlayerDoor == 1 && game.Door1 == DoorContents.Car) ||
                    (game.PlayerDoor == 2 && game.Door2 == DoorContents.Car) ||
                    (game.PlayerDoor == 3 && game.Door3 == DoorContents.Car);
            }
        }


        public async Task<BatchResult?> GetBatchResultAsync(Guid batchId)
        {
            var games = await montyHallAccess.GetBatchAsync(batchId);

            if (games == null)
            {
                return null;
            }

            var totalWins = games.Count(x => isWin(x));
            var totalDefeats = games.Count - totalWins;

            return new BatchResult
            {
                BatchId = batchId,
                TotalWins = totalWins,
                TotalDefeats = totalDefeats
            };

            static bool isWin(GameRecord game)
            {
                return (game.PlayerDoor == 1 && game.Door1 == DoorContents.Car) ||
                    (game.PlayerDoor == 2 && game.Door2 == DoorContents.Car) ||
                    (game.PlayerDoor == 3 && game.Door3 == DoorContents.Car);
            }
        }


    }
}
