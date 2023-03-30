using OnlineStore.Models;
namespace OnlineStore.RepoServices
{
    public interface ICustomerRepository
    {
        public List<Customer> GetAll();
        public Customer GetDetails(int id);
        public int Insert(Customer customer);
        public int UpdateCustomer(int id, Customer customer);
        public int DeleteCustomer(int id);
    }
}
