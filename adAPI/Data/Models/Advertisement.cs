using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace adAPI.Data.Models
{
    public class Advertisement
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }

        public string Images { get; set; } = string.Empty;


        [NotMapped]
        public bool IsDescriptionSerialize { get; set; }
        [NotMapped]
        public bool IsCreationDateSerialize { get; set; }
        [NotMapped]
        public bool IsGetAllImages { get; set; }
    }
}
