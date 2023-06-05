using MassTransit;
using MontyHallGame.Interfaces.Events;
using MontyHallGameSimulator.Interfaces.Events;

namespace MontyHallGameSimulator.Consumers
{
    public class DoorChoosenEventConsumer : IConsumer<DoorChoosenEvent>
    {
        private readonly IPublishEndpoint publishEndpoint;

        public DoorChoosenEventConsumer(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<DoorChoosenEvent> context)
        {
            await publishEndpoint.Publish(new ShowDoorCommand
            {
                GameId = context.Message.GameId
            });
        }
    }
}
