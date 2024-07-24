using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
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

            LoadSizes();
            CategoryStore_CategoriesLoaded();
            SeasonStore_SeasonsLoaded();

            _categoryStore.CategoriesLoaded += CategoryStore_CategoriesLoaded;
            _categoryStore.CategoryAdded += CategoryStore_CategoryAdded;
            _categoryStore.CategoryEdited += CategoryStore_CategoryEdited;
            _categoryStore.CategoryDeleted += CategoryStore_CategoryDeleted;
            _categoryStore.AllCategoriesDeleted += CategoryStore_AllCategoriesDeleted;

            _seasonStore.SeasonsLoaded += SeasonStore_SeasonsLoaded;
            _seasonStore.SeasonAdded += SeasonStore_SeasonAdded;
            _seasonStore.SeasonEdited += SeasonStore_SeasonEdited;
            _seasonStore.SeasonDeleted += SeasonStore_SeasonDeleted;
            _seasonStore.AllSeasonsDeleted += SeasonStore_AllSeasonsDeleted;
        }


        private void LoadSizes()
        {
            if ( _clothes != null)
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
        }


        private void SeasonStore_SeasonsLoaded()
        {
            _seasons.Clear();

            foreach (SeasonModel season in _seasonStore.Seasons)
            {
                _seasons.Add(season);
            }
        }

        private void SeasonStore_SeasonAdded(SeasonModel season,  AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            AddSeason(season, addEditSeasonFormViewModel);
            OnPropertyChanged(nameof(addEditSeasonFormViewModel.CanDeleteAll));
        }

        private void SeasonStore_SeasonEdited(SeasonModel season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            SeasonModel seasonToUpdate = _seasons.FirstOrDefault(y => y.GuidID == season.GuidID);

            if (seasonToUpdate != null)
            {
                int index = _seasons.IndexOf(seasonToUpdate);
                _seasons[index] = season;
                _seasonCollectionViewSource.View.Refresh();
                addEditSeasonFormViewModel.SelectedSeason = new(null, "Saison wählen");
                addEditSeasonFormViewModel.EditSeason = addEditSeasonFormViewModel.SelectedSeason.Name;
                OnPropertyChanged(nameof(addEditSeasonFormViewModel.CanEdit));
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Saison nicht möglich.");
            }
        }

        private void SeasonStore_SeasonDeleted(SeasonModel season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            var seasonToDelete = _seasons.FirstOrDefault(y => y.GuidID == season.GuidID);

            if (seasonToDelete != null)
            {
                _seasons.Remove(seasonToDelete);
                _seasonCollectionViewSource.View.Refresh();
                addEditSeasonFormViewModel.SelectedSeason = new(null, "Saison wählen");
                addEditSeasonFormViewModel.EditSeason = addEditSeasonFormViewModel.SelectedSeason.Name;
                //OnPropertyChanged(nameof(_addEditSeasonFormViewModel.CanDeleteAll));
            }
            else
            {
                throw new InvalidOperationException("Löschen der Saison nicht möglich.");
            }
        }

        private void SeasonStore_AllSeasonsDeleted(AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            if (_seasons != null)
            {
                _seasons.Clear();
                addEditSeasonFormViewModel.SelectedSeason = new(null, "Saison wählen");
                addEditSeasonFormViewModel.EditSeason = addEditSeasonFormViewModel.SelectedSeason.Name;
                //OnPropertyChanged(nameof(_addEditSeasonFormViewModel.CanDeleteAll));
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Kategorien nicht möglich.");
            }
        }

        private void AddSeason(SeasonModel newSeason, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            _seasons.Add(newSeason);
            _seasonCollectionViewSource.View.Refresh();
            addEditSeasonFormViewModel.AddNewSeason = "Neue Saison";
            OnPropertyChanged(nameof(Seasons));
        }


        private void CategoryStore_CategoriesLoaded()
        {
            _categories.Clear();

            foreach (CategoryModel category in _categoryStore.Categories)
            {
                _categories.Add(category);
            }
        }

        private void CategoryStore_CategoryAdded(CategoryModel category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            AddCategory(category, addEditCategoryFormViewModel);
            OnPropertyChanged(nameof(addEditCategoryFormViewModel.CanDeleteAll));
        }

        private void CategoryStore_CategoryEdited(CategoryModel category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            CategoryModel categoryToUpdate = _categories.FirstOrDefault(y => y.GuidID == category.GuidID);

            if (categoryToUpdate != null)
            {
                int index = _categories.IndexOf(categoryToUpdate);
                _categories[index] = category;
                _categoryCollectionViewSource.View.Refresh();
                addEditCategoryFormViewModel.SelectedCategory = new(null, "Kategorie wählen");
                addEditCategoryFormViewModel.EditCategory = addEditCategoryFormViewModel.SelectedCategory.Name;
                OnPropertyChanged(nameof(addEditCategoryFormViewModel.CanEdit));
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Kategorie nicht möglich.");
            }
        }

        private void CategoryStore_CategoryDeleted(CategoryModel category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            var categoryToDelete = _categories.FirstOrDefault(y => y.GuidID == category.GuidID);

            if (categoryToDelete != null)
            {
                _categories.Remove(categoryToDelete);
                _categoryCollectionViewSource.View.Refresh();
                addEditCategoryFormViewModel.SelectedCategory = new(null, "Kategorie wählen");
                addEditCategoryFormViewModel.EditCategory = addEditCategoryFormViewModel.SelectedCategory.Name;
                OnPropertyChanged(nameof(addEditCategoryFormViewModel.CanDeleteAll));
            }
            else
            {
                throw new InvalidOperationException("Löschen der Kategorie nicht möglich.");
            }
        }

        private void CategoryStore_AllCategoriesDeleted(AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            if (_categories != null)
            {
                _categories.Clear();
                addEditCategoryFormViewModel.SelectedCategory = new(null, "Kategorie wählen");
                addEditCategoryFormViewModel.EditCategory = addEditCategoryFormViewModel.SelectedCategory.Name;
                OnPropertyChanged(nameof(addEditCategoryFormViewModel.CanDeleteAll));
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Kategorien nicht möglich.");
            }
        }

        private void AddCategory(CategoryModel newCategory, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            _categories.Add(newCategory);
            _categoryCollectionViewSource.View.Refresh();
            addEditCategoryFormViewModel.AddNewCategory = "Neue Kategorie";
            OnPropertyChanged(nameof(Categories));
        }


        protected override void Dispose()
        {
            _categoryStore.CategoriesLoaded -= CategoryStore_CategoriesLoaded;
            _categoryStore.CategoryAdded -= CategoryStore_CategoryAdded;
            _categoryStore.CategoryEdited -= CategoryStore_CategoryEdited;
            _categoryStore.CategoryDeleted -= CategoryStore_CategoryDeleted;
            _categoryStore.AllCategoriesDeleted += CategoryStore_AllCategoriesDeleted;

            _seasonStore.SeasonsLoaded -= SeasonStore_SeasonsLoaded;
            _seasonStore.SeasonAdded -= SeasonStore_SeasonAdded;
            _seasonStore.SeasonEdited -= SeasonStore_SeasonEdited;
            _seasonStore.SeasonDeleted -= SeasonStore_SeasonDeleted;
            _seasonStore.AllSeasonsDeleted -= SeasonStore_AllSeasonsDeleted;

            base.Dispose();
        }
    }
}
