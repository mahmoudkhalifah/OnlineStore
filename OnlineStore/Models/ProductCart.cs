using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class ProductCart
    {
        //[ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        //[ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ProductQuantity { get; set; }
    }
}
