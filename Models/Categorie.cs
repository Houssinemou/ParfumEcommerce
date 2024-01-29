using System.ComponentModel.DataAnnotations;
namespace ParfumEcommerce.Models
{

    public class Categorie
    {
        [Key]

        public int ID { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }

        // You can add more properties related to the category

        public ICollection<Parfum>? Parfums { get; set; }

    }
}
