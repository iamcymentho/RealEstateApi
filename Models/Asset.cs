using System.Text.Json.Serialization;

namespace RealEstateApi.Models
{
    public class Asset
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public string Address { get; set; }

        public string ImageUrl { get; set; }

        public double Price { get; set; }

        public bool IsTrending { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category category { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User user { get; set; }

    }
}
