using TegEvents.Framework;
using TegEventsApi.Entities;
using TegEventsApi.Models;

namespace TegEventsApi.QueryManager
{
    public interface IVenueQueryManager : IQueryManager<VenueReadModel, Venue>
    {
        Task<IEnumerable<VenueReadModel>> GetVenueByVenueIdAsync(Func<Task<Root>> getlistOfVenues, int venueId);
    }
}
