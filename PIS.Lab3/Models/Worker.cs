namespace PIS.Lab3.Models
{
    public class Worker : IEntity
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }  
        public string RooName { get; set; }

        public override string ToString() => $"{WorkerId}\t{Name}\t{RooName}";
    }
}
