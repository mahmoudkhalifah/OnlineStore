using OnlineStore.Models;
namespace OnlineStore.RepoServices
{
    public interface IProductOrderRepository
    {
        public List<ProductOrders> GetAll();
        public ProductOrders GetDetails(int OrderId, int ProductId);
        public int Insert(ProductOrders productOrders);
        public int InsertList(List<ProductOrders> productOrdersList);
        public int UpdateProductOrders(ProductOrders productOrders);
        public int DeleteProductOrders(int OrderId, int ProductId);
    }
}
