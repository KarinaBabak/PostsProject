using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PostModel = Posts.Model.Post;
using PostEntity = Posts.Entities.Entities.Post;

namespace Posts.WebAPI.App_Helpers.MappingProfile
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<PostModel, PostEntity>()
                .ForMember(x => x.Comments, config => config.Ignore());

            CreateMap<PostEntity, PostModel>()
                .ForMember(model => model.Id, map => map.MapFrom(e => e.Id))
                .ForMember(model => model.Content, map => map.MapFrom(e => e.Content))
                .ForMember(model => model.LastUpdatedDate, map => map.MapFrom(e => e.LastUpdatedDate));
        }
    }
}