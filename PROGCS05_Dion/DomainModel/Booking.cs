using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class Booking
    {


        [Key]
        public int Id { get; set; }


        [DataType(DataType.Date)]
        public DateTime StartDatum { get; set; }

        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }


        
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room kamer { get; set; }

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

    }
}
