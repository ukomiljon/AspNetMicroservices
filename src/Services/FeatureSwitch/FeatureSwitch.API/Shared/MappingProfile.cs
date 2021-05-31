using AutoMapper;
using FeatureSwitch.API.Dto;
using FeatureSwitch.API.Models;

namespace FeatureSwitch.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FeatureRequest, Switch>();
            CreateMap<Switch, FeatureRequest>();
        }
    }
}
