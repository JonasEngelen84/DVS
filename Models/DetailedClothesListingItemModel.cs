namespace DVS.Models
{
    public class DetailedClothesListingItemModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Categorie { get; set; }
        public string Season { get; set; }
        public string Size {  get; set; }
        public int Quantity { get; set; }
        public string? Comment { get; set; }

        public DetailedClothesListingItemModel(string iD,
                                          string name,
                                          string categorie,
                                          string season,
                                          string size,
                                          int quantity,
                                          string? comment)
        {
            ID = iD;
            Name = name;
            Categorie = categorie;
            Season = season;
            Size = size;
            Quantity = quantity;
            Comment = comment;
        }
    }
}
