using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using System.Diagnostics;
using OnlineStore.Models;
using OnlineStore.RepoServices;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public  IProductRepository ProductRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        
        public List<Product> products = new List<Product>();
        public HomeController(ILogger<HomeController> logger,IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            ProductRepository = productRepository;
            products = ProductRepository.GetAll();
            CategoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            if (products.Any())
            {
                List<Product> cheapest = products.OrderBy(x => x.Price).ToList();
                List<Product> mostExpensive = products.OrderByDescending(x => x.Sold).ToList();
                List<Product> recent = products.OrderByDescending(x => x.ReleaseDate).ToList();
                List<Product> mostPopular = products.OrderByDescending(x => x.Price).ToList();

                ViewBag.cheapestProducts = cheapest;
                ViewBag.mostExpensiveProducts = mostExpensive;
                ViewBag.recentProducts = recent;
                ViewBag.mostPopularProducts = mostPopular;

                ViewBag.categories = CategoryRepository.GetAll();
            }
            return View();
        }

        public IActionResult GetProductDetails(int id)
        {
            Product selectedProduct = products.Find(x => x.ProductID==id);
            return View(selectedProduct);
        }

            [HttpPost]
        public IActionResult Filter(int? catID , int? minPrice,int? maxPrice)
        {
            List<Product> filtered = new List<Product>();
            if (catID != null)
            {
                filtered = products.Where(x => x.Categories.Any(xx=>xx.CategoryId==catID)).ToList();
             }
            if(minPrice != null)
            {
                filtered = filtered.Where(x => x.Price >= minPrice).ToList();
            }
            if (maxPrice != null)
            {
                filtered = filtered.Where(x => x.Price <= maxPrice).ToList();
            }

            ViewBag.categories = CategoryRepository.GetAll();

            if (filtered.Count == 0)
                return View("HomeError");

            return View(filtered);
        }

        [HttpPost]
        public IActionResult SearchProducts(string productName)
        {
            List<Product> filtered = new List<Product>();
            if(productName != null)
            {
                filtered = products.Where(x => x.ProductName.ToLower().Contains(productName.ToLower())).ToList();
            }

            ViewBag.categories = CategoryRepository.GetAll();
            if (filtered.Count == 0)
                return View("HomeError");

            return View("Filter",filtered);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}