namespace OnlineStore.Models
{
    public class Report
    {
        public List<Product> Products { get; set; }
        public decimal TotalMoney { get; set; }
        public int TotalNumOfOrders { get; set; }
        public int TotalNumOfProducts { get; set;}
    }
}
