namespace MontyHallGame.Interfaces.Models.Requests
{
    public class ChooseDoorRequest
    {
        public Guid GameId { get; set; }
        public int DoorNo { get; set; }
    }
}
