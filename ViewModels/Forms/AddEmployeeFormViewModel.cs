using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEmployeeFormViewModel : ViewModelBase
    {
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
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

        private string? _comment;
        public string? Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

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

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        //TODO: CanSubmit
        //public bool CanSubmit => !string.IsNullOrEmpty(Username);

        public AddEditEmployee_ClothesListViewModel AddEditEmployee_ClothesListviewViewModel { get; }
        public AddEditEmployee_EmployeeClothesListViewModel AddEditEmployee_EmployeeClothesListviewViewModel { get; }

        private readonly EmployeeStore _employeeStore;

        public ICommand AddEmployeeCommand { get; }
        public ICommand CancelEmployeeCommand { get; }

        public AddEmployeeFormViewModel(ClothesStore clothesStore,
                                        EmployeeStore employeeStore,
                                        ICommand addEmployeeCommand,
                                        ICommand cancelEmployeeCommand)
        {
            AddEditEmployee_ClothesListviewViewModel = new(clothesStore);
            AddEditEmployee_EmployeeClothesListviewViewModel = new();

            _employeeStore = employeeStore;
            AddEmployeeCommand = addEmployeeCommand;
            CancelEmployeeCommand = cancelEmployeeCommand;

            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
        }

        protected override void Dispose()
        {
            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;

            base.Dispose();
        }

        private void EmployeeStore_EmployeeAdded(EmployeeModel employee)
        {
            Id = null;
            Firstname = null;
            Lastname = null;
            Comment = null;
            AddEditEmployee_EmployeeClothesListviewViewModel.ClearCollection();
        }
    }
}
