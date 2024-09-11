using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IGenericRepository<T> where T : ModelBase
	{
		IEnumerable<T> GetAll();
		T GetById(int id);
		int Add(T Item);
		int Update(T Item);
		int Delete(T Item);
	}
}
