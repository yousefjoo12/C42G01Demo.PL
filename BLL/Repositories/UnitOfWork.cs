using BLL.Interfaces;
using DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
           _dbContext = dbContext;
            EmploeeRepository = new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepositories(_dbContext);
        }
        public IEmploeeRepository EmploeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public int Complete()
        {
           return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
