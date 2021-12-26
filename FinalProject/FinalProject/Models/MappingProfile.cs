using AutoMapper;
using FinalProject.Entities;
using FinalProject.ViewModels;

namespace FinalProject.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductVM, Product>();
            //Thêm method .ReverseMap() nếu map 2 chiều

            CreateMap<RegisterVM, UserInfo>();
        }
    }
}
