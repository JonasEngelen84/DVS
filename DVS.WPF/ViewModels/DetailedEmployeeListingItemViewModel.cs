using DVS.Domain.Models;

namespace DVS.WPF.ViewModels
{
    public class DetailedEmployeeListingItemViewModel(Employee employee, EmployeeClothesSize? employeeClothesSize) : ViewModelBase
    {
        public Employee Employee { get; private set; } = employee;
        public string ID => Employee.ID;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;

        public EmployeeClothesSize? EmployeeClothesSize { get; private set; } = employeeClothesSize;
        public Guid? EmployeeClothesSizeGuidID => EmployeeClothesSize?.GuidID;
        public Guid? ClothesGuidID => EmployeeClothesSize?.ClothesSize.Clothes.GuidID;
        public string? ClothesID => EmployeeClothesSize?.ClothesSize.Clothes.ID ?? null;
        public string? ClothesName => EmployeeClothesSize?.ClothesSize.Clothes.Name ?? null;
        public string? Size => EmployeeClothesSize?.ClothesSize.Size.Size ?? null;
        public int? Quantity => EmployeeClothesSize?.Quantity ?? 0;
        public string? Comment => EmployeeClothesSize?.Comment ?? null;
                
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

        public void Update(Employee employee, EmployeeClothesSize? employeeClothesSize)
        {
            Employee = employee;
            EmployeeClothesSize = employeeClothesSize;

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
