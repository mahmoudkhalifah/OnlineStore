using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [NotMapped]
        public decimal CartTotalBill{
            get { decimal sum = 0; for (int i = 0; i < ProductsCarts.Count; i++) sum += ProductsCarts[i].Product.Price;
                return sum;
            }
        }

        public virtual List<ProductCart> ProductsCarts { get; set; } = new List<ProductCart>();

        public void AddProduct(int productId)
        {
            //ProductsCarts.Add(new ProductCart()
            //{
            //    CartId = this.CartId,
            //    ProductId = productId
            //}) ;

        }
        public int RemoveProduct(int productId)
        {
            ProductsCarts.Remove(ProductsCarts.Where(pc => pc.ProductId == productId && pc.CartId == CartId).First());
            return 1;
        }
        public void EditQuantity(int productId, int quantity)
        {

          ProductsCarts.Where(pc => pc.ProductId == productId && pc.CartId == CartId).First().ProductQuantity=quantity;
            
        }
       

    }
}
