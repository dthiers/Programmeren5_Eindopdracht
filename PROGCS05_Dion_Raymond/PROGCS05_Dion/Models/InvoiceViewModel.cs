using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROGCS05_Dion.Models
{
    public class InvoiceViewModel
    {
        public int TotaalPrijs { get; set; }
        public int FactuurNummer { get; set; }
        public string BankNummer { get; set; }
    }
}