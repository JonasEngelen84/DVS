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
        public CategoryModel Category { get; set; }
        public SeasonModel Season { get; set; }
        public string? Comment { get; set; }

        public ObservableCollection<ClothesSizeModel> Sizes { get; set; } = [];
    }
}
