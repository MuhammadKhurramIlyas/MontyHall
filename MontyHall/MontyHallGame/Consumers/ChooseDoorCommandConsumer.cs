using MassTransit;
using MontyHallGame.Interfaces.Models.Requests;
using MontyHallGame.Providers;
using MontyHallGameSimulator.Interfaces.Events;

namespace MontyHallGame.Consumers
{
    public class ChooseDoorCommandConsumer : IConsumer<ChooseDoorCommand>
    {
        private readonly IMontyHallProvider montyHallProvider;

        public ChooseDoorCommandConsumer(IMontyHallProvider montyHallProvider)
        {
            this.montyHallProvider = montyHallProvider;
        }
        public async Task Consume(ConsumeContext<ChooseDoorCommand> context)
        {
            await montyHallProvider.ChooseDoorAsync(new ChooseDoorRequest
            {
                DoorNo = context.Message.PlayerDoor,
                GameId = context.Message.GameId
            });
        }
    }
}
