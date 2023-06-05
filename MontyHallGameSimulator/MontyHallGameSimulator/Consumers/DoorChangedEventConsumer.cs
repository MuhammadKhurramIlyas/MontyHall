using MassTransit;
using MontyHallGame.Interfaces.Events;
using MontyHallGameSimulator.Interfaces.Events;

namespace MontyHallGameSimulator.Consumers
{
    public class DoorChangedEventConsumer : IConsumer<DoorChangedEvent>
    {
        private readonly IPublishEndpoint publishEndpoint;

        public DoorChangedEventConsumer(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<DoorChangedEvent> context)
        {
            await publishEndpoint.Publish(new ShowDoorCommand
            {
                GameId = context.Message.GameId
            });
        }
    }
}
