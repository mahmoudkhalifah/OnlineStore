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
            context.Carts.Load();
            context.Products.Load();
            //  var obj = context.TraineeCourses.Find(TraineeId, CourseId);
            return context.ProductsCarts.Find(CarId, ProductId);
        }

        public void Insert(ProductCart productCart)
        {
            context.ProductsCarts.Add(productCart);
            context.SaveChanges();
        }

        public void UpdateProductCart(ProductCart productCart)
        {
            context.Entry(productCart).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
