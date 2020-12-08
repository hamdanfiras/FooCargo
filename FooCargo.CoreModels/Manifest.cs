using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FooCargo.CoreModels
{
    public partial class Manifest
    {
        public string FlightNumber { get; set; }
        public DateTime Date { get; set; }

        public List<Shipment> Shipments { get; set; }
    }
}
