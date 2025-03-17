using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Task_Xceed.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? CreationByUserId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }


        [Range(1, 365)]
        public int Duration { get; set; }

        public decimal Price { get; set; }


        [ForeignKey("Department")]
        public int CategoryId   { get; set; }

        public Category? Category { get; set; }
    }
}
