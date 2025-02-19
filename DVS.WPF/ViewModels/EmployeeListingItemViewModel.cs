using DVS.Domain.Models;
using DVS.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DVS.WPF.Commands.EmployeeCommands;

namespace DVS.WPF.ViewModels
{
    public class EmployeeListingItemViewModel : ModelBase
    {
        public Employee Employee { get; private set; }
        public string ID => Employee.Id;
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
            AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
            EmployeeStore employeeStore,
            ClothesStore clothesStore,
            SizeStore sizeStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            ClothesSizeStore clothesSizeStore,
            EmployeeClothesSizeStore employeeClothesSizesStore)
        {
            Employee = employee;
            DeleteEmployee = new DeleteEmployeeCommand(this, employeeStore);
            ClearEmpoyeeClothesList = new ClearEmployeeClothesListCommand(this, employeeStore);
            PrintEmployee = new OpenPrintEmployeeCommand();

            OpenEditEmployee = new OpenEditEmployeeCommand(
                this,
                modalNavigationStore,
                employeeStore,
                clothesStore,
                sizeStore,
                categoryStore,
                seasonStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                dVSListingViewModel,
                addEditEmployeeListingViewModel);
        }

        public void Update(Employee employee)
        {
            Employee = employee;

            OnPropertyChanged(nameof(ID));
            OnPropertyChanged(nameof(Lastname));
            OnPropertyChanged(nameof(Firstname));
            OnPropertyChanged(nameof(Comment));
            OnPropertyChanged(nameof(Clothes));
        }
    }
}
