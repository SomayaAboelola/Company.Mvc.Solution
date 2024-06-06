using System.ComponentModel.DataAnnotations;

namespace PLayer.Models
{
	public class SignInVM
	{
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "INValid")]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		public bool Rememberme { get; set; }	
	}
}
