using DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;
using Microsoft.AspNetCore.Http;

namespace C42G01Demo.PL.ViewModels
{

	public enum Gender
	{
		[EnumMember(Value = "Male")]
		Male = 1,
		[EnumMember(Value = "Female")]
		Female = 2
	}
	public enum EmployeeType
	{
		FullTime = 1,
		PartTime = 2
	}
	public class EmployeeViewModels
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name Is Required!")]
		[MaxLength(50, ErrorMessage = "Max Length for Name is 50")]
		[MinLength(4, ErrorMessage = "Min Length for Name is 4")]
		public string Name { get; set; }
		[Range(21, 60)]
		public int? Age { get; set; }
		[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
			ErrorMessage = "Address Must be like 123-Street-City-Country")]
		public string Address { get; set; }
		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }
		public bool IsActive { get; set; }//soft delete

		[EmailAddress]
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		[Display(Name = "Hire Date")]
		public DateTime HireDate { get; set; }

		public bool IsDeleted { get; set; }//soft delete
		public Gender Gender { get; set; }

		//Navigation property [one] 
		//[InverseProperty(nameof(Models.Department.Employees))]
		public Department department { get; init; }
		public int? departmentid { get; init; }
		public IFormFile Image { get; init; }
		public string ImageName { get; init; }

	}
}

