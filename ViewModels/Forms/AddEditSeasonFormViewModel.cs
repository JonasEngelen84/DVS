using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditSeasonFormViewModel : ViewModelBase
    {
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
        public bool CanDeleteAll => _seasons.Count > 0;
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private readonly ObservableCollection<SeasonModel> _seasons = [];
        private readonly CollectionViewSource _seasonCollectionViewSource;
        public ICollectionView Seasons => _seasonCollectionViewSource.View;

        private readonly SeasonStore _seasonStore;

        public ICommand AddSeasonCommand { get; }
        public ICommand EditSeasonCommand { get; }
        public ICommand DeleteSeasonCommand { get; }
        public ICommand ClearSeasonListCommand { get; }


        public AddEditSeasonFormViewModel(SeasonStore seasonStore,
                                          ICommand addSeasonCommand,
                                          ICommand editSeasonCommand,
                                          ICommand deleteSeasonCommand,
                                          ICommand clearSeasonListCommand)
        {
            _seasonStore = seasonStore;
            AddSeasonCommand = addSeasonCommand;
            EditSeasonCommand = editSeasonCommand;
            DeleteSeasonCommand = deleteSeasonCommand;
            ClearSeasonListCommand = clearSeasonListCommand;

            AddNewSeason = "Neue Saison";
            EditSeason = "Saison wählen";
            SelectedSeason = new(Guid.NewGuid(), "Saison wählen");

            _seasonCollectionViewSource = new CollectionViewSource { Source = _seasons };
            _seasonCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(SeasonModel.Name), ListSortDirection.Ascending));

            SeasonStore_SeasonsLoaded();

            _seasonStore.SeasonsLoaded += SeasonStore_SeasonsLoaded;
            _seasonStore.SeasonAdded += SeasonStore_SeasonAdded;
            _seasonStore.SeasonEdited += SeasonStore_SeasonEdited;
            _seasonStore.SeasonDeleted += SeasonStore_SeasonDeleted;
            _seasonStore.AllSeasonsDeleted += SeasonStore_AllSeasonsDeleted;
        }


        private void SeasonStore_SeasonsLoaded()
        {
            _seasons.Clear();

            foreach (SeasonModel season in _seasonStore.Seasons)
            {
                _seasons.Add(season);
            }
        }

        private void SeasonStore_SeasonAdded(SeasonModel season)
        {
            AddSeason(season);
            OnPropertyChanged(nameof(CanDeleteAll));
        }

        private void SeasonStore_SeasonEdited(SeasonModel season)
        {
            SeasonModel seasonToUpdate = _seasons.FirstOrDefault(y => y.GuidID == season.GuidID);

            if (seasonToUpdate != null)
            {
                int index = _seasons.IndexOf(seasonToUpdate);
                _seasons[index] = season;
                _seasonCollectionViewSource.View.Refresh();
                SelectedSeason = new(Guid.NewGuid(), "Saison wählen");
                EditSeason = SelectedSeason.Name;
                OnPropertyChanged(nameof(CanEdit));
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Saison nicht möglich.");
            }
        }

        private void SeasonStore_SeasonDeleted(SeasonModel season)
        {
            var seasonToDelete = _seasons.FirstOrDefault(y => y.GuidID == season.GuidID);

            if (seasonToDelete != null)
            {
                _seasons.Remove(seasonToDelete);
                _seasonCollectionViewSource.View.Refresh();
                SelectedSeason = new(Guid.NewGuid(), "Saison wählen");
                EditSeason = SelectedSeason.Name;
                OnPropertyChanged(nameof(CanDeleteAll));
            }
            else
            {
                throw new InvalidOperationException("Löschen der Saison nicht möglich.");
            }
        }

        private void SeasonStore_AllSeasonsDeleted()
        {
            if (_seasons != null)
            {
                _seasons.Clear();
                SelectedSeason = new(Guid.NewGuid(), "Saison wählen");
                EditSeason = SelectedSeason.Name;
                OnPropertyChanged(nameof(CanDeleteAll));
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Kategorien nicht möglich.");
            }
        }

        private void AddSeason(SeasonModel season)
        {
            _seasons.Add(season);
            _seasonCollectionViewSource.View.Refresh();
            AddNewSeason = "Neue Saison";
            OnPropertyChanged(nameof(Seasons));
        }
        
        protected override void Dispose()
        {
            _seasonStore.SeasonsLoaded -= SeasonStore_SeasonsLoaded;
            _seasonStore.SeasonAdded -= SeasonStore_SeasonAdded;
            _seasonStore.SeasonEdited -= SeasonStore_SeasonEdited;
            _seasonStore.SeasonDeleted -= SeasonStore_SeasonDeleted;
            _seasonStore.AllSeasonsDeleted -= SeasonStore_AllSeasonsDeleted;

            base.Dispose();
        }
    }
}
