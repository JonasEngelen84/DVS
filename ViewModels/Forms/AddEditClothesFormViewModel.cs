using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditClothesFormViewModel : ViewModelBase
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

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
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

        private string _category;
        public string Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _season;
        public string Season
        {
            get => _season;
            set
            {
                if (_season != value)
                {
                    _season = value;
                    OnPropertyChanged();
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

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        //TODO: CanSubmit
        //public bool CanSubmit => !string.IsNullOrEmpty(Username);

        private readonly ObservableCollection<string> _categories;
        public IEnumerable<string> Categories => _categories;

        private readonly ObservableCollection<string> _seasons;
        public IEnumerable<string> Seasons => _seasons;

        //TODO: Sizes in Stores implementieren
        private readonly ObservableCollection<ClothesSizeModel> _availableSizesEU =
        [
            new ClothesSizeModel { Size = "44" },
            new ClothesSizeModel { Size = "46" },
            new ClothesSizeModel { Size = "48" },
            new ClothesSizeModel { Size = "50" },
            new ClothesSizeModel { Size = "52" },
            new ClothesSizeModel { Size = "54" },
            new ClothesSizeModel { Size = "56" },
            new ClothesSizeModel { Size = "58" },
            new ClothesSizeModel { Size = "60" },
            new ClothesSizeModel { Size = "62" }
        ];
        public ObservableCollection<ClothesSizeModel> AvailableSizesEU => _availableSizesEU;

        private readonly ObservableCollection<ClothesSizeModel> _availableSizesUS =
        [
            new ClothesSizeModel { Size = "XS" },
            new ClothesSizeModel { Size = "S" },
            new ClothesSizeModel { Size = "M" },
            new ClothesSizeModel { Size = "L" },
            new ClothesSizeModel { Size = "XL" },
            new ClothesSizeModel { Size = "XLL" },
            new ClothesSizeModel { Size = "3XL" },
            new ClothesSizeModel { Size = "4XL" },
            new ClothesSizeModel { Size = "5XL" },
            new ClothesSizeModel { Size = "6XL" }
        ];
        public ObservableCollection<ClothesSizeModel> AvailableSizesUS => _availableSizesUS;

        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly ClothesStore _clothesStore;

        public ICommand OpenAddEditCategoriesCommand { get; }
        public ICommand OpenAddEditSeasonsCommand { get; }
        public ICommand AddClothesCommand { get; }
        public ICommand EditClothesCommand { get; }
        public ICommand DeleteClothesCommand { get; }
        public ICommand ClearClothesListCommand { get; }


        public AddEditClothesFormViewModel(
            CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore,
            ICommand openAddEditCategoriesCommand, ICommand openAddEditSeasonsCommand, ICommand addClothesCommand,
            ICommand editClothesCommand, ICommand deleteClothesCommand, ICommand clearClothesListCommand)
        {
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _clothesStore = clothesStore;
            OpenAddEditCategoriesCommand = openAddEditCategoriesCommand;
            OpenAddEditSeasonsCommand = openAddEditSeasonsCommand;
            AddClothesCommand = addClothesCommand;
            EditClothesCommand = editClothesCommand;
            DeleteClothesCommand = deleteClothesCommand;
            ClearClothesListCommand = clearClothesListCommand;

            _categories = [];
            _seasons = [];

            LoadCategories();
            LoadSeasons();

        }


        private void LoadCategories()
        {
            foreach (string category in _categoryStore.Categories)
            {
                _categories.Add(category);
            }
        }

        private void LoadSeasons()
        {
            foreach (string season in _seasonStore.Seasons)
            {
                _seasons.Add(season);
            }
        }
    }
}
