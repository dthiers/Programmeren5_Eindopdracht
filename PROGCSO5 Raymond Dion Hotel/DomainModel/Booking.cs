using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class Booking
    {
        [Key]
        public int Sleutel { get; set; }
        public string Voornaam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string ManOfVrouw { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Woonplaats { get; set; }
        public string Email { get; set; }
        public DateTime CheckInDatum { get; set; }
        public DateTime CheckOutDatum { get; set; }
        public int Kamer { get; set; }
        public int Prijs { get; set; }
    }
}
