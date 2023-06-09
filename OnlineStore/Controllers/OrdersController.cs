﻿    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using OnlineStore.Models;
using System.Security.Cryptography;
using OnlineStore.RepoServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using EllipticCurve.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace OnlineStore.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        public IOrderRepository OrderRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IProductOrderRepository ProductOrderRepository { get; }
        public IProductCartRepository ProductCartRepository { get; }
        public IProductRepository ProductRepository { get; }
        public OrdersController(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductOrderRepository productOrderRepository
            , IProductCartRepository productCartRepository, IProductRepository productRepository)
        {
            OrderRepository = orderRepository;
            CustomerRepository = customerRepository;
            ProductOrderRepository = productOrderRepository;
            ProductCartRepository = productCartRepository;
            ProductRepository = productRepository;
        }

        // GET: OrdersController
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
           
            return View(OrderRepository.GetAll());
        }
        [HttpPost]
        public ActionResult Index(OrderState? orderState, DateTime? orderDate, DateTime? arrivalDate, DateTime? shippingDate)
        {
            ViewBag.orderState = orderState;
            ViewBag.orderDate = orderDate;
            ViewBag.arrivalDate = arrivalDate;
            ViewBag.shippingDate = shippingDate;
            return View(OrderRepository.GetFilteredOrders(orderState: orderState,orderDate:orderDate, arrivalDate: arrivalDate, shippingDate: shippingDate));

        }
        [HttpPost]
        public ActionResult UpdateOrderState(int id,OrderState orderState)
        {
            OrderRepository.UpdateOrderState(id, orderState);
            return RedirectToAction(nameof(Index));

        }
        

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            return View(OrderRepository.GetDetails(id));
        }
        public List<ProductOrders> GetProductOrders(Cart cart , Order order)
        {
            List<ProductOrders> ret = new();
            foreach(var productInCart in cart.ProductsCarts)
            {
                ProductOrders productsOrder = new()
                {
                    OrderId = order.OrderId
                    ,
                    Order = order
                    ,
                    ProductId = productInCart.ProductId
                    ,
                    ProductQuantity = productInCart.ProductQuantity
                };
                ProductOrderRepository.Insert(productsOrder);
                //ProductCartRepository.DeleteProductCart()
                ret.Add(productsOrder);
            }
            return ret;
        }
        // GET: OrdersController/Checkout
        //[Authorize(Roles = "User")]
        public ActionResult Checkout(int id)//CustomertId
        {
            try
            {
                int CustomertId = id;
               
                Customer customer = CustomerRepository.GetDetails(CustomertId);
                if (customer.Addresses.Count == 0)
                {
                    return RedirectToAction("Create", "Addresses", new { id = customer.CustomerId});
                }
                ViewBag.CustomerId = customer.CustomerId;
                Order order = new Order() { Customer = customer , CustomerId = customer.CustomerId};
                //return View();
                return View(order);
                //return RedirectToAction(nameof(Details) , new { id = order.OrderId });
            }
            catch
            {
                //ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", order.CustomerId);
                //return View(order);
                return View();
            }
        }
        [HttpPost]
        public ActionResult Checkout( Order order)
        {
            try
            {
                //here: order is created
                //TODO: call email
                order.Customer = CustomerRepository.GetDetails(order.CustomerId);
                OrderRepository.Insert(order);
                order.ProductOrders = GetProductOrders(order.Customer.Cart , order);

                //TODO: empty cart , update quatity
                updateProductQuantity(order.Customer.Cart);
                emptyCart(order.Customer.Cart);


                return RedirectToAction(nameof(Details), new {id = order.OrderId});
            }
            catch
            {
                ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", order.CustomerId);
                return View(order);
            }
        }

        private void emptyCart(Cart cart)
        {
            while(!cart.ProductsCarts.IsNullOrEmpty())
            {
                ProductCartRepository.DeleteProductCart(cart.ProductsCarts[0].CartId, cart.ProductsCarts[0].ProductId);

            }
        }

        // GET: OrdersController/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.customers = CustomerRepository.GetAll();
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create(Order order)
        {
            try
            {
                if (ProductOrderRepository.InsertList(order.ProductOrders)==1 && OrderRepository.Insert(order)==1)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", order.CustomerId);
                return View(order);

            }
            catch
            {
                ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", order.CustomerId);
                return View(order);
            }
        }

        // GET: OrdersController/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ord = OrderRepository.GetDetails(id);
            if (ord == null)
            {
                return NotFound();
            }
            ViewBag.customers = CustomerRepository.GetAll();
            return View(ord);
        }

        // POST: OrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Edit(int id, Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    OrderRepository.UpdateOrder(id, order);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.customers = CustomerRepository.GetAll(); return View(order);

                }
            }
            ViewBag.customers = CustomerRepository.GetAll(); return View(order);
        }

        // GET: OrdersController/Delete/5
        [Authorize(Roles = "None")]
        public ActionResult Delete(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Order order = OrderRepository.GetDetails(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: OrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "None")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Order order= OrderRepository.GetDetails(id);

                foreach(var item in order.ProductOrders)
                {
                    ProductOrderRepository.DeleteProductOrders(id, item.ProductId);
                }
               
                OrderRepository.DeleteOrder(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        void updateProductQuantity(Cart cart)
        {
            foreach(var productInCart in cart.ProductsCarts)
            {
                var oldProduct = ProductRepository.GetDetails(productInCart.ProductId);
                oldProduct.Sold = oldProduct.Sold + productInCart.ProductQuantity;
                oldProduct.AvailableQuantity = oldProduct.AvailableQuantity - productInCart.ProductQuantity;

                //Product newProduct = new()
                //{
                //    ProductID = oldProduct.ProductID,
                //    ProductName = oldProduct.ProductName,
                //    AvailableQuantity = oldProduct.AvailableQuantity - productInCart.ProductQuantity,
                //    Sold = oldProduct.Sold + productInCart.ProductQuantity,
                //    Categories = oldProduct.Categories,
                //    Description = 
                //}
                ProductRepository.UpdateProduct(oldProduct);
            }
            
        }
    }
}
