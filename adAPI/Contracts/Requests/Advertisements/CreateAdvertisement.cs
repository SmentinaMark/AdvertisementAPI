using Newtonsoft.Json;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace adAPI.Contracts.Requests
{
    public class CreateAdvertisement
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        #region Images
        /// <summary>
        /// Array of items returns deseriazided items from Images and sets serialized value into Image.
        /// </summary>

        public string[] _Images
        {
            get
            {
                return string.IsNullOrWhiteSpace(Images) ? Array.Empty<string>() : JsonConvert.DeserializeObject<string[]>(Images);
            }
            set
            {
                Images = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// This field is bind with the field from AdvertisementModel.
        /// </summary>
        [JsonIgnore]
        public string Images { get; set; } = string.Empty;
        #endregion
    }
}

