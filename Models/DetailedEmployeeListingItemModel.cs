namespace DVS.Models
{
    public class DetailedEmployeeListingItemModel : ModelBase
    {
        private string _iD;
        public string ID
        {
            get => _iD;
            set
            {
                if (_iD != value)
                {
                    _iD = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    OnPropertyChanged(nameof(Lastname));
                }
            }
        }

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                if (value != _firstname)
                {
                    _firstname = value;
                    OnPropertyChanged(nameof(Firstname));
                }
            }
        }

        private string? _clothesID;
        public string? ClothesID
        {
            get => _clothesID;
            set
            {
                if (value != _clothesID)
                {
                    _clothesID = value;
                    OnPropertyChanged(nameof(ClothesID));
                }
            }
        }

        private string? _clothesName;
        public string? ClothesName
        {
            get => _clothesName;
            set
            {
                if(value != _clothesName)
                {
                    _clothesName = value;
                    OnPropertyChanged(nameof(ClothesName));
                }
            }
        }

        private string? _size;
        public string? Size
        {
            get => _size;
            set
            {
                if (_size != value)
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
                if (_quantity != value)
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
                if (value != _isDeleting)
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


        public DetailedEmployeeListingItemModel(
            string iD, string lastname, string firstname, string? clothesID,
            string? clothesName, string? size, int? quantity)
        {
            ID = iD;
            Lastname = lastname;
            Firstname = firstname;
            ClothesID = clothesID;
            ClothesName = clothesName;
            Size = size;
            Quantity = quantity;
        }
    }
}
