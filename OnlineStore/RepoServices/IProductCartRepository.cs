using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public interface IProductCartRepository
    {
        public List<ProductCart> GetAll();
        public ProductCart GetDetails(int CarId, int ProductId);
        public void Insert(ProductCart productCart);
        public void UpdateProductCart(ProductCart productCart);
        public void DeleteProductCart(int CarId, int ProductId);
    }
}
