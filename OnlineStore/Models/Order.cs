using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Order
    {
        [Required,Key]
        public int OrderId { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } =DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime? ShippingDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ArrivalDate { get; set; }
        [Required,Range(0, 9999999999999999.99)]
        public decimal Bill { get; set; }
        [Required]
        public OrderState OrderState { get; set; } = OrderState.Processing;
        public PayMethod PaymentMethod{ get; set; } = PayMethod.Cash;

        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public virtual Address? Address { get; set; }

        [ForeignKey("Customer")]
        public  int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }


        //foregin key (Many to many)
        public virtual List<ProductOrders> ProductOrders { get; set; } = new List<ProductOrders>();


    }
}
