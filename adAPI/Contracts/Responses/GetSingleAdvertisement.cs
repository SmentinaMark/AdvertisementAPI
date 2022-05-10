using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace adAPI.Contracts
{
    public class GetSingleAdvertisement
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }


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

        public string[] _Images
        {
            get
            {
                return string.IsNullOrWhiteSpace(Images) ? Array.Empty<string>() : JsonConvert.DeserializeObject<string[]>(Images.Replace("\\", "\\\\"));
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

        #region ShouldSerialize
        /// <summary>
        /// Flag for displaying the description.
        /// </summary>

        [JsonIgnore]
        public bool IsDescriptionSerialize { get; set; }
        public bool ShouldSerializeDescription()
        {
            if (IsDescriptionSerialize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Flag for displaying the date.
        /// </summary>

        [JsonIgnore]
        public bool IsCreationDateSerialize { get; set; }
        public bool ShouldSerializeCreationDate()
        {
            if (IsCreationDateSerialize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Flag for displaying all images.
        /// </summary>

        [JsonIgnore]
        public bool IsGetAllImages { get; set; }
        public bool ShouldSerialize_Images()
        {
            if (IsGetAllImages)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ShouldSerializeMainImage()
        {
            if (!IsGetAllImages)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
