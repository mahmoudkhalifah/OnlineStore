using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required,MaxLength(50, ErrorMessage = "Reached maximum number of characters (50)")]
        public string CategoryName { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        //foregin key (Many to many)
        public virtual List<Product> Products { get; set; } = new List<Product>();

    }
}
