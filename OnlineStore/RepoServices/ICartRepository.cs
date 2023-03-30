using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public interface ICartRepository
    {
        public List<Cart> GetAll();
        public Cart GetDetails(int id);
        public void Insert(Cart cart);
        public void UpdateCart(Cart cart);
        public void DeleteCart(int id);
    }
}
