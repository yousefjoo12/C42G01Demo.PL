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
using Microsoft.CodeAnalysis;
using C42G01Demo.PL.Helpers;
using System.Collections.Generic;
namespace C42G01Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IUnitOfWork UnitOfWork, IWebHostEnvironment env, IMapper mapper)
        {
            _unitOfWork = UnitOfWork;
            _env = env;
            _mapper = mapper;
        }

        public IActionResult Index(string searchInpt)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrWhiteSpace(searchInpt))
            {
                employees = _unitOfWork.EmploeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmploeeRepository.GetEmployeesByName(searchInpt.ToLower());

            }
            var MappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModels>>(employees);
            return View(MappedEmployees);
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

                mappedEmp.ImageName = DocumentSettings.UplodeFile(EmployeesVM.Image, "Images");
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
                //  EmployeesVM.ImageName = DocumentSettings.UplodeFile(EmployeesVM.Image, "Images");
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
                DocumentSettings.DeleteFile(EmployeeVm.ImageName, "Images");
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
}
