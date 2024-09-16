﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IEmploeeRepository : IGenericRepository<Employee>
	{
		IQueryable<Employee> GetEmployeesByAddress(string address);
		IQueryable<Employee> GetEmployeesByName(string name);

	}
}
