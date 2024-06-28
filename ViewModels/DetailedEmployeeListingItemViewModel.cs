namespace DVS.ViewModels
{
    public class DetailedEmployeeListingItemViewModel : ViewModelBase
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

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }

        private string? _clothesID;
        public string? ClothesID
        {
            get => _clothesID;
            set
            {
                _clothesID = value;
                OnPropertyChanged(nameof(ClothesID));
            }
        }

        private string? _clothesName;
        public string? ClothesName
        {
            get => _clothesName;
            set
            {
                _clothesName = value;
                OnPropertyChanged(nameof(ClothesName));
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


        public DetailedEmployeeListingItemViewModel(
            string iD, string lastname, string firstname, string? clothesID,
            string? clothesName, string? size, int? quantity, string? comment)
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
