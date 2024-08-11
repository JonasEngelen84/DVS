using DVS.Domain.Models;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class AddEditSeasonFormViewModel(ICommand addSeasonCommand, ICommand updateSeasonCommand,
        ICommand deleteSeasonCommand, ICommand clearSeasonListCommand,
        AddEditListingViewModel addEditListingViewModel) : ViewModelBase
    {
        public AddEditListingViewModel AddEditListingViewModel { get; } = addEditListingViewModel;
        public ICommand AddSeason { get; } = addSeasonCommand;
        public ICommand UpdateSeason { get; } = updateSeasonCommand;
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

        private string _updateSelectedSeason;
        public string UpdateSelectedSeason
        {
            get => _updateSelectedSeason;
            set
            {
                _updateSelectedSeason = value;
                OnPropertyChanged(nameof(UpdateSelectedSeason));
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
                    UpdateSelectedSeason = new(value.Name);
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
            !string.IsNullOrEmpty(UpdateSelectedSeason) &&
            !SelectedSeason.Name.Equals("Saison wählen") &&
            !SelectedSeason.Name.Equals(UpdateSelectedSeason);

        public bool CanDelete => !SelectedSeason.Name.Equals("Saison wählen");
        public bool CanDeleteAll => !AddEditListingViewModel.Seasons.IsEmpty;
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public void SeasonCollectionChanged()
        {
            OnPropertyChanged(nameof(CanDeleteAll));
        }
    }
}
