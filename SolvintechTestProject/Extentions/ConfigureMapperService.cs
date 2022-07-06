using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Entities.Shared;
using Solvintech.Entities;

namespace SolvintechTestProject.Extentions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<User, UserForRegistrationDto>().ReverseMap();
        }
    }
    public static class ConfigureMapperService
    {
        public static void ConfigureMapper(this IServiceCollection serviceCollection)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);

        }
    }
}