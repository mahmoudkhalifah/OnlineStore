using OnlineStore.Models;
using OnlineStore.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.RepoServices
{
    public class AddressRepoService: IAddressRepository
    {
        public MainDBContext Context { get; }

        //request service of type mainDbContext to be injected in Ctor
        public AddressRepoService(MainDBContext context)
        {
            Context = context;
        }
        public List<Address> GetAll()
        {
            return Context.Addresses.ToList();
        }

        public Address GetDetails(int id)
        {
            return Context.Addresses.Find(id);
        }

        public int Insert(Address address)
        {
            try
            {
                Context.Addresses.Add(address);
                Context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateAddress(int id, Address address)
        {
            try
            {

                Address add = Context.Addresses.Find(id);
                add.FloorNumber=address.FloorNumber;
                add.NearestLandmark=address.NearestLandmark;
                add.ApartmentNo=address.ApartmentNo;
                add.City =address.City;
                add.Zone=address.Zone;
                add.Country=address.Country;
                add.Street= address.Street;
                add.Governorate=address.Governorate;
                

                Context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteAddress(int id)
        {
            try
            {
                Context.Addresses.Remove(Context.Addresses.Find(id));
                Context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
