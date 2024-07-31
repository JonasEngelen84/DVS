using System.Collections.ObjectModel;

namespace DVS.Domain.Models
{
    public class ClothesModel(Guid guidID, string id, string name, CategoryModel category, SeasonModel season, string? comment)
    {
        public Guid GuidID { get; } = guidID;
        public string ID { get; } = id;
        public string Name { get; } = name;
        public CategoryModel Category { get; } = category;
        public SeasonModel Season { get; } = season;
        public string? Comment { get; } = comment;

        public ObservableCollection<ClothesSizeModel> Sizes { get; set; } = [];
    }
}
