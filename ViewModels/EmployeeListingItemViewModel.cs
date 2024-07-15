using DVS.Commands.DVSHeadViewCommands;
using DVS.Models;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.ListViewItems
{
    public class EmployeeListingItemViewModel : ViewModelBase
    {
        public EmployeeModel Employee { get; private set; }

        private string _iD;
        public string ID
        {
            get => _iD;
            set
            {
                if (value != _iD)
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
                if (_firstname != value)
                {
                    _firstname = value;
                    OnPropertyChanged(nameof(Firstname));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
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


        public EmployeeListingItemViewModel(EmployeeModel employee,
            DVSListingViewModel dVSListingViewModel, ModalNavigationStore modalNavigationStore)
        {
            Employee = employee;

            DeleteCommand = new DeleteEmployeeCommand();
            ClearClothesListCommand = new ClearEmployeeClothesListCommand();
            PrintEmployeeCommand = new OpenPrintEmployeeCommand();
            OpenEditCommand = new OpenEditEmployeeCommand(
                dVSListingViewModel, employee, modalNavigationStore);
        }
    }
}
