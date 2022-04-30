using adAPI.Contracts;
using adAPI.Data;
using adAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace adAPI.Controllers
{
    [Route("api/advertisement")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IDataManager<Advertisement> _dataManager;
        public AdvertisementController(IDataManager<Advertisement> dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdvertisements([FromQuery]CollectionQueryParameters queryParameters)
        {
            var advertisements = await _dataManager.GetItemsAsync(queryParameters);

            return Ok(advertisements);
        }

        [HttpGet("{id}")]
        public IActionResult GetAdvertisement(Guid id, bool additionalFields)
        {
            var advertisement = _dataManager.GetItemById(id, additionalFields);

            return Ok(advertisement);
        }

        [HttpPost]
        public async Task<IActionResult> PostAdvertisement(Advertisement newAdvertisement)
        {
            var advertisement = await _dataManager.AddItemAsync(newAdvertisement);
            if(advertisement == null)
            {
                return BadRequest(advertisement.Id);
            }
            return CreatedAtAction(nameof(PostAdvertisement), advertisement.Id);
        }
    }
}
