using adAPI.Contracts;
using adAPI.Models;
using adAPI.Validation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace adAPI.Controllers
{
    /// <summary>
    /// This contriller allows to work with advertisements endpoints
    /// </summary>
    [Route("api/advertisements")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IDataManager _dataManager;
        private readonly AdvertisementValidator _validator;
        public AdvertisementController(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _validator = new AdvertisementValidator();
        }

        /// <summary>
        /// The endpoint allows to get the final list of advertisements.
        /// </summary>
        /// <param name="queryParameters">Object with query parameters for view finished collection.</param>
        /// <returns>200 with list of items or 204</returns>
        [HttpGet]
        public IActionResult GetAdvertisements([FromQuery] CollectionQueryParameters queryParameters)
        {
            var advertisements = _dataManager.GetItems(queryParameters);
            if (advertisements.Count == 0)
            {
                return NoContent();
            }

            return Ok(advertisements);
        }

        /// <summary>
        /// The endpoint allows to get the advertisement of advertisements by ID.
        /// </summary>
        /// <param name="id">Item Id.</param>
        /// <param name="additionalFields">Flag for adding additional fields.</param>
        /// <returns>200 with object or 404</returns>
        [HttpGet("{id}")]
        public IActionResult GetAdvertisement(Guid id, bool additionalFields)
        {
            var advertisement = _dataManager.GetItemById(id, additionalFields);

            if (advertisement != null && advertisement.Id == id)
            {
                return Ok(advertisement);
            }

            return NotFound();
        }

        /// <summary>
        /// The endpoint allows you to post new advertisement of advertisements.
        /// </summary>
        /// <param name="newAdvertisement">Object to adding into the Db.</param>
        /// <returns>201 with Id or 400 with Id</returns>
        [HttpPost]
        public IActionResult PostAdvertisement(Advertisement newAdvertisement)
        {
            var validResult = _validator.Validate(newAdvertisement);

            if (validResult.IsValid)
            {
                var advertisement = _dataManager.AddItem(newAdvertisement);
                return CreatedAtAction(nameof(PostAdvertisement), newAdvertisement.Id);
            }

            return BadRequest(newAdvertisement.Id);
        }
    }
}
