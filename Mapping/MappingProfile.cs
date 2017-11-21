using AutoMapper;
using PagueVeloz.Controllers.Resources;
using PagueVeloz.Models;

namespace PagueVeloz.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<People, PeopleResource>();
            CreateMap<PeoplePhone, PeoplePhoneResource>();
            CreateMap<State, StateResource>();
        }
    }
}