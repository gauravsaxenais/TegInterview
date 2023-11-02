using TegEvents.Framework;
using TegEventsApi.Entities;
using TegEventsApi.Models;

namespace TegEventsApi.QueryManager
{
    public interface IEventQueryManager : IQueryManager<EventReadModel, Event>
    {
        Task<IEnumerable<EventReadModel>> GetEventsByStartDate(Func<Task<Root>> getlistOfEvents, DateTimeOffset startDate);

        Task<IEnumerable<EventReadModel>> GetEventsByVenueIdAsync(Func<Task<Root>> getlistOfEvents, int venueId);
    }
}
