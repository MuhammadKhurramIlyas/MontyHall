using System.ComponentModel.DataAnnotations;

namespace MontyHallGameSimulator.Interfaces.Models.Request
{
    public class CreateSimulationRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Total Simulations can be at least 1.")]
        public int TotalSimulations { get; set; } = 1;
        public bool CanChangeDoor { get; set; } = false;
    }
}
