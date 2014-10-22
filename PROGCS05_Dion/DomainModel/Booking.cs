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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> StartDatum { get; set; }

        // ik had gevonden op google dat de display format zo moest voor het systeem omdat die het anders niet herkende.
        // Het database Date format is ook yyyy-MM-dd
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EindDatum { get; set; }
        
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> GeboorteDatum { get; set; }
        [Required]
        [Display(Name = "Man of Vrouw")]
        public string ManOfVrouw { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Woonplaats { get; set; }
        public string Email { get; set; }
        public int Prijs { get; set; }
        [Display(Name="Factuuradres")]
        public int FactuurNummer { get; set; }
        public string BankrekeningNummer { get; set; }
        public int Capaciteit { get; set; }

        public virtual ICollection<Guest> GuestList { get; set; }  
    }
}
