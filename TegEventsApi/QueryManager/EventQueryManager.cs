using AutoMapper;
using TagEvents.RedisCache;
using TegEvents.Framework;
using TegEventsApi.Entities;
using TegEventsApi.Models;

namespace TegEventsApi.QueryManager
{
    public class EventQueryManager : QueryManager<Event, EventReadModel>, IEventQueryManager
    {
        private readonly IRedisCacheService _redisCacheService;
        public EventQueryManager(IRedisCacheService redisCacheService, ILogger<QueryManager<Event, EventReadModel>> logger, IMapper mapper) : base(logger, mapper)
        {
            _redisCacheService = redisCacheService;
        }

        public async Task<IEnumerable<EventReadModel>> GetEventsByStartDate(Func<Task<Root>> getlistOfEvents, DateTimeOffset startDate)
        {
            return await GetByPredicateAsync(async () => await GetEvents(getlistOfEvents), x => x.StartDate == startDate);
        }

        public async Task<IEnumerable<EventReadModel>> GetEventsByVenueIdAsync(Func<Task<Root>> getlistOfEvents, int venueId)
        {
            return await GetByPredicateAsync(async () => await GetEvents(getlistOfEvents), x => x.VenueId == venueId);
        }

        private async Task<List<Event>?> GetEvents(Func<Task<Root>> GetlistOfEvents)
        {
            Root? root;
            //try
            //{
                root = await GetlistOfEvents();
                //await _redisCacheService.SetAsync("root", root);
            //}
            //catch (Exception)
            //{
            //    // if json url fails, see if data
            //    // is stored in cache
            //    root = await _redisCacheService.GetAsync<Root>("root");
            //}

            return root?.Events;
        }
    }
}
