using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.RepoServices;

namespace OnlineStore.Controllers
{
    public class CategoriesController : Controller
    {
        public ICategoryRepository CategoryRepository { get; }
        public CategoriesController(ICategoryRepository CategoryRepository) 
        { 
            this.CategoryRepository = CategoryRepository;
        }
        // GET: CategoriesController
        public ActionResult Index()
        {
            return View(CategoryRepository.GetAll());
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View(CategoryRepository.GetDetails(id));
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                CategoryRepository.Insert(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(CategoryRepository.GetDetails(id));
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                CategoryRepository.UpdateCategory(id, category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(CategoryRepository.GetDetails(id));
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                CategoryRepository.DeleteCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
