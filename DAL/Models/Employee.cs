﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
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
	public class Employee : ModelBase
	{


		public string Name { get; set; }

		public int? Age { get; set; }
		public string Address { get; set; }

		public decimal Salary { get; set; }
		public bool IsActive { get; set; }=true;

		public string Email { get; set; }
		public string PhoneNumber { get; set; }

		public DateTime HireDate { get; set; }= DateTime.Now;	

		public bool IsDeleted { get; set; }=false;
		public Gender Gender { get; set; }

		public Department department { get; init; }
		public int? departmentid { get; init; }

        public string ImageName { get; set; }

    }
}
