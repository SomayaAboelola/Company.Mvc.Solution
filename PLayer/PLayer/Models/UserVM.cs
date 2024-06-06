using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PLayer.Models
{
	public class UserVM
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		[ValidateNever]
		public IEnumerable<string?> Roles { get; set; }=new List<string>();


	}
}
