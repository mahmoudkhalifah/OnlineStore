using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.RepoServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
namespace OnlineStore.Controllers
{
    public class AddressesController : Controller
    {
        public IAddressRepository addressRepo { get;}
        public ICustomerRepository customerRepo { get; }

        public AddressesController(IAddressRepository Addrepo,ICustomerRepository custrepo )
        {
            addressRepo = Addrepo;
            customerRepo = custrepo;
        }

        // GET: Addresses
        public  ActionResult Index()
        {
           
            return View(addressRepo.GetAll());
        }

        // GET: Addresses/Details/5
        public  ActionResult Details(int? id)
        {
            if (id != null)
            {
                Address addr = addressRepo.GetDetails((int)id);
                if(addr==null)
                     return NotFound();

                return View(addr);
            }
            else return NotFound();
           
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(customerRepo.GetAll(), "CustomerId", "Fname");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create([Bind("AddressId,ApartmentNo,FloorNumber,Street,Zone,City,Governorate,Country,NearestLandmark,CustomerId")] Address address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (addressRepo.Insert(address) == 1)
                        return RedirectToAction(nameof(Index));
                    else return View(address); ;
                }
                catch
                {
                    ViewData["CustomerId"] = new SelectList(customerRepo.GetAll(), "CustomerId", "Fname", address.CustomerId);
                    return View(address);
                }

            }
            ViewData["CustomerId"] = new SelectList(customerRepo.GetAll(), "CustomerId", "Fname", address.CustomerId);
            return View(address);

        }
            // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addr = addressRepo.GetDetails((int)id);
            if (addr == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(customerRepo.GetAll(), "CustomerId", "Fname", addr.CustomerId);
            return View(addr);

        }

            // POST: Addresses/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    addressRepo.UpdateAddress(id, address);

                }
                catch (Exception e)
                {

                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(customerRepo.GetAll(), "CustomerId", "Fname", address.CustomerId);
            return View(address);

        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Address address = addressRepo.GetDetails((int)id);

            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  ActionResult DeleteConfirmed(int id)
        {
            if (addressRepo.DeleteAddress(id) == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

       
    }
}
