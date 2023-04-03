using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public enum Gender
    {
        Male, Female
    }
    public class ApplicationUser:IdentityUser
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please enter First Name "), MaxLength(40)]
        public string? FirstName { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please enter Last Name "), MaxLength(40)]
        public string? LastName { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }//= Models.Gender.Female;
        public byte[]? ProfilePicture { get; set; } 
    }
}
