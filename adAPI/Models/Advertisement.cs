using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace adAPI.Models
{
    public class Advertisement
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
                if (!string.IsNullOrWhiteSpace(_Images))
                {
                    return Images[0];
                }
                else
                {
                    return string.Empty;
                }

            }
        }

        /// <summary>
        /// Array of items returns deseriazided items from _Images and sets serialized value into _Image.
        /// </summary>
        [NotMapped]
        public string[] Images
        {
            get
            {
                return string.IsNullOrWhiteSpace(_Images) ? Array.Empty<string>() : JsonConvert.DeserializeObject<string[]>(_Images);
            }
            set
            {
                _Images = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// This field is bind with the db column.
        /// </summary>
        [JsonIgnore]
        public string _Images { get; set; } = string.Empty;
        #endregion

        #region ShouldSerialize
        /// <summary>
        /// Flag for displaying the description.
        /// </summary>
        [NotMapped]
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
        [NotMapped]
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
        [NotMapped]
        [JsonIgnore]
        public bool IsGetAllImages { get; set; }
        public bool ShouldSerializeImages()
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
