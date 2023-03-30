using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.RepoServices;
using System.Data;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        public IProductRepository ProductRepository { get; set; }
        public ProductController(IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            return View(ProductRepository.GetAll());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(ProductRepository.GetDetails(id));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            ProductRepository.Insert(product);
            return RedirectToAction("Index");
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = ProductRepository.GetDetails(id);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            ProductRepository.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Product product = ProductRepository.GetDetails(id);
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Product product = ProductRepository.GetDetails(id);
                ProductRepository.DeleteProduct(id);
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
