using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public class CartRepositoryService : ICartRepository
    {
        public MainDBContext context { get; set; }
        public CartRepositoryService(MainDBContext mainDBContext)
        {
            context = mainDBContext;
        }
        public void DeleteCart(int id)
        {
            Cart cart = context.Carts.Find(id);
            context.Carts.Remove(cart);
            context.SaveChanges();
        }

        public List<Cart> GetAll()
        {
            return context.Carts.ToList();
        }

        public Cart GetDetails(int id)
        {
            return context.Carts.Find(id);
        }

        public void Insert(Cart cart)
        {
            context.Carts.Add(cart);
            context.SaveChanges();
        }

        public void UpdateCart(Cart cart)
        {
            context.Entry(cart).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
