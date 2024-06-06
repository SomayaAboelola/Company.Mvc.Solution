using AutoMapper;
using DALayer.Entities;
using PLayer.Models;

namespace PLayer.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeVM, Employee>().ReverseMap();
        }
    }
   
}
