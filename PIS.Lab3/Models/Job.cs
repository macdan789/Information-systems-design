namespace PIS.Lab3.Models
{
    public class Job : IEntity
    {
        public int JobId { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"{JobId}\t{Description}";
    }
}
