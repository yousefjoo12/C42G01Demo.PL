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
	public class DepartmentRepositories : IDepartmentRepository
	{
		private readonly AppDbContext _dbContext;
		public DepartmentRepositories(AppDbContext dbContext)
        {
			_dbContext = dbContext;
		}
        public int Add(Department department)
		{
			_dbContext.Departments.Add(department);
			return _dbContext.SaveChanges();
		}

		public int Delete(Department department)
		{
			_dbContext.Departments.Remove(department);
			return _dbContext.SaveChanges();
		}
		public int Update(Department department)
		{
			_dbContext.Departments.Update(department);
			return _dbContext.SaveChanges();
		}

		public IEnumerable<Department> GetAll()
		{
			return _dbContext.Departments.AsNoTracking().ToList();
		}

		public Department GetById(int id)
		{
			return _dbContext.Departments.Find(id);
			
		}

	}
}
