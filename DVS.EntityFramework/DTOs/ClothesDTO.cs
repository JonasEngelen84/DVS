using DVS.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DVS.EntityFramework.DTOs
{
    public class ClothesDTO
    {
        public Guid GuidID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Season Season { get; set; }
        public string? Comment { get; set; }

        public ObservableCollection<ClothesSize> Sizes { get; set; } = [];
    }
}
