using System.ComponentModel.DataAnnotations;

namespace PLayer.Models
{
	public class RegisterVM 
	{
		public string  FirstName { get; set; }
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "INValid")]

		public string Email { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "ConfirmPassword is Required")]
		[DataType(DataType.Password)]
		[Compare("Password" ,ErrorMessage = "ConfirmPassword not match with Password")]
		public string ConfirmPassword { get; set; }	
		public bool Agree { get; set; }


	} 
}
