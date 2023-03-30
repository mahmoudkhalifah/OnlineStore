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

        public virtual List<ProductCart> ProductsCarts { get; set; } = new List<ProductCart>();

        public void AddProduct(Product product)
        {
            ProductsCarts.Add(new ProductCart()
            {
                CartId = this.CartId,
                ProductId = product.ProductID
            });

        }
        public void RemoveProduct(Product product)
        {
            ProductsCarts.Remove(ProductsCarts.Where(pc => pc.ProductId == product.ProductID && pc.CartId == CartId).First());

        }
        public void EditQuantity(Product product,int quantity)
        {

          ProductsCarts.Where(pc => pc.ProductId == product.ProductID && pc.CartId == CartId).First().ProductQuantity=quantity;
            
        }
        //public Order CreateOrder()
        //{
        //    Order ord= new Order();
        //    for(int i=0;i<)

        //    return ord;
        //}

    }
}
