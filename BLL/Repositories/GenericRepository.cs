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
        public void Add(T Item)
        {
            //_dbContext.Set<T>().Add(Item);
            _dbContext.Add(Item); 
        }

        public void Delete(T Item)
        {
            _dbContext.Set<T>().Remove(Item); 
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _dbContext.Employees.Include(E=>E.department).AsNoTracking().ToList(); 
            }
            else
            {
                return _dbContext.Set<T>().AsNoTracking().ToList();

            }
        }

        public T GetById(int id)
        {
            return _dbContext.Find<T>(id);
        }

        public void Update(T Item)
        {
            _dbContext.Set<T>().Update(Item); 
        }
    }
}
