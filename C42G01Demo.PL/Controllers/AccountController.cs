using C42G01Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace C42G01Demo.PL.Controllers
{
	public class AccountController : Controller
	{
		#region Sign Up
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public IActionResult SignUp(SignUpViewModel ViewModel)
		{
			if (ModelState.IsValid) 
			{
				var user = new IdentityUser()
				{
					UserName = ViewModel.Email.Split("@")[0],
					Email = ViewModel.Email,

				};
			}
			return View(ViewModel);
		}
		#endregion
	}
}
