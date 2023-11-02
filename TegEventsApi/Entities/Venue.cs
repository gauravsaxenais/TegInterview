using TegEvents.Framework;

namespace TegEventsApi.Entities
{
    public class Venue : IEntityWithId
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; } 
        public string? Location { get; set; }
    }
}
