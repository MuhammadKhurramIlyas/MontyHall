using MassTransit;
using MontyHallGame.Interfaces.Models.Requests;
using MontyHallGame.Providers;
using MontyHallGameSimulator.Interfaces.Events;

namespace MontyHallGame.Consumers
{
    public class CreateGameCommandConsumer : IConsumer<CreateGameCommand>
    {
        private readonly IMontyHallProvider provider;

        public CreateGameCommandConsumer(IMontyHallProvider provider)
        {
            this.provider = provider;
        }

        public async Task Consume(ConsumeContext<CreateGameCommand> context)
        {
            await provider.CreateGameAsync(new CreateGameRequest
            {
                BatchId = context.Message.BatchId,
                CanChangeDoor = context.Message.CanChangeDoor
            });
        }
    }
}
