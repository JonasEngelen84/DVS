using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEmployeeFormViewModel : ViewModelBase
    {
        private string _iD;
        public string Id
        {
            get => _iD;
            set
            {
                _iD = value;
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

        public AddEditEmployee_ClothesListViewModel AddEditEmployee_ClothesListViewModel { get; }

        public ICommand AddEmployeeCommand { get; }
        public ICommand CancelEmployeeCommand { get; }

        public AddEmployeeFormViewModel(DVSDetailedClothesListingViewModel dVSDetailedClothesListingViewModel,
                                        ICommand addEmployeeCommand,
                                        ICommand cancelEmployeeCommand)
        {
            AddEditEmployee_ClothesListViewModel = new(dVSDetailedClothesListingViewModel);

            AddEmployeeCommand = addEmployeeCommand;
            CancelEmployeeCommand = cancelEmployeeCommand;

            _iD = "ID";
            _lastname = "Nachname";
            _firstname = "Vorname";
            _comment = "Kommentar";
        }
    }
}
