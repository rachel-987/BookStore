using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 1000000)]
        public double Price50Book { get; set; }

        [Required]
        [Range(1, 1000000)]
        public double Price100Book { get; set; }

        [ValidateNever]
        public string ImageURL { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        [ValidateNever]
        public Category Category { get; set; }

        public int CoverTypeID { get; set; }

        [ValidateNever]
        [ForeignKey("CoverTypeID")]
        public CoverType CoverType { get; set; }
    }
}