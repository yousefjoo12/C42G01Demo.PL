using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
	public class EmploeeRepository : GenericRepository<Employee>, IEmploeeRepository
	{
	
		public EmploeeRepository(AppDbContext dbContext):base(dbContext)
		{
			
		}

		public IQueryable<Employee> GetEmployeesByAddress(string address)
		{
			return _dbContext.Employees.Where(E => E.Address.ToLower().Contains(address.ToLower()));
		}
	}
}
