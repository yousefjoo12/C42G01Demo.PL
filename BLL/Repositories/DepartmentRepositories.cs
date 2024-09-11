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
	public class DepartmentRepositories : GenericRepository<Department>,IDepartmentRepository
	{
		

		public DepartmentRepositories(AppDbContext dbContext):base(dbContext) 
		{
			
		}
		
	}
}
