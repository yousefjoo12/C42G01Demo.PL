using DAL.Data.Migrations;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
	public class AppDbContext : IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlServer("Server = . ; Database =C42G01MVC ; Trusted_Connection = True; MultipleActiveResultSets= True;");
		//}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Fluent APIS
			modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<IdentityRole>()
				.ToTable("Roles");
			modelBuilder.Entity<IdentityUser>()
				.ToTable("Users");

		}
		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }

	}
}
