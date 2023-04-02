using Microsoft.AspNetCore.Http;
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

namespace OnlineStore.Controllers
{

   // [Authorize(Roles = "Admin")]

    public class OrdersController : Controller
    {
        public IOrderRepository OrderRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IProductOrderRepository ProductOrderRepository { get; }
        public OrdersController(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductOrderRepository productOrderRepository)
        {
            OrderRepository = orderRepository;
            CustomerRepository = customerRepository;
            ProductOrderRepository = productOrderRepository;
        }

        // GET: OrdersController
        public ActionResult Index()
        {
            return View(OrderRepository.GetAll());
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            return View(OrderRepository.GetDetails(id));
        }
        public List<ProductOrders> GetProductOrders(List<ProductCart>pcs)
        {
            List<ProductOrders> ret = new();
            foreach(var pc in pcs)
            {
                //ret.Add(new ProductOrders() { })
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
                if (CustomertId == 0)
                    CustomertId = 1;

                //if (order.Customer == null)
                //    order.Customer = CustomerRepository.GetDetails(order.CustomerId);
                //order.Products = new(order.Customer.Cart.ProductsList());
                //TODO: empty cart
                //ViewBag.orderStates = new SelectList(Enums.OrderState)
                Customer customer = CustomerRepository.GetDetails(CustomertId);
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
                //TODO: empty the cart
                OrderRepository.Insert(order);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", order.CustomerId);
                return View(order);
            }
        }
        // GET: OrdersController/Create
        public ActionResult Create()
        {
            ViewBag.customers = CustomerRepository.GetAll();
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
    }
}
