using System.ComponentModel.DataAnnotations;

namespace MyVillageApp.Models.Account
{
    public class ForgotPasswordPhoneModel
    {
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter valid 10 digit phone number")]
        public string PhoneNumber { get; set; }
    }
}