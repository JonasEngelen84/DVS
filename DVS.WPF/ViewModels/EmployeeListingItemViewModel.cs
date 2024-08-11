using DVS.WPF.Commands.AddEditEmployeeCommands;
using DVS.Domain.Models;
using DVS.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.ListViewItems
{
    public class EmployeeListingItemViewModel : ViewModelBase
    {
        public Employee Employee { get; private set; }
        public string ID => Employee.ID;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;
        public string? Comment => Employee.Comment;
        public ObservableCollection<EmployeeClothesSize> Clothes => Employee.Clothes;

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

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
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

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand OpenEditEmployee { get; set; }
        public ICommand DeleteEmployee { get; set; }
        public ICommand ClearEmpoyeeClothesList { get; set; }
        public ICommand PrintEmployee { get; set; }


        public EmployeeListingItemViewModel(Employee employee,
                                            DVSListingViewModel dVSListingViewModel,
                                            ModalNavigationStore modalNavigationStore,
                                            EmployeeStore employeeStore,
                                            ClothesStore clothesStore)
        {
            Employee = employee;
            OpenEditEmployee = new OpenEditEmployeeCommand(this, modalNavigationStore, employeeStore, clothesStore, dVSListingViewModel);
            DeleteEmployee = new DeleteEmployeeCommand(this, employeeStore);
            ClearEmpoyeeClothesList = new ClearEmployeeClothesListCommand(this, employeeStore);
            PrintEmployee = new OpenPrintEmployeeCommand();
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
