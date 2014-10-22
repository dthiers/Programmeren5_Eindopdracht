using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROGCS05_Dion.Models
{
    public class StartBookingViewModel
    {
        [DataType(DataType.Date)]
        public DateTime BeginDatum { get; set; }

        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }

        [Display(Name="Capaciteit: 2,3 of 5")]
        public string Capaciteit { get; set; }
    }
}
