using System.ComponentModel.DataAnnotations;

namespace MontyHallSimulatorDataAccess.Database.Entities
{
    public class MontyHallBatch
    {
        [Key]
        public Guid BatchId { get; set; }
        public int TotalSimulations { get; set; }
        public bool CanChangeDoor { get; set; }

        public virtual ICollection<MontyHallGame> MontyHallGames { get; set; }
    }
}
