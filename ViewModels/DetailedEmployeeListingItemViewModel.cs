using DVS.Models;

namespace DVS.ViewModels
{
    public class DetailedEmployeeListingItemViewModel : ViewModelBase
    {
        public EmployeeModel Employee { get; private set; }
        public Guid GuidID => Employee.GuidID;
        public string ID => Employee.ID;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;
        public Guid? ClothesGuidID { get; }
        public string? ClothesID => Employee.Clothes.FirstOrDefault(s => s.GuidID == ClothesGuidID)?.ID ?? null;
        public string? ClothesName => Employee.Clothes.FirstOrDefault(s => s.GuidID == ClothesGuidID)?.Name ?? null;
        public string Size { get; }

        public int? Quantity => Employee.Clothes.FirstOrDefault(c => c.GuidID == ClothesGuidID)?.Sizes
            .FirstOrDefault(s => s.Size == Size)?.Quantity ?? null;

        public string? Comment => Employee.Clothes.FirstOrDefault(s => s.GuidID == ClothesGuidID)?.Sizes
            .FirstOrDefault(s => s.Size == Size)?.Comment ?? null;

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


        public DetailedEmployeeListingItemViewModel(EmployeeModel employee, Guid? clothesGuidID, string size)
        {
            Employee = employee;
            ClothesGuidID = clothesGuidID;
            Size = size;
        }


        public void Update(EmployeeModel employee)
        {
            Employee = employee;

            OnPropertyChanged(nameof(ID));
            OnPropertyChanged(nameof(Lastname));
            OnPropertyChanged(nameof(Firstname));
            OnPropertyChanged(nameof(ClothesID));
            OnPropertyChanged(nameof(ClothesName));
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Comment));
        }
    }
}
