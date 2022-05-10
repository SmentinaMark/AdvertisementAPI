using Newtonsoft.Json;

namespace adAPI.Contracts
{
    public class GetAdvertisements
    {
        public string Title { get; set; }
        public double Price { get; set; }

        #region Images
        /// <summary>
        /// This field returns first item from array of Images[].
        /// </summary>
        public string MainImage
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Images))
                {
                    return _Images[0];
                }
                else
                {
                    return string.Empty;
                }

            }
        }

        /// <summary>
        /// Array of items returns deseriazided items from Images and sets serialized value into Image.
        /// </summary>
        [JsonIgnore]
        public string[] _Images
        {
            get
            {
                var result = string.IsNullOrWhiteSpace(Images) ? Array.Empty<string>() : JsonConvert.DeserializeObject<string[]>(Images.Replace("\\", "\\\\"));

                return result;
            }
            set
            {

                Images = value.ToString();
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
