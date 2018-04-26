using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CommentModel = Posts.Model.Comment;
using CommentEntity = Posts.Entities.Entities.Comment;

namespace Posts.WebAPI.App_Helpers.MappingProfile
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
    {
        CreateMap<CommentEntity, CommentModel>()
            .ForMember(model => model.Id, map => map.MapFrom(e => e.Id))
            .ForMember(model => model.Content, map => map.MapFrom(e => e.Content))
            .ForMember(model => model.LastUpdatedDate, map => map.MapFrom(e => e.LastUpdatedDate))
            .ForMember(model => model.PostId, map => map.MapFrom(e => e.PostId))
            .ForMember(model => model.UserLogin, map => map.MapFrom(e => e.UserLogin));
    }
}
}