using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;
using OnlineStore.RepoServices;
using System.Diagnostics;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public ICustomerRepository customerRepo { get; }


        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,
           
            SignInManager<ApplicationUser> signInManager, ICustomerRepository custrepo)
        {
            _logger = logger;
            _userManager = userManager;
            customerRepo = custrepo;
            _signInManager = signInManager; 
        }

        public IActionResult Index(int id)
        {
            if(_signInManager.IsSignedIn(User))
            {
                var customer = customerRepo.GetAll().Where(e => e.Email == _userManager.GetUserAsync(User).Result.Email).FirstOrDefault();
                id = customer.CustomerId;
            }
            
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