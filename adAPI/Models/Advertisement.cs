using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;


namespace adAPI.Models
{
    public class Advertisement
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool IsDescriptionSerialize { get; set; }
        [NotMapped]
        [JsonIgnore]
        public bool IsCreationDateSerialize { get; set; }

        public bool ShouldSerializeDescription()
        {
            if(IsDescriptionSerialize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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
    }
}
