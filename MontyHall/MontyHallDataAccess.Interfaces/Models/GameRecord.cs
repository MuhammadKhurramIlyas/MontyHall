using MontyHall.Common.Types;

namespace MontyHallDataAccess.Interfaces.Models
{
    public class GameRecord
    {
        public DoorContents Door1 { get; set; }
        public DoorContents Door2 { get; set; }
        public DoorContents Door3 { get; set; }
        public int? PlayerDoor { get; set; }
        public int? FirstDoor { get; set; }
        public int? SecondDoor { get; set; }
        public bool CanChangeDoor { get; set; }
        public Guid GameId { get; set; }
        public Guid BatchId { get; set; }
    }
}
