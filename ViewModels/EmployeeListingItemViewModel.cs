using DVS.Commands.DVSHeadViewCommands;
using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels.ListViewItems
{
    public class EmployeeListingItemViewModel : ViewModelBase
    {
        public EmployeeModel Employee { get; private set; }
        public string ID => Employee.ID;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;
        public string? Comment => Employee.Comment;
        public ObservableCollection<ClothesModel> Clothes => Employee.Clothes;

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

        public ICommand OpenEditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearClothesListCommand { get; set; }
        public ICommand PrintEmployeeCommand { get; set; }


        public EmployeeListingItemViewModel(EmployeeModel employee, DVSListingViewModel dVSListingViewModel,
            ModalNavigationStore modalNavigationStore, EmployeeStore employeeStore, ClothesStore clothesStore)
        {
            Employee = employee;
            OpenEditCommand = new OpenEditEmployeeCommand(this, modalNavigationStore, employeeStore, clothesStore, dVSListingViewModel);
            DeleteCommand = new DeleteEmployeeCommand(this, employeeStore);
            ClearClothesListCommand = new ClearEmployeeClothesListCommand(this, employeeStore);
            PrintEmployeeCommand = new OpenPrintEmployeeCommand();
        }

        public void Update(EmployeeModel employee)
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
