using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class ProductOrders
    {

        //[ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        //[ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        
        [Range(0, int.MaxValue)]
        public int? ProductQuantity { get; set; }
    }
}
