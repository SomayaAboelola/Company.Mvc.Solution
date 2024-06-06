using System.ComponentModel.DataAnnotations;

namespace PLayer.Models
{
    public class ForgetPasswordVM
    {
        [EmailAddress(ErrorMessage = "INValid")]
        public string Email { get; set; }


    }
}
