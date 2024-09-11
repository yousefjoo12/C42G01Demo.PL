using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
	public class EmploeeRepository : IEmploeeRepository
	{
		private readonly AppDbContext _dbContext;
		public EmploeeRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public int Add(Employee employee)
		{
			_dbContext.Employees.Add(employee);
			return _dbContext.SaveChanges();
		}

		public int Delete(Employee employee)
		{
			_dbContext.Employees.Remove(employee);
			return _dbContext.SaveChanges();
		}

		public IEnumerable<Employee> GetAll()
		{
			return _dbContext.Employees.AsNoTracking().ToList();
		}

		public Employee GetById(int id)
		{
			return _dbContext.Employees.Find(id);
		}

		public int Update(Employee employee)
		{
			_dbContext.Employees.Update(employee);
			return _dbContext.SaveChanges();
		}
	}
}
