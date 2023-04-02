using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public class ProductCartRepositoryService : IProductCartRepository
    {
        public MainDBContext context { get; set; }
        public ProductCartRepositoryService(MainDBContext mainDBContext)
        {
            context = mainDBContext;
        }
        public void DeleteProductCart(int CarId, int ProductId)
        {
            ProductCart productCart = context.ProductsCarts.Find(CarId, ProductId);
            context.ProductsCarts.Remove(productCart);
            context.SaveChanges();
        }

        public List<ProductCart> GetAll()
        {
            return context.ProductsCarts
                .Include(pc => pc.Cart)
                .Include(pc => pc.Product)
                .ToList();
        }

        public ProductCart GetDetails(int CarId, int ProductId)
        {
            
       
            return context.ProductsCarts.Include(x=>x.Product).Include(x=>x.Cart).Where(x=>x.CartId==CarId &&x.ProductId==ProductId).First();
        }

        public void Insert(ProductCart productCart)
        {
            context.ProductsCarts.Add(productCart);
            context.SaveChanges();
        }

        public void UpdateProductCart(ProductCart productCart)
        {
            context.ProductsCarts.Update(productCart);
            context.SaveChanges();
        }
    }
}
