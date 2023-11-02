using AutoMapper;
using TegEventsApi.Entities;
using TegEventsApi.Models;

namespace TegEventsApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventReadModel>();
            CreateMap<Venue, VenueReadModel>();
        }
    }
}
