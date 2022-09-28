namespace PIS.Lab3.Models
{
    public class WorkerJob : IEntity
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int JobId { get; set; }

        public override string ToString() => $"{Id}\t{WorkerId}\t{JobId}";
    }
}
