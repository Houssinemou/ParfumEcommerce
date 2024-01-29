using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
namespace ParfumEcommerce.Models
{
    public class Parfum
    {
        [Key]
        public int ID { get; set; }
        public string? ParfumName { get; set; }
        public float Prix { get; set; }
        
        public string? Image { get; set; }
       

        // Navigation property for the relationship with Categorie
        public Categorie? Categorie { get; set; }

        [ForeignKey("Categorie")]
        public int CategorieId { get; set; }

    } 
}
