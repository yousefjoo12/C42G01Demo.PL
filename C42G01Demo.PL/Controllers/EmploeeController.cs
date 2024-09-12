using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using BLL.Repositories;
using DAL.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System;
namespace C42G01Demo.PL.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IEmploeeRepository _repository;
		private readonly IWebHostEnvironment _env;

		public EmployeeController(IEmploeeRepository repository, IWebHostEnvironment env)
		{
			_repository = repository;
			_env = env;
		}

		public IActionResult Index()
		{
			var emploee = _repository.GetAll();
            //1-ViewData
            ViewData["Massage"] = "Hello ViewData";
            //2-ViewBag
            ViewBag.Massage = "Hello ViewBag";

            return View(emploee);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Employee employees)
		{
			if (ModelState.IsValid)
			{
				var count = _repository.Add(employees);
				if (count > 0)
				{
					return RedirectToAction("Index");
				}

			}
			return View(employees);

		}
		[HttpGet]
		public IActionResult Details(int? id, string viewName = "Details")
		{
			if (!id.HasValue)
			{
				return BadRequest();
			}
			var employee = _repository.GetById(id.Value);
			if (employee == null)
			{
				return NotFound();
			}
			return View(viewName, employee);
		}
		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (!id.HasValue)
			{
				return BadRequest();
			}
			var employee = _repository.GetById(id.Value);
			if (employee == null)
			{
				return NotFound();
			}
			return View(employee);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([FromRoute] int? id, Employee employee)
		{
			if (!ModelState.IsValid)
			{
				return View(employee);
			}
			try
			{
				_repository.Update(employee);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
				else
				{
					ModelState.AddModelError(string.Empty, "An Error occured during update employee");
				}
			}
			return View(employee);
		}
		[HttpGet]
		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Employee employee)
		{
			try
			{
				_repository.Delete(employee);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
				else
				{
					ModelState.AddModelError(string.Empty, "An Error occured during Delete employee");
				}
				return View(employee);
			}
		}
	}
}
