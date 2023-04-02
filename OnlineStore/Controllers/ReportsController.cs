using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.RepoServices;
using System.Data;

namespace OnlineStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        IReportsRepository ReportsRepository;
        public ReportsController(IReportsRepository reportsRepository)
        {
            ReportsRepository = reportsRepository;
        }
        // GET: ReportsController1
        public ActionResult Index()
        {
            return View(ReportsRepository.GetReport(null,null));
        }
        [HttpPost]
        public ActionResult Index(DateTime? startDate,DateTime? endDate)
        {
            /*ViewBag.startDate = startDate?.ToString("dd mm yyyy");
            ViewBag.endDate = endDate?.ToString("dd mm yyyy");*/
            ViewBag.startDate = startDate;
            ViewBag.endDate = endDate;
            return View(ReportsRepository.GetReport(startDate, endDate));
        }
    }
}
