using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DomainModel {
    public class Room {
        [Key]
        public int Id { get; set; }
        public int Capaciteit { get; set; }


        /// <summary>
        /// Omdat je in Booking een referentie hebt naar Kamer, heb je ook een referentie de andere kant op.
        /// Het is een '1 op veel' relatie. 
        /// 
        /// 1 Kamer heeft meerrdere Bookings.
        /// 
        /// Daarom heb je hier een 'navigation property' met een lijst van bookingen
        /// </summary>
        public virtual ICollection<Booking> BookingList { get; set; }     
    }
}
