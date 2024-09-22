using AutoMapper;
using C42G01Demo.PL.ViewModels;
using DAL.Models;

namespace C42G01Demo.PL.Helpers
{
    public class mappingProfiles:Profile
    {
        public mappingProfiles()
        {
            CreateMap<EmployeeViewModels, Employee>().ReverseMap();
        }
    }
}
