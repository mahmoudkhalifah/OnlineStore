using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public interface IOrderRepository
    {
        public List<Order> GetAll();
        public Order GetDetails(int id);
        public int Insert(Order order);
        public int UpdateOrder(int id, Order order);
        public int DeleteOrder(int id);
/**/
        public int UpdateOrderState(int id,OrderState orderState);
        public List<Order> GetFilteredOrders(OrderState? orderState, DateTime? orderDate, DateTime? arrivalDate, DateTime? shippingDate);
    }
}
