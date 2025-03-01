using DVS.Domain.Models;
using DVS.WPF.Commands.EmployeeCommands;
using DVS.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.ListingItems
{
    public class EmployeeListingItemViewModel : ModelBase
    {
        public Employee Employee { get; private set; }
        public string Id => Employee.Id;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;
        public string? Comment => Employee.Comment;
        public ObservableCollection<EmployeeClothesSize> Clothes => Employee.Clothes;

        private bool _isSubmitting;
        public bool IsSubmitting
        {
            get
            {
                return _isSubmitting;
            }
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }

        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                if (value != _isDeleting)
                {
                    _isDeleting = value;
                    OnPropertyChanged(nameof(IsDeleting));
                }
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged(nameof(IsExpanded));
                }
            }
        }

        public bool HasError;

        public ICommand OpenEditEmployee { get; set; }
        public ICommand DeleteEmployee { get; set; }
        public ICommand ClearEmpoyeeClothesList { get; set; }
        public ICommand PrintEmployee { get; set; }


        public EmployeeListingItemViewModel(
            Employee employee,
            DVSListingViewModel dVSListingViewModel,
            ModalNavigationStore modalNavigationStore,
            EmployeeStore employeeStore,
            ClothesStore clothesStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            ClothesSizeStore clothesSizeStore,
            EmployeeClothesSizeStore employeeClothesSizesStore)
        {
            Employee = employee;
            DeleteEmployee = new DeleteEmployeeCommand(
                this,
                employeeStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore);
            ClearEmpoyeeClothesList = new ClearEmployeeClothesListCommand(
                this,
                employeeStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore);
            PrintEmployee = new OpenPrintEmployeeCommand();

            OpenEditEmployee = new OpenEditEmployeeCommand(
                this,
                modalNavigationStore,
                employeeStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore);
        }

        public void Update(Employee employee)
        {
            Employee = employee;

            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Lastname));
            OnPropertyChanged(nameof(Firstname));
            OnPropertyChanged(nameof(Comment));
            OnPropertyChanged(nameof(Clothes));
        }
    }
}
