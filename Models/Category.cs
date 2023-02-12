using System.ComponentModel.DataAnnotations;

namespace RealEstateApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Category name can't be empty")]  // "REQUIRED" is a form of model validation
        public string Name { get; set; } // can make the property nullable by putting a question mark(?) after the data type

        [Required (ErrorMessage = "ImageUrl can't be empty")]
        public string ImageUrl { get; set; }

        //public ICollection<Property> Properties { get; set; } // Estatblishing One - Many relationship between Categories and Properties

        public ICollection<Asset> Assets { get; set; } // Estatblishing One - Many relationship between Categories and Properties

    }
}
