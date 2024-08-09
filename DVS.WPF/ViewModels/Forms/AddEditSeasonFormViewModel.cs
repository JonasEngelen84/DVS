using DVS.Domain.Models;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class AddEditSeasonFormViewModel(ICommand addSeasonCommand, ICommand editSeasonCommand,
        ICommand deleteSeasonCommand, ICommand clearSeasonListCommand,
        AddEditListingViewModel addEditListingViewModel) : ViewModelBase
    {
        public AddEditListingViewModel AddEditListingViewModel { get; } = addEditListingViewModel;
        public ICommand AddSeason { get; } = addSeasonCommand;
        public ICommand EditSeason { get; } = editSeasonCommand;
        public ICommand DeleteSeason { get; } = deleteSeasonCommand;
        public ICommand ClearSeasonList { get; } = clearSeasonListCommand;

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

        private string _editSelectedSeason;
        public string EditSelectedSeason
        {
            get => _editSelectedSeason;
            set
            {
                _editSelectedSeason = value;
                OnPropertyChanged(nameof(EditSelectedSeason));
                OnPropertyChanged(nameof(CanEdit));
            }
        }

        private Season _selectedSeason;
        public Season SelectedSeason
        {
            get => _selectedSeason;
            set
            {
                if (value != null)
                {
                    _selectedSeason = value;
                    EditSelectedSeason = new(value.Name);
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
            !string.IsNullOrEmpty(EditSelectedSeason) &&
            !SelectedSeason.Name.Equals("Saison wählen") &&
            !SelectedSeason.Name.Equals(EditSelectedSeason);

        public bool CanDelete => !SelectedSeason.Name.Equals("Saison wählen");
        public bool CanDeleteAll => !AddEditListingViewModel.Seasons.IsEmpty;
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public void SeasonCollectionChanged()
        {
            OnPropertyChanged(nameof(CanDeleteAll));
        }
    }
}
