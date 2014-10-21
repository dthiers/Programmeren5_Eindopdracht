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
        [Display(Name="Start-datum")]
        public DateTime StartDatum { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Eind-datum")]
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
        [DataType(DataType.Date)]
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

        [Required(ErrorMessage = "Email is rhjhgjhjkjkhjk")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public int Prijs { get; set; }
        public int FactuurNummer { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage="Bankrekeningnummer moet een lengte van 10 hebben!"), MinLength(10)]
        public string BankrekeningNummer { get; set; }
        public int Capaciteit { get; set; }
    }
}
