using Microsoft.AspNetCore.Mvc;

namespace C42G01Demo.PL.Controllers
{
	public class EmploeeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
