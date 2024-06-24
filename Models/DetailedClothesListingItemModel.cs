namespace DVS.Models
{
    public class DetailedClothesListingItemModel : ModelBase
    {
        private string _iD;
        public string ID
        {
            get
            {
                return _iD;
            }
            set
            {
                _iD = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _categorie;
        public string Categorie
        {
            get
            {
                return _categorie;
            }
            set
            {
                _categorie = value;
                OnPropertyChanged(nameof(Categorie));
            }
        }

        private string _season;
        public string Season
        {
            get
            {
                return _season;
            }
            set
            {
                _season = value;
                OnPropertyChanged(nameof(Season));
            }
        }

        private string _size;
        public string Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private string? _comment;
        public string? Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

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
