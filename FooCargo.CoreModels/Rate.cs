namespace FooCargo.CoreModels
{
    public partial class Rate
    {
        public MailType MailType { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Amount { get; set; }
    }
}
