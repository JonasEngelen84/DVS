using DVS.Domain.Models;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class AddEditEmployeeFormViewModel(Employee? employee, DVSListingViewModel
        dVSListingViewModel, ICommand submitCommand) : ViewModelBase
    {
        public DVSListingViewModel DVSListingViewModel { get; } = dVSListingViewModel;
        private Employee? Employee { get; } = employee;
        public ICommand SubmitEmployee { get; } = submitCommand;

        private string _iD;
        public string ID
        {
            get => _iD;
            set
            {
                if (ID != value)
                {
                    _iD = value;
                    OnPropertyChanged(nameof(ID));
                    OnPropertyChanged(nameof(CanSubmit));
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
                    OnPropertyChanged(nameof(CanSubmit));
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
                    OnPropertyChanged(nameof(CanSubmit));
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (Comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
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
                if (_isSubmitting != value)
                {
                    _isSubmitting = value;
                    OnPropertyChanged(nameof(IsSubmitting));
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
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(HasErrorMessage));
                }
            }
        }

        public bool CanSubmit
        {//TODO: canSubmitEmployee auf true setzen wenn Kleidungsliste verändert wird
            get
            {
                if (string.IsNullOrEmpty(ID) || ID == "ID" ||
                    string.IsNullOrEmpty(Lastname) || Lastname == "Nachname" ||
                    string.IsNullOrEmpty(Firstname) || Firstname == "Vorname")
                {
                    return false;
                }

                if (Employee != null)
                {
                    if (ID == Employee.ID &&
                        Lastname == Employee.Lastname &&
                        Firstname == Employee.Firstname)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
    }
}
