using MassTransit;
using MontyHallGame.Interfaces.Models.Requests;
using MontyHallGame.Providers;
using MontyHallGameSimulator.Interfaces.Events;

namespace MontyHallGame.Consumers
{
    public class ChangeDoorCommandConsumer : IConsumer<ChangeDoorCommand>
    {
        private readonly IMontyHallProvider montyHallProvider;

        public ChangeDoorCommandConsumer(IMontyHallProvider montyHallProvider)
        {
            this.montyHallProvider = montyHallProvider;
        }
        public async Task Consume(ConsumeContext<ChangeDoorCommand> context)
        {
            await montyHallProvider.ChooseDoorAsync(new ChooseDoorRequest
            {
                DoorNo = context.Message.PlayerDoor,
                GameId = context.Message.GameId,
            });
        }
    }
}
