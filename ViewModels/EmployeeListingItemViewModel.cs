using DVS.Commands;
using DVS.Models;
using System.Windows.Input;

namespace DVS.ViewModels.ListViewItems
{
    public class EmployeeListingItemViewModel : ViewModelBase
    {
        public EmployeeModel Employee { get; private set; }

        public string ID => Employee.ID;
        public string Lastname => Employee.Lastname;
        public string Firstname => Employee.Firstname;
        public string Comment => Employee.Comment;

        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
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
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public EmployeeListingItemViewModel(EmployeeModel employee)
        {
            Employee = employee;

            EditCommand = new OpenEditEmployeeCommand();
            DeleteCommand = new DeleteEmployeeCommand();
        }


        private void EditEmployee()
        {
            // Implementiere die Bearbeitungslogik
        }

        private void DeleteEmployee()
        {
            // Implementiere die Löschlogik
        }
    }
}
