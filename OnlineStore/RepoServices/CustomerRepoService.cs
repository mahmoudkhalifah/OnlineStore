﻿using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public class CustomerRepoService : ICustomerRepository
    {
        private readonly MainDBContext Context;
        public CustomerRepoService(MainDBContext context)
        {
            Context = context;
        }

        public int DeleteCustomer(int id)
        {
            try {

                Context.Customers.Remove(Context.Customers.Find(id));
                Context.SaveChanges();
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
          
        }

        public List<Customer> GetAll()
        {
            return Context.Customers.ToList();
        }

        public Customer GetDetails(int id)
        {
            return Context.Customers.Find(id);
        }

        public int Insert(Customer customer)
        {
            try
            {
                Context.Customers.Add(customer);
                Context.SaveChanges();
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
             
        }

        public int UpdateCustomer(int id, Customer customer)
        {
            try {

                Customer Cust = Context.Customers.Find(id);
                Cust.PhoneNumber=customer.PhoneNumber;
                Cust.Fname=customer.Fname;
                Cust.Lname=customer.Lname;
                Cust.Addresses=customer.Addresses;
                Cust.Gender=customer.Gender;
                Cust.Cart=customer.Cart;
                Cust.CartId=customer.CartId;
                Cust.Orders=customer.Orders;

                //Cust = customer;
                Context.SaveChanges();
                return 1;
            }
            catch(Exception ex) {
                return 0;
            }

        }
    }
}