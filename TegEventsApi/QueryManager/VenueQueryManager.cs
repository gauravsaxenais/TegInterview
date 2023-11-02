using AutoMapper;
using TegEvents.Framework;
using TegEventsApi.Entities;
using TegEventsApi.Models;

namespace TegEventsApi.QueryManager
{
    public class VenueQueryManager : QueryManager<Venue, VenueReadModel>, IVenueQueryManager
    {
        public VenueQueryManager(ILogger<QueryManager<Venue, VenueReadModel>> logger, IMapper mapper) : base(logger, mapper)
        {
        }
        public async Task<IEnumerable<VenueReadModel>> GetVenueByVenueIdAsync(Func<Task<Root>> getlistOfVenues, int venueId)
        {
            return await GetByPredicateAsync(async () => await GetVenues(getlistOfVenues), x => x.Id ==  venueId);
        }
        private static async Task<List<Venue>> GetVenues(Func<Task<Root>> getlistOfEvents)
        {
            var venues = await getlistOfEvents();
            return venues.Venues;
        }
    }
}
