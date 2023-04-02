using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public class OrderRepository : IOrderRepository
    {
        public MainDBContext Context { get; }
        public OrderRepository(MainDBContext context)
        {
            Context = context;
        }
        public int DeleteOrder(int id)
        {
            try
            {
                Context.Remove(Context.Orders.Find(id));
                Context.SaveChanges();
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public List<Order> GetAll()
        {
            return Context.Orders.ToList();
        }

        public Order GetDetails(int id)
        {
            return Context.Orders.Find(id);
        }

        public int Insert(Order order)
        {
            
            try
            {
                Context.Orders.Add(order);
                Context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateOrder(int id, Order order)
        {
            
            try

            {
                Order order1 = Context.Orders.Find(id);

                order1.ArrivalDate = order.ArrivalDate;
                order1.OrderDate = order.OrderDate;
                order1.ShippingDate = order.ShippingDate;
                order1.OrderState = order.OrderState;
                order1.Bill = order.Bill;
                order1.PaymentMethod = order.PaymentMethod;
                Context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateOrderState(int id, OrderState orderState)
        {
            try
            {
                Context.Orders.Find(id).OrderState = orderState;
                Context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public List<Order> GetFilteredOrders(OrderState? orderState,DateTime? orderDate, DateTime? arrivalDate, DateTime? shippingDate)
        {

            List<Order> orders = Context.Orders.ToList();
            if(orderState != null)
            {
                orders = orders.Where(o=>o.OrderState == orderState).ToList();
            }
            if(arrivalDate != null)
            {
                orders = orders.Where(o => o.ArrivalDate == arrivalDate).ToList();
            }
            if (orderDate != null)
            {
                orders = orders.Where(o => o.OrderDate == orderDate).ToList();
            }
            if (shippingDate != null)
            {
                orders = orders.Where(o => o.ShippingDate == shippingDate).ToList();
            }
            return orders;
        }
    }
}
