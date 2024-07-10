namespace DVS.Models
{
    public class DetailedClothesListingItemModel : ModelBase
    {
        private string _iD;
        public string ID
        {
            get => _iD;
            set
            {
                if (value != _iD)
                {
                    _iD = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private CategoryModel _category;
        public CategoryModel Category
        {
            get => _category;
            set
            {
                if (value != _category)
                {
                    _category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        private SeasonModel _season;
        public SeasonModel Season
        {
            get => _season;
            set
            {
                if (value != _season)
                {
                    _season = value;
                    OnPropertyChanged(nameof(Season));
                }
            }
        }

        private string? _size;
        public string? Size
        {
            get => _size;
            set
            {
                if (value != _size)
                {
                    _size = value;
                    OnPropertyChanged(nameof(Size));
                }
            }
        }

        private int? _quantity;
        public int? Quantity
        {
            get => _quantity;
            set
            {
                if(value != _quantity)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if(_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        private bool _isDeleting;
        public bool IsDeleting
        {
            get => _isDeleting;
            set
            {
                if (_isDeleting != value)
                {
                    _isDeleting = value;
                    OnPropertyChanged(nameof(IsDeleting));
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (value != _errorMessage)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(HasErrorMessage));
                }
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);


        public DetailedClothesListingItemModel(
            string iD, string name, CategoryModel category,
            SeasonModel season, string? size, int? quantity)
        {
            ID = iD;
            Name = name;
            Category = category;
            Season = season;
            Size = size;
            Quantity = quantity;
        }
    }
}
