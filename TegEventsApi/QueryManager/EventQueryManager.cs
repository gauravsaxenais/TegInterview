using AutoMapper;
using TegEvents.Framework;
using TegEventsApi.Entities;
using TegEventsApi.Models;

namespace TegEventsApi.QueryManager
{
    public class EventQueryManager : QueryManager<Event, EventReadModel>, IEventQueryManager
    {
        public EventQueryManager(ILogger<QueryManager<Event, EventReadModel>> logger, IMapper mapper) : base(logger, mapper)
        {
        }

        public async Task<IEnumerable<EventReadModel>> GetEventsByStartDate(Func<Task<Root>> getlistOfEvents, DateTimeOffset startDate)
        {
            return await GetByPredicateAsync(async () => await GetEvents(getlistOfEvents), x => x.StartDate == startDate);
        }

        public async Task<IEnumerable<EventReadModel>> GetEventsByVenueIdAsync(Func<Task<Root>> getlistOfEvents, int venueId)
        {
            return await GetByPredicateAsync(async () => await GetEvents(getlistOfEvents), x => x.VenueId == venueId);
        }

        private static async Task<List<Event>> GetEvents(Func<Task<Root>> getlistOfEvents)
        {
            var events = await getlistOfEvents();
            return events.Events;
        }
    }
}
