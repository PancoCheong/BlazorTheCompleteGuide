// Business.Mapper.MappingProfile.cs
using AutoMapper;
using DataAccess.Data;
using Models;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>(); //map by same name 
            CreateMap<HotelRoom, HotelRoomDTO>(); //map by same name 

            //CreateMap<Employee, EmployeeDto>()
            //    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
