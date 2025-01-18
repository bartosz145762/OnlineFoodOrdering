using System.ComponentModel.DataAnnotations;

namespace OnlineMealOrdering.Models
{
    public class DeliveryDetails
    {
        [Required(ErrorMessage = "Imię i nazwisko jest wymagane.")]
        [Display(Name = "Imię i nazwisko")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Adres jest wymagany.")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane.")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany.")]
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [Phone(ErrorMessage = "Wprowadź prawidłowy numer telefonu.")]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
    }
}
