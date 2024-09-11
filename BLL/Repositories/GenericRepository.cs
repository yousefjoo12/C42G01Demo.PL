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
	public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
	{
		private protected readonly AppDbContext _dbContext;

		public GenericRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public int Add(T Item)
		{
			//_dbContext.Set<T>().Add(Item);
			_dbContext.Add(Item);
			return _dbContext.SaveChanges();
		}

		public int Delete(T Item)
		{
			_dbContext.Set<T>().Remove(Item);
			return _dbContext.SaveChanges();
		}

		public IEnumerable<T> GetAll()
		{
			return _dbContext.Set<T>().AsNoTracking().ToList();
		}

		public T GetById(int id)
		{
			return _dbContext.Find<T>(id);
		}

		public int Update(T Item)
		{
			_dbContext.Set<T>().Update(Item);
			return _dbContext.SaveChanges();
		}
	}
}
