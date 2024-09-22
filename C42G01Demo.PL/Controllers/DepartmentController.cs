using BLL.Interfaces;
using BLL.Repositories;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System;

namespace C42G01Demo.PL.Controllers
{
    public class DepartmentController : Controller
    { 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        //private readonly IDepartmentRepository _DepartmentRepository;//NULL
        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {  
           _unitOfWork = unitOfWork;
            _env = env;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(department);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(viewName, department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            try
            {
                _unitOfWork.DepartmentRepository.Update(department);
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
                    ModelState.AddModelError(string.Empty, "An Error occured during update department");
                }
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id,"Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
		public IActionResult Delete(Department department)
        {
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
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
					ModelState.AddModelError(string.Empty, "An Error occured during Delete department");
				}
                return View(department);
			}
        }

	}
}