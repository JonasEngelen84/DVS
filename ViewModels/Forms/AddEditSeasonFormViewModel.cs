using DVS.Models;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditSeasonFormViewModel : ViewModelBase
    {
        private AddEditListingViewModel AddEditListingViewModel { get; }

        private string _addNewSeason;
        public string AddNewSeason
        {
            get => _addNewSeason;
            set
            {
                _addNewSeason = value;
                OnPropertyChanged(nameof(AddNewSeason));
                OnPropertyChanged(nameof(CanAdd));
            }
        }

        private string _editSeason;
        public string EditSeason
        {
            get => _editSeason;
            set
            {
                _editSeason = value;
                OnPropertyChanged(nameof(EditSeason));
                OnPropertyChanged(nameof(CanEdit));
            }
        }

        private SeasonModel _selectedSeason;
        public SeasonModel SelectedSeason
        {
            get => _selectedSeason;
            set
            {
                if (value != null)
                {
                    _selectedSeason = value;
                    EditSeason = new(value.Name);
                    OnPropertyChanged(nameof(SelectedSeason));
                    OnPropertyChanged(nameof(CanDelete));
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

        public bool CanAdd =>
            !string.IsNullOrEmpty(AddNewSeason) &&
            !AddNewSeason.Equals("Neue Saison");

        public bool CanEdit =>
            !string.IsNullOrEmpty(EditSeason) &&
            !SelectedSeason.Name.Equals("Saison wählen") &&
            !SelectedSeason.Name.Equals(EditSeason);

        public bool CanDelete => !SelectedSeason.Name.Equals("Saison wählen");
        public bool CanDeleteAll => !AddEditListingViewModel.Seasons.IsEmpty;
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand AddSeasonCommand { get; }
        public ICommand EditSeasonCommand { get; }
        public ICommand DeleteSeasonCommand { get; }
        public ICommand ClearSeasonListCommand { get; }


        public AddEditSeasonFormViewModel(ICommand addSeasonCommand, ICommand editSeasonCommand, ICommand deleteSeasonCommand,
            ICommand clearSeasonListCommand, AddEditListingViewModel addEditListingViewModel)
        {
            AddEditListingViewModel = addEditListingViewModel;
            AddSeasonCommand = addSeasonCommand;
            EditSeasonCommand = editSeasonCommand;
            DeleteSeasonCommand = deleteSeasonCommand;
            ClearSeasonListCommand = clearSeasonListCommand;
        }
    }
}
