using DVS.ViewModels;

namespace DVS.Models
{
    public class ClothesModel(string id, string name, CategoryModel categorie, SeasonModel season, string? comment)
    {
        public string ID { get; set; } = id;
        public string Name { get; set; } = name;
        public CategoryModel Categorie { get; set; } = categorie;
        public SeasonModel Season { get; set; } = season;
        public string? Comment { get; set; } = comment;

        public List<ClothesSizeModel> Sizes { get; set; } = [];
    }
}
