using System.ComponentModel.DataAnnotations;

namespace PLayer.Models
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "ConfirmPassword not match with Password")]
        public string ConfirmPassword { get; set; }
    }
}
