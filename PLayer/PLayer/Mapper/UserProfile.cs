using AutoMapper;
using DALayer.Entities;
using PLayer.Models;

namespace PLayer.Mapper
{
	public class UserProfile :Profile
	{
        public UserProfile()
        {
           CreateMap<AppUser ,UserVM>().ReverseMap();   
        }
    }
}
