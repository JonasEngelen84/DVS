using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditEmployeeFormViewModel : ViewModelBase
    {
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

        public DVSListingViewModel AvailableClothes { get; }
        public DVSListingViewModel NewEmployeeClothes { get; }

        public ICommand AddEmployeeCommand { get; }
        public ICommand EditEmployeeCommand { get; }
        public ICommand ClearEmployeeClothesListCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }


        public AddEditEmployeeFormViewModel(DVSListingViewModel availableClothes,
                                            ClothesStore clothesStore,
                                            EmployeeStore employeeStore,
                                            ICommand addEmployeeCommand,
                                            ICommand editEmployeeCommand,
                                            ICommand clearEmployeeClothesListCommand,
                                            ICommand deleteEmployeeCommand)
        {
            AvailableClothes = availableClothes;
            NewEmployeeClothes = new DVSListingViewModel( clothesStore, employeeStore);

            AddEmployeeCommand = addEmployeeCommand;
            EditEmployeeCommand = editEmployeeCommand;
            ClearEmployeeClothesListCommand = clearEmployeeClothesListCommand;
            DeleteEmployeeCommand = deleteEmployeeCommand;
        }
    }
}
