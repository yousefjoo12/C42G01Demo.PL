﻿using AutoMapper;
using C42G01Demo.PL.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C42G01Demo.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var user = await _userManager.Users.Select(U => new UserViewModels
                {
                    Id = U.Id,
                    FName = U.FName,
                    LName = U.LName,
                    PhoneNumbers = U.PhoneNumber,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result,

                }).ToListAsync();
                return View(user);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    var mappeduser = new UserViewModels
                    {
                        Id = user.Id,
                        FName = user.FName,
                        LName = user.LName,
                        PhoneNumbers = user.PhoneNumber,
                        Email = user.Email,
                        Roles = _userManager.GetRolesAsync(user).Result
                    };
                return View(new List<UserViewModels> { mappeduser }); 
                }
            }
            return View(Enumerable.Empty<UserViewModels>());
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id ,string viewName = "Details")
        {
            if (id is null)
            {
                BadRequest();
            }
            var User =await _userManager.FindByIdAsync(id);
            if (User is null) 
            {
                return NotFound();
            }
            var mapperUser = _mapper.Map<ApplicationUser, UserViewModels>(User);
           return View(viewName,mapperUser);
        }
    }
}
