using OnlineStore.Models;

namespace OnlineStore.RepoServices
{
    public interface IReportsRepository
    {
        /*public List<Product> GetMostSoldProducts(DateTime start,DateTime end);
        public int GetNumberOfOrders(DateTime start, DateTime end);
        public int GetNumberOfProducts(DateTime start, DateTime end);
        public float GetTotalAmountOfMoney(DateTime start, DateTime end);*/

        public Report GetReport(DateTime? startDate,DateTime? endDate);
    }
}
