using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        //Use of Auto Mapper install the Same from nuget pacakge
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,MemberDto>()
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src=> 
             src.Photos.FirstOrDefault(x=> x.IsMain).Url))
             .ForMember(dest => dest.Age, opt => opt.MapFrom(
                 src => src.DateOfBirth.CalculateAge()
             ));
            CreateMap<Photo,PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
        }
    }
}