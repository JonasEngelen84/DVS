namespace DVS.Models
{
    public class ClothesModel(int id, string name, string categorie, string size, string season, int quantity)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Categorie { get; set; } = categorie;
        public string Season { get; set; } = season;
        public string Size { get; set; } = size;
        public int Quantity { get; set; } = quantity;
        public string Comment { get; set; } = "";
    }
}
