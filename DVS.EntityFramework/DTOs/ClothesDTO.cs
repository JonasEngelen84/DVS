using DVS.Domain.Models;
using System.Collections.ObjectModel;

namespace DVS.EntityFramework.DTOs
{
    public class ClothesDTO
    {
        public Guid GuidID { get; set; }
        public Guid CategoryGuidID { get; set; }
        public Guid SeasonGuidID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string? Comment { get; set; }

        public ObservableCollection<ClothesSize> Sizes { get; set; } = [];
    }
}
