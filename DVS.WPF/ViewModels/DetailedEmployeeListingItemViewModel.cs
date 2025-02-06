using DVS.Domain.Models;

namespace DVS.WPF.ViewModels
{
    public class DetailedEmployeeListingItemViewModel : ViewModelBase
    {
        public Employee Employee { get; private set; }
        public string Id => Employee.Id;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;

        public EmployeeClothesSize? EmployeeClothesSize { get; private set; }
        public Guid? EmployeeClothesSizeGuidID => EmployeeClothesSize?.GuidId;
        public Guid? ClothesGuidID => EmployeeClothesSize?.ClothesSize.Clothes.GuidId;
        public string? ClothesID => EmployeeClothesSize?.ClothesSize.Clothes.Id ?? null;
        public string? ClothesName => EmployeeClothesSize?.ClothesSize.Clothes.Name ?? null;
        public string? Size => EmployeeClothesSize?.ClothesSize.Size.Size ?? null;
        public string? Comment => EmployeeClothesSize?.Comment ?? null;

        private int? _quantity;
        public int? Quantity
        {
            get { return _quantity; }

            set
            {
                if (EmployeeClothesSize != null)
                    _quantity = EmployeeClothesSize.Quantity;
                
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public DetailedEmployeeListingItemViewModel(Employee employee, EmployeeClothesSize? employeeClothesSize)
        {
            Employee = employee;
            EmployeeClothesSize = employeeClothesSize;
            _quantity = EmployeeClothesSize?.Quantity ?? null;
        }

        public void Update(Employee employee, EmployeeClothesSize? employeeClothesSize)
        {
            Employee = employee;
            EmployeeClothesSize = employeeClothesSize;

            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Lastname));
            OnPropertyChanged(nameof(Firstname));
            OnPropertyChanged(nameof(ClothesID));
            OnPropertyChanged(nameof(ClothesName));
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Comment));
        }
    }
}
