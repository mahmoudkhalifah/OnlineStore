using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Models;
using System.Data;
using System.Security.Cryptography;
using OnlineStore.RepoServices;


namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        public ICartRepository cartRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public CartController(ICartRepository cartRepository, ICustomerRepository customerRepository)
        {
            this.cartRepository = cartRepository;
            CustomerRepository = customerRepository;
        }
        // GET: CartController
        public ActionResult Index(int id)
        {
            ViewBag.Cart = cartRepository.GetDetails(id);
            return View();
        }

        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartController/Create
        public ActionResult Create()
        {

            ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname");
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cart cart)
        {
            try
            {
                cartRepository.Insert(cart);
                return RedirectToAction("Index");
            }

            catch{
                ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", cart.CustomerId);
                return View(cart);
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            Cart cart = cartRepository.GetDetails(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", cart.CustomerId);
            return View(cart);
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cart cart)
        {
            if (ModelState.IsValid)
            {
                cartRepository.UpdateCart(cart);
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["CustomerId"] = new SelectList(CustomerRepository.GetAll(), "CustomerId", "Fname", cart.CustomerId);
                return View(cart);
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Cart cart = cartRepository.GetDetails(id);
            return View(cart);
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Cart cart = cartRepository.GetDetails(id);
                cartRepository.DeleteCart(id);
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
