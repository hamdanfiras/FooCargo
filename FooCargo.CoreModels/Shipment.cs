
namespace FooCargo.CoreModels
{
    public partial class Shipment
    {
        public string AWBNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public MailType MailType { get; set; }
        public decimal Weight { get; set; }
        public int NumberOfItems { get; set; }
        public decimal Fee { get; set; }

        public Manifest Manifest { get; set; }
    }
}
