namespace DVS.Models
{
    public class DetailedClothesListingItemModel
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Categorie { get; private set; }
        public string Season { get; private set; }
        public string Size {  get; private set; }
        public int Quantity { get; private set; }
        public string? Comment { get; private set; }

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
