using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public class ReportsRepository : IReportsRepository
    {
        MainDBContext Context;
        public ReportsRepository(MainDBContext mainDBContext)
        {
            Context = mainDBContext;
        }
        
        public Report GetReport(DateTime? startDate,DateTime? endDate)
        {
            /*List<Order> orders;
            if (startDate != null && endDate != null)
            {
                orders = Context.Orders
                    .Include(o => o.Products)
                    .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                    .ToList();
            }
            else if(startDate != null)
            {
                orders = Context.Orders
                    .Include(o => o.Products)
                    .Where(o => o.OrderDate >= startDate)
                    .ToList();
            }
            else if (startDate != null)
            {
                orders = Context.Orders
                    .Include(o => o.Products)
                    .Where(o => o.OrderDate <= endDate)
                    .ToList();
            } else
            {
                orders = Context.Orders
                    .Include(o => o.Products)
                    .ToList();
            }

            Report report = new Report();
            
            report.TotalNumOfOrders = orders.Count();
            report.TotalNumOfProducts = orders.Select(o => o.Products.Count()).Sum();
            report.TotalMoney = orders.Select(o => o.Bill).Sum();
            /*report.Products = orders.SelectMany(o => o.Products).Distinct()
                .OrderBy(p => p.)
                .Take(5)
                .ToList();*/

            //return report;
            return new Report();

        }
    }
}
