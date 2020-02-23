using System.Linq;
using AutoMapper;
using PortalExample.API.Dtos;
using PortalExample.API.Models;

namespace PortalExample.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>().
            ForMember(dest => dest.PhotoUrl, opt =>
            {
                opt.MapFrom(src => src.Photo.FirstOrDefault(p => p.IsMain).Url);
            }).
             ForMember(dest => dest.Age, opt =>
             {
                 opt.ResolveUsing(src => src.DateOfBirth.CalculateAge());
             })
            ;

            CreateMap<User, UserForDetailedDto>().
              ForMember(dest => dest.PhotoUrl, opt =>
              {
                  opt.MapFrom(src => src.Photo.FirstOrDefault(p => p.IsMain).Url);
              }).ForMember(dest => dest.Age, opt =>
              {
                  opt.ResolveUsing(src => src.DateOfBirth.CalculateAge());
              });

            CreateMap<Photo, PhotoForDetailedDto>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();

            CreateMap<UserForUpdateDto, User>();

        }
    }
}