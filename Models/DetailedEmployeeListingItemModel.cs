namespace DVS.Models
{
    public class DetailedEmployeeListingItemModel
    {
        public string ID { get; private set; }
        public string Lastname { get; private set; }
        public string Firstname { get; private set; }
        public string ClothesID { get; private set; }
        public string ClothesName { get; private set; }
        public string Size {  get; private set; }
        public int Quantity { get; private set; }
        public string? Comment { get; private set; }

        public DetailedEmployeeListingItemModel(string iD,
                                          string lastname,
                                          string firstname,
                                          string clothesID,
                                          string clothesName,
                                          string size,
                                          int quantity,
                                          string? comment)
        {
            ID = iD;
            Lastname = lastname;
            Firstname = firstname;
            ClothesID = clothesID;
            ClothesName = clothesName;
            Size = size;
            Quantity = quantity;
            Comment = comment;
        }
    }
}
