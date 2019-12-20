using AutoMapper;
using RealEstateAgencyBackend.BLL.DTO;
using RealEstateAgencyBackend.Models;
using System;
using System.Collections.Generic;
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
            config.CreateMap<User, UserDto>();
            config.CreateMap<UserDto, User>();

            config.CreateMap<RentalAnnouncementDto, RentalAnnouncement>();
            config.CreateMap<RentalAnnouncement, RentalAnnouncementDto>();

            config.CreateMap<RentalAnnouncement, RentalAnnouncementDto>();
        }
    }
}