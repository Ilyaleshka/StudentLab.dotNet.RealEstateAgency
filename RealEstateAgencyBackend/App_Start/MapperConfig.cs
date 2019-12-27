using AutoMapper;
using RealEstateAgencyBackend.BLL.DTO;
using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RealEstateAgencyBackend
{
    public static class MapperConfig
    {
        public static MapperConfiguration CreateConfiguration()
        {
            MapperConfiguration config = new MapperConfiguration(ConfigMapper);
            return config;
        }

        public static void ConfigMapper(IMapperConfigurationExpression config)
        {
            string str = ConfigurationManager.AppSettings["hostAddress"].ToString();


            config.CreateMap<RentalAnnouncementDto, RentalAnnouncement>();
            config.CreateMap<RentalAnnouncement, RentalAnnouncementDto>()
                .ForMember(r => r.Images,c => c.MapFrom(d => d.PostImages));
            config.CreateMap<RentalAnnouncementCreateModel, RentalAnnouncementDto>();
            config.CreateMap<RentalAnnouncementDto,RentalAnnouncementViewModel>();

            config.CreateMap<RentalRequestDto, RentalRequest>();
            config.CreateMap<RentalRequest, RentalRequestDto>();
            config.CreateMap<RentalRequestCreateViewModel, RentalRequestDto>();
            config.CreateMap<RentalRequestDto, RentalRequestViewModel>();

            config.CreateMap<User, UserDto>();
            config.CreateMap<UserDto, User>();
            config.CreateMap<UserCreateModel, CreateUserDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.LastName));
            config.CreateMap<UserDto, UserViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserLastName));

            config.CreateMap<PostImage, ImageDto>();
            config.CreateMap<ImageDto, PostImage>();

           
        }
    }
}