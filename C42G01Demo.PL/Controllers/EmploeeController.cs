using BLL.Interfaces;
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
using System.Collections;
using System.Collections.Generic;
namespace C42G01Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IUnitOfWork UnitOfWork, IWebHostEnvironment env, IMapper mapper/*,IDepartmentRepository departmentRepository*/)
        {
            _unitOfWork = UnitOfWork;
            _env = env;
            _mapper = mapper;
            //_departmentRepository = departmentRepository;
        }

        public IActionResult Index(string searchinp)
        {


            if (string.IsNullOrEmpty(searchinp))
            {
                var emploee = _unitOfWork.EmploeeRepository.GetAll();
                return View(emploee);
			if (string.IsNullOrEmpty(searchinp))
			{
				var emploee = _repository.GetAll();
				var mappedemp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModels>>(emploee);
				return View(mappedemp);

            }
            else
            {
                var emploee = _unitOfWork.EmploeeRepository.GetEmployeesByName(searchinp.ToLower());
                return View(emploee);
			}
			else
			{
				var emploee = _repository.GetEmployeesByName(searchinp.ToLower());
                var mappedemp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModels>>(emploee);

                return View(mappedemp);

            }
            ////1-ViewData
            //ViewData["Massage"] = "Hello ViewData";
            ////2-ViewBag
            //ViewBag.Massage = "Hello ViewBag";

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

                _unitOfWork.EmploeeRepository.Add(mappedEmp);
                var count = _unitOfWork.Complete();
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
            var employee = _unitOfWork.EmploeeRepository.GetById(id.Value);
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModels>(employee);

            if (employee == null)
            {
                return NotFound();
            }
            return View(viewName, mappedEmp);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var employee = _unitOfWork.EmploeeRepository.GetById(id.Value);


            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModels EmployeesVM)
        {
            if (!ModelState.IsValid)
            {
                return View(EmployeesVM);
            }
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModels, Employee>(EmployeesVM);

                _unitOfWork.EmploeeRepository.Update(mappedEmp);
                _unitOfWork.Complete();
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
            return View(EmployeesVM);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeViewModels EmployeeVm)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModels, Employee>(EmployeeVm);

                _unitOfWork.EmploeeRepository.Delete(mappedEmp);
                _unitOfWork.Complete();
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
                return View(EmployeeVm);
            }
        }
    }
			if (employee == null)
			{
				return NotFound();
			}
			return View(employee);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([FromRoute] int? id, EmployeeViewModels EmployeesVM)
		{
			if (id != EmployeesVM.Id)
			{
				return BadRequest();
			}
			if (!ModelState.IsValid)
			{
				return View(EmployeesVM);
			}
			try
			{
                var mappedEmp = _mapper.Map<EmployeeViewModels, Employee>(EmployeesVM);

                _repository.Update(mappedEmp);
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
			return View(EmployeesVM);
		}
		[HttpGet]
		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(EmployeeViewModels EmployeesVM)
		{
			try
			{
                var mappedEmp = _mapper.Map<EmployeeViewModels, Employee>(EmployeesVM);

                _repository.Delete(mappedEmp);
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
				return View(EmployeesVM);
			}
		}
	}
}
