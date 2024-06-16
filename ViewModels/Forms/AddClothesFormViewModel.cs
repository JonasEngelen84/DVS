using DVS.Models;
using DVS.Stores;
using DVS.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddClothesFormViewModel : ViewModelBase
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

        private readonly ObservableCollection<SizeOption> _availableSizesEU =
        [
            new SizeOption { Size = "44" },
            new SizeOption { Size = "46" },
            new SizeOption { Size = "48" },
            new SizeOption { Size = "50" },
            new SizeOption { Size = "52" },
            new SizeOption { Size = "54" },
            new SizeOption { Size = "56" },
            new SizeOption { Size = "58" },
            new SizeOption { Size = "60" },
            new SizeOption { Size = "62" }
        ];
        public ObservableCollection<SizeOption> AvailableSizesEU => _availableSizesEU;

        private readonly ObservableCollection<SizeOption> _availableSizesUS =
        [
            new SizeOption { Size = "XS" },
            new SizeOption { Size = "S" },
            new SizeOption { Size = "M" },
            new SizeOption { Size = "L" },
            new SizeOption { Size = "XL" },
            new SizeOption { Size = "XLL" },
            new SizeOption { Size = "3XL" },
            new SizeOption { Size = "4XL" },
            new SizeOption { Size = "5XL" },
            new SizeOption { Size = "6XL" }
        ];
        public ObservableCollection<SizeOption> AvailableSizesUS => _availableSizesUS;

        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly ClothesStore _clothesStore;

        public ICommand OpenAddEditCategoriesCommand { get; }
        public ICommand OpenAddEditSeasonsCommand { get; }
        public ICommand AddClothesCommand { get; }
        public ICommand CancelClothesCommand { get; }


        public AddClothesFormViewModel(CategoryStore categoryStore,
                                       SeasonStore seasonStore,
                                       ClothesStore clothesStore,
                                       ICommand openAddEditCategoriesCommand,
                                       ICommand openAddEditSeasonsCommand,
                                       ICommand addClothesCommand,
                                       ICommand cancelClothesCommand)
        {
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _clothesStore = clothesStore;
            OpenAddEditCategoriesCommand = openAddEditCategoriesCommand;
            OpenAddEditSeasonsCommand = openAddEditSeasonsCommand;
            AddClothesCommand = addClothesCommand;
            CancelClothesCommand = cancelClothesCommand;

            _categories = [];
            _seasons = [];

            _iD = "ID";
            _name = "Name";
            _comment = "Kommentar";

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
