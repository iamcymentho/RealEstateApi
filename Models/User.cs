using System.ComponentModel.DataAnnotations;

namespace RealEstateApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Name Field can't be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fill in a valid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password Field can't be empty")]
        public string Password { get; set; }    

        public string PhoneNumber { get; set; }

        public ICollection<Property> Properties { get; set; }  // Estatblishing One - Many relationship between Users and Properties
    }
}
