using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Product
    {
        [Key]
        public int ProductID { set; get; }
        [Required]
        [StringLength(50,ErrorMessage ="Product name should not exceed 50 letters")]
        public string ProductName { set; get; }

        [Required,Range(0, 9999999999999999.99)]
        public decimal Price { set; get; }

        [Required]
        public Colors Color { set; get; }//Check

        [ Range(0, 999999999999999.999)]
        public decimal? Width { set; get; }

        [Range(0, 999999999999999.999)]
        public decimal? Height { set; get; }

        [Required]
        [Range(0,100)]
        public int Discount { set; get; }

        [StringLength(100, ErrorMessage = "Description should not exceed 100 letters")]
        public string? Description { set; get; }

        [Required]
        [Range(0, int.MaxValue)]
        public int AvailableQuantity { set; get; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Sold { set; get; }

        [Required]
        public bool IsDeleted { set; get; } = false;

        //TODO:: if no image is uploaded,view default image
        //TODO::add restrictions on image sizes
        public byte[]? Image1 { set; get; }
        public byte[]? Image2 { set; get; }
        public byte[]? Image3 { set; get; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { set; get; } = DateTime.Now;
        //byte[] imgdata = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(path));

        //foregin key (Many to many)
        public virtual List<Category> Categories { get; set;} = new List<Category>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public virtual List<ProductCart> ProductsCarts { get; set; } = new List<ProductCart>();


        public string StringFromByteArray(byte[] bytes)
        {
            string url = "";
            if (bytes != null && bytes.Length > 0)
            {
                string img = Convert.ToBase64String(bytes, 0, bytes.Length);
                url = "data:image/jpeg;base64," + img;
            }
            return url;
        }
    }
}
