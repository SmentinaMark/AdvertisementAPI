using adAPI.Contracts;
using adAPI.Models;
using adAPI.Services;
using adAPI.Validation;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace adAPI.Controllers
{
    /// <summary>
    /// This contriller allows to work with advertisements endpoints
    /// </summary>
    [Route("api/advertisements")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _service;
        private readonly AdvertisementValidator _validator;

        private readonly IMapper _mapper;

        public AdvertisementController(IAdvertisementService service, IMapper mapper)
        {
            _validator = new AdvertisementValidator();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// The endpoint allows to get the final list of advertisements.
        /// </summary>
        /// <param name="queryParameters">Object with query parameters for view finished collection.</param>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        public IActionResult GetAdvertisements([FromQuery] CollectionQueryParameters queryParameters)
        {
            var advertisements = _service.GetItems(queryParameters);
            if (advertisements.Count == 0)
            {
                return NoContent();
            }

            var mappedAdvertisements = _mapper.Map<List<GetAdvertisements>>(advertisements);

            return Ok(mappedAdvertisements);
        }

        /// <summary>
        /// The endpoint allows to get the advertisement of advertisements by ID.
        /// </summary>
        /// <param name="id">Item Id.</param>
        /// <param name="additionalFields">Flag for adding additional fields.</param>
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
        public IActionResult GetAdvertisement(Guid id, bool additionalFields)
        {
            var advertisement = _service.GetItemById(id, additionalFields);

            if (advertisement != null && advertisement.Id == id)
            {
                var mappedAdvertisement = _mapper.Map<GetSingleAdvertisement>(advertisement);
                return Ok(mappedAdvertisement);
            }

            return NotFound();
        }

        /// <summary>
        /// The endpoint allows you to post new advertisement of advertisements.
        /// </summary>
        /// <param name="newAdvertisement">Object to adding into the Db.</param>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(ProblemDetails))]
        public IActionResult PostAdvertisement(Advertisement newAdvertisement)
        {
            var validResult = _validator.Validate(newAdvertisement);

            if (validResult.IsValid)
            {
                _service.AddItem(newAdvertisement);

                var mappedAdvertisement = _mapper.Map<CreateAdvertisement>(newAdvertisement);
                return CreatedAtAction(nameof(PostAdvertisement), mappedAdvertisement);
            }

            return BadRequest();
        }
    }
}
