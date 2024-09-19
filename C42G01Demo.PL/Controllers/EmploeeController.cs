﻿using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using BLL.Repositories;
using DAL.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System;
using C42G01Demo.PL.ViewModels;
using AutoMapper;
namespace C42G01Demo.PL.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IEmploeeRepository _repository;
		private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmploeeRepository repository, IWebHostEnvironment env,IMapper mapper/*,IDepartmentRepository departmentRepository*/)
		{
			_repository = repository;
			_env = env;
            _mapper = mapper;
            //_departmentRepository = departmentRepository;
        }

		public IActionResult Index(string searchinp)
		{


			if (string.IsNullOrEmpty(searchinp))
			{
				var emploee = _repository.GetAll();
				return View(emploee);

			}
			else
			{
				var emploee = _repository.GetEmployeesByName(searchinp.ToLower());
				return View(emploee);


			}
			////1-ViewData
			//ViewData["Massage"] = "Hello ViewData";
			////2-ViewBag
			//ViewBag.Massage = "Hello ViewBag";

		}
		public IActionResult Create()
		{

			return View();
		}
		[HttpPost]
		public IActionResult Create(EmployeeViewModels EmployeesVM)
		{
			if (ModelState.IsValid)
			{
				var mappedEmp = _mapper.Map<EmployeeViewModels, Employee>(EmployeesVM);

				var count = _repository.Add(mappedEmp);
				if (count > 0)
				{
					return RedirectToAction("Index");
				}

			}
			return View(EmployeesVM);

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
