using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.ViewModels;

namespace OnlineStore.Controllers
{
    [Authorize(Roles ="Admin")]//keda role mn8er roles keda esmha policey
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager; 
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", await _roleManager.Roles.ToListAsync());

            if (await _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "Role is exists!");
                //def error 3la elfeild ely esmo name eno exists
                return View("Index", await _roleManager.Roles.ToListAsync());
            }

            await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));

            return RedirectToAction(nameof(Index));
        }
    }
}
