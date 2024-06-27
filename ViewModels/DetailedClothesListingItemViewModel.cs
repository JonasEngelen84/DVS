namespace DVS.ViewModels
{
    public class DetailedClothesListingItemViewModel : ViewModelBase
    {
        private string _iD;
        public string ID
        {
            get => _iD;
            set
            {
                _iD = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _categorie;
        public string Categorie
        {
            get => _categorie;
            set
            {
                _categorie = value;
                OnPropertyChanged(nameof(Categorie));
            }
        }

        private string _season;
        public string Season
        {
            get => _season;
            set
            {
                _season = value;
                OnPropertyChanged(nameof(Season));
            }
        }

        private string? _size;
        public string? Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        private int? _quantity;
        public int? Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private string? _comment;
        public string? Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        private bool _isDeleting;
        public bool IsDeleting
        {
            get => _isDeleting;
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public DetailedClothesListingItemViewModel(
            string iD, string name, string categorie,
            string season, string ?size, int? quantity, string? comment)
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
