using OnlineStore.Models;
using System.Collections.Generic;

namespace OnlineStore.RepoServices
{
    public interface IAddressRepository
    {
        public List<Address> GetAll();
        public Address GetDetails(int id);
        public int Insert(Address address);
        public int UpdateAddress(int id, Address address);
        public int DeleteAddress(int id);
    }
}
