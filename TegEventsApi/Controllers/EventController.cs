using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TegEvents.Framework;
using TegEvents.Framework.Blob;
using TegEventsApi.Entities;
using TegEventsApi.Models;
using TegEventsApi.QueryManager;

namespace TegEventsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IBlobManager<Root> _blobManager;
        private readonly IEventQueryManager _queryManager;
        private readonly IVenueQueryManager _venueQueryManager;

        public EventController(IVenueQueryManager venueQueryManager, IBlobManager<Root> blobManager, ILogger<EventController> logger, IEventQueryManager queryManager)
        {
            EnsureArg.IsNotNull(queryManager, nameof(Manager));
            EnsureArg.IsNotNull(blobManager, nameof(Manager));
            EnsureArg.IsNotNull(logger, nameof(ILogger<EventController>));
            EnsureArg.IsNotNull(venueQueryManager, nameof(Manager));

            _logger = logger;
            _blobManager = blobManager;
            _queryManager = queryManager;
            _venueQueryManager = venueQueryManager;
        }

        [HttpGet(nameof(GetAllEventsByVenue))]
        [ProducesResponseType(typeof(IEnumerable<EventReadModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEventsByVenue([Required, FromQuery] int venueId)
        {
            var eventsByVenue = await _queryManager.GetEventsByVenueIdAsync(_blobManager.GetListOfData, venueId);
            return StatusCode(StatusCodes.Status200OK, eventsByVenue);
        }

        [HttpGet(nameof(GetAllEventsByDate))]
        [ProducesResponseType(typeof(IEnumerable<EventReadModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEventsByDate([Required, FromQuery] DateTime startDate)
        {
            var eventsByVenue = await _queryManager.GetEventsByStartDate(_blobManager.GetListOfData, startDate);
            return StatusCode(StatusCodes.Status200OK, eventsByVenue);
        }

        [HttpGet(nameof(GetVenueInformation))]
        [ProducesResponseType(typeof(IEnumerable<EventReadModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVenueInformation([Required, FromQuery] int venueId)
        {
            var venues = await _venueQueryManager.GetVenueByVenueIdAsync(_blobManager.GetListOfData, venueId);
            return StatusCode(StatusCodes.Status200OK, venues.FirstOrDefault());
        }
    }
}