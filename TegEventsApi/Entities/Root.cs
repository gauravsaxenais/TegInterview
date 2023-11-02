using TegEvents.Framework;

namespace TegEventsApi.Entities
{
    public class Root : IEntity
    {
        public List<Event> Events { get; set; }
        public List<Venue> Venues { get; set; }
    }
}
