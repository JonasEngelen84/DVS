using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace DVS.ViewModels
{
    public class AddEditListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CategoryModel> _categories = [];
        private readonly CollectionViewSource _categoryCollectionViewSource;
        public ICollectionView Categories => _categoryCollectionViewSource.View;

        private readonly ObservableCollection<SeasonModel> _seasons = [];
        private readonly CollectionViewSource _seasonCollectionViewSource;
        public ICollectionView Seasons => _seasonCollectionViewSource.View;

        private ObservableCollection<ClothesSizeModel> _availableSizesEU;
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

        private ObservableCollection<ClothesSizeModel> _availableSizesUS;
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

        private readonly ClothesModel _clothes;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;

        public AddEditListingViewModel(ClothesModel clothes, CategoryStore categoryStore, SeasonStore seasonStore)
        {
            _clothes = clothes;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;

            _categoryCollectionViewSource = new CollectionViewSource { Source = _categories };
            _categoryCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(CategoryModel.Name), ListSortDirection.Ascending));

            _seasonCollectionViewSource = new CollectionViewSource { Source = _seasons };
            _seasonCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(SeasonModel.Name), ListSortDirection.Ascending));

            _availableSizesEU =
            [
                new ClothesSizeModel("44"),
                new ClothesSizeModel("46"),
                new ClothesSizeModel("48"),
                new ClothesSizeModel("50"),
                new ClothesSizeModel("52"),
                new ClothesSizeModel("54"),
                new ClothesSizeModel("56"),
                new ClothesSizeModel("58"),
                new ClothesSizeModel("60"),
                new ClothesSizeModel("62")
            ];

            _availableSizesUS =
            [
                new ClothesSizeModel("XS"),
                new ClothesSizeModel("S"),
                new ClothesSizeModel("M"),
                new ClothesSizeModel("L"),
                new ClothesSizeModel("XL"),
                new ClothesSizeModel("XLL"),
                new ClothesSizeModel("3XL"),
                new ClothesSizeModel("4XL"),
                new ClothesSizeModel("5XL"),
                new ClothesSizeModel("6XL")
            ];

            LoadCategories();
            LoadSeasons();
        }


        public void LoadSizes()
        {
            foreach (var size in _clothes.Sizes)
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

        private void LoadCategories()
        {
            _categories.Clear();

            foreach (CategoryModel category in _categoryStore.Categories)
            {
                _categories.Add(category);
            }
        }

        private void LoadSeasons()
        {
            _seasons.Clear();

            foreach (SeasonModel season in _seasonStore.Seasons)
            {
                _seasons.Add(season);
            }
        }
    }
}
