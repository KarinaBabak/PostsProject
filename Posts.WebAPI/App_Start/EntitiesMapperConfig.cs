using AutoMapper;
using Posts.WebAPI.App_Helpers.MappingProfile;

namespace Posts.WebAPI.App_Start
{
    public static class EntitiesMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new PostMappingProfile());
            });
        }
    }
}