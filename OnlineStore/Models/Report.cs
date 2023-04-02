namespace OnlineStore.Models
{
    public class Report
    {
        public Dictionary<Product,int> Products { get; set; }
        public decimal TotalMoney { get; set; }
        public int TotalNumOfOrders { get; set; }
        public int TotalNumOfProducts { get; set;}
        public int TotalQuantityOfProducts { get; set; }
    }
}
