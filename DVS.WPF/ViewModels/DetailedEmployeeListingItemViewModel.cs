using DVS.Domain.Models;

namespace DVS.WPF.ViewModels
{
    public class DetailedEmployeeListingItemViewModel(Employee employee, EmployeeClothesSize? employeeClothesSize) : ViewModelBase
    {
        public Employee Employee { get; private set; } = employee;
        public string ID => Employee.Id;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;

        public EmployeeClothesSize? EmployeeClothesSize { get; private set; } = employeeClothesSize;
        public Guid? EmployeeClothesSizeGuidID => EmployeeClothesSize?.GuidId;
        public Guid? ClothesGuidID => EmployeeClothesSize?.ClothesSize.Clothes.GuidId;
        public string? ClothesID => EmployeeClothesSize?.ClothesSize.Clothes.Id ?? null;
        public string? ClothesName => EmployeeClothesSize?.ClothesSize.Clothes.Name ?? null;
        public string? Size => EmployeeClothesSize?.ClothesSize.Size.Size ?? null;
        public int? Quantity => EmployeeClothesSize?.Quantity ?? null;
        public string? Comment => EmployeeClothesSize?.Comment ?? null;

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
