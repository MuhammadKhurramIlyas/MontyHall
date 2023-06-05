using MassTransit;
using MontyHallGame.Interfaces.Events;
using MontyHallGameSimulator.Interfaces.Events;

namespace MontyHallGameSimulator.Consumers
{
    public class GameCreatedEventConsumer : IConsumer<GameCreatedEvent>
    {
        private readonly IPublishEndpoint publishEndpoint;

        public GameCreatedEventConsumer(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<GameCreatedEvent> context)
        {
            var rand = new Random();
            var doorNo = rand.Next(1, 4);
            await publishEndpoint.Publish(new ChooseDoorCommand
            {
                GameId = context.Message.GameId,
                PlayerDoor = doorNo
            });
        }
    }
}
