using DVS.Commands;
using DVS.Models;
using System.Windows.Input;

namespace DVS.ViewModels.ListViewItems
{
    public class EmployeeListingItemViewModel : ViewModelBase
    {
        private EmployeeModel _employee;
        public EmployeeModel Employee 
        {
            get =>  _employee;
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(Employee));
                ID = Employee?.ID;
                Lastname = Employee?.Lastname;
                Firstname = Employee?.Firstname;
                Comment = Employee?.Comment;
            }
        }

        private string _iD;
        public string ID
        {
            get => _iD;
            set
            {
                _iD = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }
        
        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
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
        public ICommand ClearClothesListCommand { get; set; }
        public ICommand PrintCommand { get; set; }


        public EmployeeListingItemViewModel(EmployeeModel employee)
        {
            Employee = employee;
            EditCommand = new OpenEditEmployeeCommand();
            DeleteCommand = new DeleteEmployeeCommand();
        }
    }
}
