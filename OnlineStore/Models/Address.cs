using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Address
    {
        [Required,Key]
        public int AddressId { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Reached maximum number of characters (50)")]
        public string ApartmentNo { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Reached maximum number of characters (50)")]
        public string FloorNumber { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Reached maximum number of characters (50)")]
        public string Street { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Reached maximum number of characters (50)")]
        public string Zone { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Reached maximum number of characters (50)")]
        public string City { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Reached maximum number of characters (50)")]
        public string Governorate { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Reached maximum number of characters (50)")]
        public string Country { get; set; }
        [MaxLength(100,ErrorMessage ="Reached maximum number of characters (100)")]
        public string? NearestLandmark { get; set; }

        [ForeignKey("Customer")]
        public  int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
