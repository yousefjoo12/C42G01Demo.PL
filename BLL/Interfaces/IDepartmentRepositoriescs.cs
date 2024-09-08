using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	internal interface IDepartmentRepositoriescs
	{
		IEnumerable<Department> GetAll();
		Department GetById(int id);
		int Add(Department department);
		int Update(Department department);
		int Delete(Department department);
	}
}
