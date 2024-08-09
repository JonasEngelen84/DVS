using DVS.Domain.Models;

namespace DVS.WPF.ViewModels
{
    public class DetailedEmployeeListingItemViewModel(Employee employee, Guid? employeeClothesSizeGuidID) : ViewModelBase
    {
        public Employee Employee { get; private set; } = employee;
        public string ID => Employee.ID;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;

        public Guid? EmployeeClothesSizeGuidID { get; private set; } = employeeClothesSizeGuidID;
        public Guid? ClothesGuidID => Employee.EmployeeClothes.FirstOrDefault(s => s.GuidID == EmployeeClothesSizeGuidID)?.ClothesSize.Clothes.GuidID;
        public string? ClothesID => Employee.EmployeeClothes.FirstOrDefault(s => s.GuidID == EmployeeClothesSizeGuidID)?.ClothesSize.Clothes.ID ?? null;
        public string? ClothesName => Employee.EmployeeClothes.FirstOrDefault(s => s.GuidID == EmployeeClothesSizeGuidID)?.ClothesSize.Clothes.Name ?? null;
        public string? Size => Employee.EmployeeClothes.FirstOrDefault(c => c.GuidID == EmployeeClothesSizeGuidID)?.ClothesSize.Size.Size ?? null;
        public int? Quantity => Employee.EmployeeClothes.FirstOrDefault(c => c.GuidID == EmployeeClothesSizeGuidID)?.Quantity ?? null;
        public string? Comment => Employee.EmployeeClothes.FirstOrDefault(s => s.GuidID == EmployeeClothesSizeGuidID)?.Comment ?? null;
                
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

        public void Update(Employee employee, Guid? employeeClothesSizeGuidID)
        {
            Employee = employee;
            EmployeeClothesSizeGuidID = employeeClothesSizeGuidID;

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
