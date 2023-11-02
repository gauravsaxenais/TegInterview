using TegEvents.Framework;

namespace TegEventsApi.Entities
{
    public class Event : IEntityWithId
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public int VenueId { get; set; }
        public int Id { get; set; }
    }
}
