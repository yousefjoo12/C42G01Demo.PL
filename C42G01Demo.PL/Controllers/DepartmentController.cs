using BLL.Interfaces;
using BLL.Repositories;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;

namespace C42G01Demo.PL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository _DepartmentRepository;

		//private readonly IDepartmentRepository _DepartmentRepository;//NULL
		public DepartmentController(IDepartmentRepository repository)
        {

			//_DepartmentRepository = repository;
			_DepartmentRepository = repository;
		}
        public IActionResult Index()
		{
			_DepartmentRepository.GetAll();	
			return View();
		}
	}
}
