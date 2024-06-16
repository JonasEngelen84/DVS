namespace DVS.Models
{
    public class ClothesModel(string id, string name, string categorie, string season, string? comment)
    {
        public string ID { get; set; } = id;
        public string Name { get; set; } = name;
        public string Categorie { get; set; } = categorie;
        public string Season { get; set; } = season;
        public string? Comment { get; set; } = comment;

        public List<ClothesSizeModel> Sizes { get; set; } = [];
    }
}
