using MassTransit;
using MontyHallGame.Interfaces.Events;
using MontyHallGameSimulator.Interfaces.Events;

namespace MontyHallGameSimulator.Consumers
{
    public class FirstDoorOpenedEventConsumer : IConsumer<FirstDoorOpenedEvent>
    {
        private readonly IPublishEndpoint publishEndpoint;

        public FirstDoorOpenedEventConsumer(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<FirstDoorOpenedEvent> context)
        {
            if (context.Message.CanChangeDoor)
            {
                var lstDoorsToChoose = Enumerable
                    .Range(1, 3)
                    .ToList();

                lstDoorsToChoose
                    .Remove(context.Message.FirstDoor);

                var random = new Random();
                var doorNo = lstDoorsToChoose[random.Next(0, 2)];
                await publishEndpoint.Publish(new ChangeDoorCommand
                {
                    GameId = context.Message.GameId,
                    PlayerDoor = doorNo
                });

                return;
            }

            await publishEndpoint.Publish(new ShowDoorCommand
            {
                GameId = context.Message.GameId
            });
        }
    }
}
