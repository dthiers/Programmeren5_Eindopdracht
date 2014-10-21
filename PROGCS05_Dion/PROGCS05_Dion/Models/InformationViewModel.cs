using PROGCS05_Dion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROGCS05_Dion
{
    public class InformationViewModel
    {
        [Required]
        public string Voornaam { get; set; }
        public string Tussenvoegsel { get; set; }
        [Required]
        public string Achternaam { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime GeboorteDatum { get; set; }
        [Required]
        [Display(Name = "Geslacht")]
        public string ManOfVrouw { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Woonplaats { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name="Bankrekeningnummer (IBAN)")]
        [Required(ErrorMessage = "Uw bankrekeningnummer (IBAN) moet 10 karakters bevatten")]
        [MaxLength(10, ErrorMessage = "Bankrekeningnummer moet een lengte van 10 hebben!"), MinLength(10)]
        public string Bankrekeningnummer { get; set; }
        public StartBookingViewModel BookingInformation { get; set; }

        public InformationViewModel()
        {
            BookingInformation = new StartBookingViewModel();
        }
    }
}
