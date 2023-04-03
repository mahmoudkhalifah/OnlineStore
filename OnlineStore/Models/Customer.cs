using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Customer
    {
        [Required,Key]
        public int CustomerId { get; set; }
        [Required, StringLength(50, ErrorMessage = "First name should not exceed 50 letters")]
        public string Fname { get; set; }
        [Required, StringLength(50, ErrorMessage = "Last name should not exceed 50 letters")]
        public string Lname { get; set; }

        [NotMapped]
        public string FullName { get => Fname + " " + Lname; }

        //[Required]
        public Nullable<bool> Gender { get; set; }       
        [Required,MinLength(11, ErrorMessage ="Please enter a valid phone number of 11 numbers")]
        public string PhoneNumber { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

  
        [EmailAddress]
        public string? Email { get; set; }
        public virtual Cart Cart { get; set; }

        //foregin key (Many to many)
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public virtual List<Address> Addresses { get; set; }= new List<Address>();
    }
    
}
