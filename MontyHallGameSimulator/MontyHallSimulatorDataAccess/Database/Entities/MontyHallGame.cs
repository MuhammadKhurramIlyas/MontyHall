using System.ComponentModel.DataAnnotations;

namespace MontyHallSimulatorDataAccess.Database.Entities
{
    public class MontyHallGame
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BatchId { get; set; }
        public virtual MontyHallBatch MontyHallBatch { get; set; }
        public Guid GameId { get; set; }
        public bool IsWin { get; set; }
    }
}
