using MassTransit;
using MontyHallGame.Interfaces.Models.Requests;
using MontyHallGame.Providers;
using MontyHallGameSimulator.Interfaces.Events;

namespace MontyHallGame.Consumers
{
    public class ShowDoorCommandConsumer : IConsumer<ShowDoorCommand>
    {
        private readonly IMontyHallProvider montyHallProvider;

        public ShowDoorCommandConsumer(IMontyHallProvider montyHallProvider)
        {
            this.montyHallProvider = montyHallProvider;
        }
        public async Task Consume(ConsumeContext<ShowDoorCommand> context)
        {
            await montyHallProvider.ShowDoorAsync(new ShowDoorRequest
            {
                GameId = context.Message.GameId
            });
        }
    }
}
