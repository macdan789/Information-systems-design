namespace PIS.Lab3.Models
{
    public class ResidentialOperatingOffice : IEntity
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string City { get; set; }

        public override string ToString() => $"{ShortName}\t{LongName}\t{City}";
    }
}
