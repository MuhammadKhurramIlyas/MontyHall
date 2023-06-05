namespace MontyHallGame.Interfaces.Models.Responses
{
    public class ShowDoorResponse
    {
        public Guid GameId { get; set; }
        public int DoorNo { get; set; } = -1;
        public bool CanChangeDoor { get; set; }
    }
}
