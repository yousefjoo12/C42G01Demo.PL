using C42G01Demo.PL.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace C42G01Demo.PL.Controllers
{

	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region Sign Up
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel ViewModel)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser()
				{
					UserName = ViewModel.Email.Split("@")[0],
					Email = ViewModel.Email,
					IsAgree = ViewModel.IsAgree,
					FName = ViewModel.FName,
					LName = ViewModel.LName,
				};
				var Result = await _userManager.CreateAsync(user, ViewModel.Password);
				if (Result.Succeeded)
				{
					return RedirectToAction(nameof(SignIn));
				}
				foreach (var Error in Result.Errors)
				{
					ModelState.AddModelError(string.Empty, Error.Description);
				}
			}
			return View(ViewModel);
		}

		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		//[Authorize]
		public async Task<IActionResult> SignIn(SignInViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(viewModel.Email);
				if (user is not null)
				{
					bool flag =await _userManager.CheckPasswordAsync(user, viewModel.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false);
						if (result.Succeeded)
						{
							return RedirectToAction(nameof(HomeController.Index),"Home");
						}

					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Login");
			}
			return View(viewModel);	
		}

		public async Task<IActionResult> SignOut() 
		{
			await _signInManager.SignOutAsync();
			return View(nameof(SignIn));
		}
		#endregion
	}
}
