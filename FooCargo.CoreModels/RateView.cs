using System;
using System.Collections.Generic;
using System.Text;

namespace FooCargo.CoreModels
{
    // For the sql view sample
    public class RateView
    {
        public MailType MailType { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Amount { get; set; }
        public decimal NewAmount { get; set; }

    }
}
