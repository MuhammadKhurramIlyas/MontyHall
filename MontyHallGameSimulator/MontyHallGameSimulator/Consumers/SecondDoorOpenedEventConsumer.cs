using MassTransit;
using MontyHallGame.Interfaces.Events;
using MontyHallGameSimulator.Providers;

namespace MontyHallGameSimulator.Consumers
{
    public class SecondDoorOpenedEventConsumer : IConsumer<SecondDoorOpenedEvent>
    {
        private readonly ISimulationProvider simulationProvider;

        public SecondDoorOpenedEventConsumer(ISimulationProvider simulationProvider)
        {
            this.simulationProvider = simulationProvider;
        }

        public async Task Consume(ConsumeContext<SecondDoorOpenedEvent> context)
        {
            await simulationProvider.ProcessGameResultsAsync(context.Message.GameId);
        }
    }
}
