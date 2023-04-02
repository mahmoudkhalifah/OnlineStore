using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public class ProductRepositoryService:IProductRepository
    {
        public MainDBContext context { get; set; }
        public ProductRepositoryService(MainDBContext mainDBContext)
        {
            context = mainDBContext;
        }

        public List<Product> GetAll()
        {
            return context.Products.Include(p=>p.Categories).ToList();
        }

        public Product GetDetails(int id)
        {
            return context.Products.Find(id);
        }

        public void Insert(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            Product product = context.Products.Find(id);
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
