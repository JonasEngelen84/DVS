namespace DVS.ViewModels
{
    public class DetailedClothesListingItem : ViewModelBase
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Categorie { get; set; }
        public string Season { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string? Comment { get; set; }
    }
}
