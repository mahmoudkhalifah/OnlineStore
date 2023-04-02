using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public class ProductOrderRepoService : IProductOrderRepository
    {
        public MainDBContext context { get; set; }
        public ProductOrderRepoService(MainDBContext mainDBContext)
        {
            context = mainDBContext;
        }
        public int DeleteProductOrders(int OrderId, int ProductId)
        {
            try
            {
                ProductOrders productOrder = context.ProductOrders.Find(OrderId, ProductId);
                context.ProductOrders.Remove(productOrder);
                context.SaveChanges();
                return 1;
            }
            catch 
            {
                return 0;
            }
        }

        public List<ProductOrders> GetAll()
        {
            return context.ProductOrders
               .Include(pc => pc.Order)
               .Include(pc => pc.Product)
               .ToList();
        }

        public ProductOrders GetDetails(int OrderId, int ProductId)
        {
            return context.ProductOrders.Include(x => x.Product).Include(x => x.Order).Where(x => x.OrderId == OrderId && x.ProductId == ProductId).FirstOrDefault();

        }

        public int Insert(ProductOrders productOrders)
        {
            try
            {
                context.ProductOrders.Add(productOrders);
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int InsertList(List<ProductOrders> productOrdersList)
        {
            try
            {
                for(int i=0;i< productOrdersList.Count; i++)
                {
                    context.ProductOrders.Add(productOrdersList[i]);
                }
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int UpdateProductOrders(ProductOrders productOrders)
        {
            try
            {
                context.ProductOrders.Update(productOrders);
                context.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
