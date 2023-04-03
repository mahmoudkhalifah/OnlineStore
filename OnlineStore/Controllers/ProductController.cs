using EllipticCurve.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Models;
using OnlineStore.RepoServices;
using System.Data;
using System.Drawing;
using System.Web.Helpers;
using System.Web.WebPages.Html;

namespace OnlineStore.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        public IProductRepository ProductRepository { get; set; }
        public ICategoryRepository categoryRepository { get; set; }
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.ProductRepository = productRepository;
            this.categoryRepository = categoryRepository;
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewData["Categories"] = new MultiSelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Product product,IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (product.image1 != null)
                    {
                        product.image1.CopyTo(memoryStream);

                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            product.Image1 = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }
                    memoryStream.SetLength(0);

                    if (product.image2 != null)
                    {
                        product.image2.CopyTo(memoryStream);

                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            product.Image2 = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }
                    memoryStream.SetLength(0);
                    if (product.image3 != null)
                    {
                        product.image3.CopyTo(memoryStream);

                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            product.Image3 = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }

                }
                for (int i = 0; i < collection["Categories"].Count(); i++)
                {
                    Category cat = categoryRepository.GetDetails(int.Parse(collection["Categories"][i]));
                    product.Categories.Add(cat);
                }
                ProductRepository.Insert(product);
                return RedirectToAction("Index");
            }
            else return View();

        }

        // GET: ProductController/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
           
            Product product = ProductRepository.GetDetails(id);
            ViewData["Categories"] = new MultiSelectList(categoryRepository.GetAll(), "CategoryId", "CategoryName",product.Categories.Select(x=>x.CategoryId).ToList());
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Product product, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (product.image1 != null)
                    {
                        product.image1.CopyTo(memoryStream);

                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            product.Image1 = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }
                    memoryStream.SetLength(0);

                    if (product.image2 != null)
                    {
                        product.image2.CopyTo(memoryStream);

                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            product.Image2 = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }
                    memoryStream.SetLength(0);
                    if (product.image3 != null)
                    {
                        product.image3.CopyTo(memoryStream);

                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            product.Image3= memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }

                }
                for (int i = 0; i < collection["Categories"].Count();i++)
                {
                    Category cat = categoryRepository.GetDetails(int.Parse(collection["Categories"][i]));
                    product.Categories.Add(cat);
                }
                ProductRepository.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            else return View();
        }

        // GET: ProductController/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
