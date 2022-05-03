using adAPI.Contracts;
using adAPI.Models;
using adAPI.Validation;
using Microsoft.AspNetCore.Mvc;
namespace adAPI.Controllers
{
    [Route("api/advertisements")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IDataManager<Advertisement, CollectionQueryParameters> _dataManager;
        private readonly AdvertisementValidator _validator;
        public AdvertisementController(IDataManager<Advertisement, CollectionQueryParameters> dataManager)
        {
            _dataManager = dataManager;
            _validator = new AdvertisementValidator();
        }

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
