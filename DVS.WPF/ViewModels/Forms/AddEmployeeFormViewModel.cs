using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class AddEmployeeFormViewModel(
        AddEditEmployeeListingViewModel addEditEmployeeListingViewModel,
        ICommand submitCommand)
        : ViewModelBase
    {
        public AddEditEmployeeListingViewModel AddEditEmployeeListingViewModel { get; } = addEditEmployeeListingViewModel;

        public ICommand SubmitEmployee { get; } = submitCommand;

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                if (Id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
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

        private string _comment = "";
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

        public bool CanSubmit
        {
            get
            {
                if (string.IsNullOrEmpty(Id) || Id == "Id" ||
                    string.IsNullOrEmpty(Lastname) || Lastname == "Nachname" ||
                    string.IsNullOrEmpty(Firstname) || Firstname == "Vorname")
                {
                    return false;
                }

                return true;
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

        public bool HasError;
    }
}
