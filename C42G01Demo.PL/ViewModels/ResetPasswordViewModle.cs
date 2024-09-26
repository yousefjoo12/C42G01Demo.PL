using System.ComponentModel.DataAnnotations;

namespace C42G01Demo.PL.ViewModels
{
	public class ResetPasswordViewModle
	{
		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password Is Required")]
		[Compare(nameof(Password), ErrorMessage = "Confirm Password dose't match Password")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
	}
}
