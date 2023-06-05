using MontyHall.Common.Types;

namespace MontyHallDataAccess.Database.Entities
{
    public class MontyHallGame
    {
        public Guid Id { get; set; }
        public Guid BatchId { get; set; }
        public bool CanChangeDoor { get; set; }
        public DoorContents Door1 { get; set; }
        public DoorContents Door2 { get; set; }
        public DoorContents Door3 { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? StartedOn { get; set; }
        public DateTime? DoorChoosenOn { get; set; }
        public int? PlayerDoor { get; set; }
        public DateTime? FirstDoorOpenedOn { get; set; }
        public int? FirstDoor { get; set; }
        public DateTime? DoorChangedOn { get; set; }
        public DateTime? SecondDoorOpenedOn { get; set; }
        public int? SecondDoor { get; set; }
    }
}
