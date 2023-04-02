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
            List<Order> orders;
            if (startDate != null && endDate != null)
            {
                orders = Context.Orders
                    .Include(o => o.ProductOrders)
                    .ThenInclude(po=>po.Product)
                    .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                    .ToList();

            }
            else if(startDate != null)
            {
                orders = Context.Orders
                    .Include(o => o.ProductOrders)
                    .ThenInclude(po => po.Product)
                    .Where(o => o.OrderDate >= startDate)
                    .ToList();
            }
            else if (startDate != null)
            {
                orders = Context.Orders
                    .Include(o => o.ProductOrders)
                    .ThenInclude(po => po.Product)
                    .Where(o => o.OrderDate <= endDate)
                    .ToList();
            } else
            {
                orders = Context.Orders
                    .Include(o => o.ProductOrders)
                    .ThenInclude(po => po.Product)
                    .ToList();
            }

            Report report = new Report();
            
            report.TotalNumOfOrders = orders.Count();
            report.TotalNumOfProducts = orders.SelectMany(o => o.ProductOrders)
                .GroupBy(p=>p.Product)
                .Count();

            report.TotalQuantityOfProducts = orders.SelectMany(o => o.ProductOrders)
                .Sum(po => po.ProductQuantity)??0;
            report.TotalMoney = orders.Select(o => o.Bill).Sum();

            report.Products = orders.SelectMany(o => o.ProductOrders)
                .GroupBy(p => p.Product)
                .Select(g => new {
                    prod = g.Key,
                    quantity = g.Sum(p => p.ProductQuantity)
                })
                .OrderByDescending(p => p.quantity)
                .ToDictionary(p=>p.prod,p=>p.quantity??0);

            return report;
        }
    }
}
