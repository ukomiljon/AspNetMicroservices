using AutoMapper;
using EventBus.Messages;
using FeatureSwitch.API.Dto;
using FeatureSwitch.API.Models;

namespace FeatureSwitch.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FeatureRequest, Switch>().ReverseMap();
            CreateMap<FeatureRequest, SwitchFeatureEvent>().ReverseMap();            
        }
    }
}
