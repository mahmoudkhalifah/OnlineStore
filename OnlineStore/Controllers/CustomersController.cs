using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.RepoServices;


namespace OnlineStore.Controllers
{
    public class CustomersController : Controller
    {
        public ICartRepository cartRepo { get;}
        public ICustomerRepository customerRepo  { get;}
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public CustomersController( ICustomerRepository custrepo, ICartRepository carrepo, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            cartRepo = carrepo;
            customerRepo = custrepo;
            this._userManager = userManager;
            this._signInManager = _signInManager;
        }

        // GET: Customers
        public  ActionResult Index()
        {
            return View(customerRepo.GetAll());
        }

        // GET: Customers/Details/5
        public  IActionResult Details(int? id)
        {
            if (id != null)
            {
                Customer customer = customerRepo.GetDetails((int)id);
                if (customer == null)
                    return NotFound();

                return View(customer);
            }
            else return NotFound();
        }

        // GET: Customers/Create
        public async Task< IActionResult> Create()
        {

            //try
            //{
            //    if (customerRepo.Insert(customer) == 1)
            //        return RedirectToAction(nameof(Index));
            //    else return View(customer); ;
            //}
            //catch
            //{
            //    ViewData["CartId"] = new SelectList(cartRepo.GetAll(), "CartId", "CartId", customer.CartId);
            //    return View(customer);
            //}
            ViewData["CartId"] = new SelectList(cartRepo.GetAll(), "CartId", "CartId");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Fname,Lname,Gender,PhoneNumber,CartId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (customerRepo.Insert(customer) == 1)
                        return RedirectToAction(nameof(Index));
                    else return View(customer); ;
                }
                catch
                {
                    ViewData["CartId"] = new SelectList(cartRepo.GetAll(), "CartId", "CartId", customer.CartId);
                    return View(customer);
                }

            }
            ViewData["CartId"] = new SelectList(cartRepo.GetAll(), "CartId", "CartId" ,customer.CartId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cust = customerRepo.GetDetails((int)id);
            if (cust == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(cartRepo.GetAll(), "CartId", "CartId", cust.CartId);
            return View(cust);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Fname,Lname,Gender,PhoneNumber,CartId")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customerRepo.UpdateCustomer(id, customer);

                }
                catch (Exception e)
                {

                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(cartRepo.GetAll(), "CartId", "CartId", customer.CartId);
            return View(customer);

        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = customerRepo.GetDetails((int)id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (customerRepo.DeleteCustomer(id) == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
