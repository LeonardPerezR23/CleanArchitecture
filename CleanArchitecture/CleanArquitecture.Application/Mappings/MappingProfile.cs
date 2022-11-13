using AutoMapper;
using CleanArchitecture.Domain;
using CleanArquitecture.Application.Features.Streamers.Commands;
using CleanArquitecture.Application.Features.Videos.Queries.GetVideosList;

namespace CleanArquitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Video, VideosVm>();
            CreateMap<CreateStreamerCommand, Streamer>();
        }
    }
}
