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

        public int Capaciteit { get; set; }
    }
}
