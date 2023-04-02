using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using OnlineStore.Models;
using System.Security.Cryptography;
using OnlineStore.RepoServices;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace OnlineStore.Controllers
{
    //[Authorize(Roles = "Admin")]
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
            ViewBag.orderState = OrderState.Processing;
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

        // GET: OrdersController/Create
        public ActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname");
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
            ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", ord.CustomerId);
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
                    ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", order.CustomerId);
                    return View(order);

                }
            }
            ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", order.CustomerId);
            return View(order);
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
