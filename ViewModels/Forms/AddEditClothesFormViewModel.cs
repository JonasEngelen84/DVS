using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
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
                if (_iD != value)
                {
                    _iD = value;
                    OnPropertyChanged(nameof(ID));
                    OnPropertyChanged(nameof(CanSubmit));
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
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
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }

        private CategoryModel _category;
        public CategoryModel Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(nameof(CanSubmit));
                }
            }
        }

        private SeasonModel _season;
        public SeasonModel Season
        {
            get => _season;
            set
            {
                if (_season != value)
                {
                    _season = value;
                    OnPropertyChanged(nameof(CanSubmit));
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
                if (value != _isSubmitting)
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
                if (value != _errorMessage)
                {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                    OnPropertyChanged(nameof(HasErrorMessage));
                }
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public bool CanSubmit => !string.IsNullOrEmpty(ID) &&
                                 ID != "ID" &&
                                 !string.IsNullOrEmpty(Name) &&
                                 Name != "Name" &&
                                 Category != null &&
                                 Season != null;

        private readonly ObservableCollection<CategoryModel> _categories = [];
        private readonly CollectionViewSource _categoryCollectionViewSource;
        public ICollectionView Categories => _categoryCollectionViewSource.View;

        private readonly ObservableCollection<SeasonModel> _seasons = [];
        private readonly CollectionViewSource _seasonCollectionViewSource;
        public ICollectionView Seasons => _seasonCollectionViewSource.View;

        //TODO: Sizes in Stores implementieren
        private  ObservableCollection<ClothesSizeModel> _availableSizesEU =
        [
            new ClothesSizeModel(Guid.NewGuid(), "44"),
            new ClothesSizeModel(Guid.NewGuid(), "46"),
            new ClothesSizeModel(Guid.NewGuid(), "48"),
            new ClothesSizeModel(Guid.NewGuid(), "50"),
            new ClothesSizeModel(Guid.NewGuid(), "52"),
            new ClothesSizeModel(Guid.NewGuid(), "54"),
            new ClothesSizeModel(Guid.NewGuid(), "56"),
            new ClothesSizeModel(Guid.NewGuid(), "58"),
            new ClothesSizeModel(Guid.NewGuid(), "60"),
            new ClothesSizeModel(Guid.NewGuid(), "62"),
        ];
        public ObservableCollection<ClothesSizeModel> AvailableSizesEU
        {
            get => _availableSizesEU;
            set
            {
                if (_availableSizesEU != value)
                {
                    _availableSizesEU = value;
                    OnPropertyChanged(nameof(AvailableSizesEU));
                }
            }
        }

        private  ObservableCollection<ClothesSizeModel> _availableSizesUS =
        [
            new ClothesSizeModel(Guid.NewGuid(), "XS"),
            new ClothesSizeModel(Guid.NewGuid(), "S"),
            new ClothesSizeModel(Guid.NewGuid(), "M"),
            new ClothesSizeModel(Guid.NewGuid(), "L"),
            new ClothesSizeModel(Guid.NewGuid(), "XL"),
            new ClothesSizeModel(Guid.NewGuid(), "XLL"),
            new ClothesSizeModel(Guid.NewGuid(), "3XL"),
            new ClothesSizeModel(Guid.NewGuid(), "4XL"),
            new ClothesSizeModel(Guid.NewGuid(), "5XL"),
            new ClothesSizeModel(Guid.NewGuid(), "6XL")
        ];
        public ObservableCollection<ClothesSizeModel> AvailableSizesUS
        {
            get => _availableSizesUS;
            set
            {
                if (_availableSizesEU != value)
                {
                    _availableSizesUS = value;
                    OnPropertyChanged(nameof(AvailableSizesUS));
                }
            }
        }

        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;
        private readonly ClothesStore _clothesStore;

        public ICommand OpenAddEditCategoriesCommand { get; }
        public ICommand OpenAddEditSeasonsCommand { get; }
        public ICommand SubmitCommand { get; }


        public AddEditClothesFormViewModel(CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore,
            ICommand openAddEditCategoriesCommand, ICommand openAddEditSeasonsCommand, ICommand submitCommand)
        {
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;
            _clothesStore = clothesStore;
            OpenAddEditCategoriesCommand = openAddEditCategoriesCommand;
            OpenAddEditSeasonsCommand = openAddEditSeasonsCommand;
            SubmitCommand = submitCommand;

            _categoryCollectionViewSource = new CollectionViewSource { Source = _categories };
            _categoryCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(CategoryModel.Name), ListSortDirection.Ascending));

            _seasonCollectionViewSource = new CollectionViewSource { Source = _seasons };
            _seasonCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(SeasonModel.Name), ListSortDirection.Ascending));

            CategoryStore_LoadCategories();
            SeasonStore_LoadSeasons();

            _categoryStore.CategoriesLoaded += CategoryStore_LoadCategories;
            _seasonStore.SeasonsLoaded += SeasonStore_LoadSeasons;

        }


        public void LoadSizes(ClothesModel clothesModel)
        {
            foreach (var size in clothesModel.Sizes)
            {
                var matchingSizeEU = _availableSizesEU.FirstOrDefault(s => s.Size == size.Size);
                if (matchingSizeEU != null)
                {
                    matchingSizeEU.IsSelected = true;
                    matchingSizeEU.Quantity = size.Quantity;
                }

                var matchingSizeUS = _availableSizesUS.FirstOrDefault(s => s.Size == size.Size);
                if (matchingSizeUS != null)
                {
                    matchingSizeUS.IsSelected = true;
                    matchingSizeUS.Quantity = size.Quantity;
                }
            }
        }

        private void CategoryStore_LoadCategories()
        {
            _categories.Clear();

            foreach (CategoryModel category in _categoryStore.Categories)
            {
                _categories.Add(category);
            }
        }

        private void SeasonStore_LoadSeasons()
        {
            _seasons.Clear();

            foreach (SeasonModel season in _seasonStore.Seasons)
            {
                _seasons.Add(season);
            }
        }

        public void Dispose()
        {
            _categoryStore.CategoriesLoaded -= CategoryStore_LoadCategories;
            _seasonStore.SeasonsLoaded -= SeasonStore_LoadSeasons;
        }
    }
}
