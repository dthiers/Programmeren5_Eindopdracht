using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class Booking{
        
        [Key]
        public int Id { get; set; }
        [Required]
        public string Voornaam { get; set; }
        public string Tussenvoegsel { get; set; }
        [Required]
        public string Achternaam { get; set; }
        [Required]
        public DateTime GeboorteDatum { get; set; }
        [Required]
        [Display(Name = "Man of Vrouw")]
        public string ManOfVrouw { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Woonplaats { get; set; }
        [Required]
        [Display(Name = "Emailadres")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Check-in datum")]
        public DateTime CheckInDatum { get; set; }
        [Required]
        [Display(Name = "Check-out datum")]
        public DateTime CheckOutDatum { get; set; }
        [Required]
        [Display(Name = "Kamer  2, 3 of 5")]
        public int Kamer { get; set; }
        public int Prijs { get; set; }
    }
}
