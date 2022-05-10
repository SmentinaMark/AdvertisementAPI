using adAPI.Contracts;
using adAPI.Contracts.Requests;
using adAPI.Data.Models;
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
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(GetAdvertisements))]
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
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(GetSingleAdvertisement))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(GetSingleAdvertisement))]
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
        [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(GetCreateAdvertisement))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(GetCreateAdvertisement))]
        public IActionResult PostAdvertisement(CreateAdvertisement newAdvertisement)
        {

            var validResult = _validator.Validate(newAdvertisement);

            if (validResult.IsValid)
            {
                var creatMappedAdvertisement = _mapper.Map<Advertisement>(newAdvertisement);
                _service.AddItem(creatMappedAdvertisement);

                var mappedAdvertisement = _mapper.Map<GetCreateAdvertisement>(creatMappedAdvertisement);
                return CreatedAtAction(nameof(PostAdvertisement), mappedAdvertisement);
            }

            return BadRequest();
        }
    }
}
