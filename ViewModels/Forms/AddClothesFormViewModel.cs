using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddClothesFormViewModel : ViewModelBase
    {
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
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

        private string _size;
        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedSeason;
        public string SelectedSeason
        {
            get => _selectedSeason;
            set
            {
                if (_selectedSeason != value)
                {
                    _selectedSeason = value;
                    OnPropertyChanged();
                }
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

        private readonly ObservableCollection<string> _categories;
        public IEnumerable<string> Categories => _categories;

        private readonly ObservableCollection<string> _seasons;
        public IEnumerable<string> Seasons => _seasons;

        //private readonly ObservableCollection<ClothesModel> _clothes;
        //public IEnumerable<ClothesModel> Clothes => _clothes;

        public AddEditClothes_ClothesListviewViewModel AddEditClothes_ClothesListViewViewModel { get; }

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
            AddEditClothes_ClothesListViewViewModel = new(clothesStore);
            
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _clothesStore = clothesStore;
            OpenAddEditCategoriesCommand = openAddEditCategoriesCommand;
            OpenAddEditSeasonsCommand = openAddEditSeasonsCommand;
            AddClothesCommand = addClothesCommand;
            CancelClothesCommand = cancelClothesCommand;

            _categories = [];
            _seasons = [];

            CategoryStore_CategoriesLoaded();
            SeasonStore_SeasonsLoaded();

            _clothesStore.ClothesAdded += ClothesStore_AddClothes;

        }


        protected override void Dispose()
        {
            _clothesStore.ClothesAdded -= ClothesStore_AddClothes;

            base.Dispose();
        }

        private void CategoryStore_CategoriesLoaded()
        {
            _categories.Clear();

            foreach (string category in _categoryStore.Categories)
            {
                _categories.Add(category);
            }
        }

        private void SeasonStore_SeasonsLoaded()
        {
            _seasons.Clear();

            foreach (string season in _seasonStore.Seasons)
            {
                _seasons.Add(season);
            }
        }

        private void ClothesStore_AddClothes(ClothesModel clothes)
        {
            Id = null;
            Name = null;
            Size = null;
            //Quantity = null;
            Comment = null;
            SelectedCategory = null;
            SelectedSeason = null;
            Comment = null;
        }
    }
}
