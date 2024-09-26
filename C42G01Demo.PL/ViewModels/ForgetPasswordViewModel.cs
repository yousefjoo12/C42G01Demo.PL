using System.ComponentModel.DataAnnotations;

namespace C42G01Demo.PL.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "IsValid Email")]
		public string Email { get; set; }
	}
}
