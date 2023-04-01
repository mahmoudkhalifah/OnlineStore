using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using OnlineStore.Models;
using System.Security.Cryptography;
using OnlineStore.RepoServices;

namespace OnlineStore.Controllers
{
    public class OrdersController : Controller
    {
        public IOrderRepository OrderRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public OrdersController(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            OrderRepository = orderRepository;
            CustomerRepository = customerRepository;
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
                OrderRepository.Insert(order);
                return RedirectToAction(nameof(Index));
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
