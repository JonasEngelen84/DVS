using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace DVS.WPF.ViewModels
{
    public class AddEditClothesListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Category> _categories = [];
        private readonly CollectionViewSource _categoryCollectionViewSource;
        public ICollectionView Categories => _categoryCollectionViewSource.View;

        private readonly ObservableCollection<Season> _seasons = [];
        private readonly CollectionViewSource _seasonCollectionViewSource;
        public ICollectionView Seasons => _seasonCollectionViewSource.View;

        private ObservableCollection<SizeModel> _availableSizesEU = [];
        public ObservableCollection<SizeModel> AvailableSizesEU
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

        private ObservableCollection<SizeModel> _availableSizesUS = [];
        public ObservableCollection<SizeModel> AvailableSizesUS
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

        private readonly Clothes _clothes;
        private readonly  SizeStore _sizeStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;


        public AddEditClothesListingViewModel(Clothes clothes,
                                              SizeStore sizeStore,
                                              CategoryStore categoryStore,
                                              SeasonStore seasonStore)
        {
            _clothes = clothes;
            _sizeStore = sizeStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;

            _categoryCollectionViewSource = new CollectionViewSource { Source = _categories };
            _categoryCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(Category.Name), ListSortDirection.Ascending));

            _seasonCollectionViewSource = new CollectionViewSource { Source = _seasons };
            _seasonCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(Season.Name), ListSortDirection.Ascending));

            CategoryStore_CategoriesLoaded();
            SeasonStore_SeasonsLoaded();
            SizesLoaded();

            _categoryStore.CategoriesLoaded += CategoryStore_CategoriesLoaded;
            _categoryStore.CategoryAdded += CategoryStore_CategoryAdded;
            _categoryStore.CategoryUpdated += CategoryStore_CategoryUpdated;
            _categoryStore.CategoryDeleted += CategoryStore_CategoryDeleted;

            _seasonStore.SeasonsLoaded += SeasonStore_SeasonsLoaded;
            _seasonStore.SeasonAdded += SeasonStore_SeasonAdded;
            _seasonStore.SeasonUpdated += SeasonStore_SeasonUpdated;
            _seasonStore.SeasonDeleted += SeasonStore_SeasonDeleted;
        }


        private void SizesLoaded()
        {
            _availableSizesEU.Clear();
            _availableSizesUS.Clear();

            foreach (SizeModel size in _sizeStore.Sizes)
            {
                // Wenn eine Clothes-Instanz übergeben wurde => prüfen ob sie die aktuelle Größe beinhaltet
                var matchingSize = _clothes?.Sizes.FirstOrDefault(s => s.Size.Size == size.Size);

                if (matchingSize != null)
                {
                    size.IsSelected = true;
                    size.Quantity = matchingSize.Quantity;
                }
                else
                {
                    size.IsSelected = false;
                    size.Quantity = 0;
                }

                if (size.IsSizeSystemEU)
                    _availableSizesEU.Add(size);
                else
                    _availableSizesUS.Add(size);
            }
        }


        private void SeasonStore_SeasonsLoaded()
        {
            _seasons.Clear();

            foreach (Season season in _seasonStore.Seasons)
            {
                _seasons.Add(season);
            }
        }

        private void SeasonStore_SeasonAdded(Season newSeason,  AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            _seasons.Add(newSeason);
            _seasonCollectionViewSource.View.Refresh();
            addEditSeasonFormViewModel.AddNewSeason = "Neue Saison";
        }

        private void SeasonStore_SeasonUpdated(Season season, AddEditSeasonFormViewModel? addEditSeasonFormViewModel)
        {
            Season seasonToUpdate = _seasons.FirstOrDefault(y => y.GuidID == season.GuidID);

            if (seasonToUpdate != null)
            {
                int index = _seasons.IndexOf(seasonToUpdate);
                _seasons[index] = season;
                _seasonCollectionViewSource.View.Refresh();

                if (addEditSeasonFormViewModel != null)
                {
                    addEditSeasonFormViewModel.SelectedSeason = new(Guid.NewGuid(), "Saison wählen");
                    addEditSeasonFormViewModel.EditSelectedSeason = addEditSeasonFormViewModel.SelectedSeason.Name;
                    OnPropertyChanged(nameof(addEditSeasonFormViewModel.CanEdit));
                }
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Saison nicht möglich.");
            }
        }

        private void SeasonStore_SeasonDeleted(Guid guidID, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            var seasonToDelete = _seasons.FirstOrDefault(y => y.GuidID == guidID);

            if (seasonToDelete != null)
            {
                _seasons.Remove(seasonToDelete);
                _seasonCollectionViewSource.View.Refresh();
                addEditSeasonFormViewModel.SelectedSeason = new(Guid.NewGuid(), "Saison wählen");
                addEditSeasonFormViewModel.EditSelectedSeason = addEditSeasonFormViewModel.SelectedSeason.Name;
            }
            else
            {
                throw new InvalidOperationException("Löschen der Saison nicht möglich.");
            }
        }

        
        private void CategoryStore_CategoriesLoaded()
        {
            _categories.Clear();

            foreach (Category category in _categoryStore.Categories)
            {
                _categories.Add(category);
            }
        }

        private void CategoryStore_CategoryAdded(Category newCategory, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            _categories.Add(newCategory);
            _categoryCollectionViewSource.View.Refresh();
            addEditCategoryFormViewModel.AddNewCategory = "Neue Kategorie";
        }

        private void CategoryStore_CategoryUpdated(Category category, AddEditCategoryFormViewModel? addEditCategoryFormViewModel)
        {
            Category categoryToUpdate = _categories.FirstOrDefault(y => y.GuidID == category.GuidID);

            if (categoryToUpdate != null)
            {
                int index = _categories.IndexOf(categoryToUpdate);
                _categories[index] = category;
                _categoryCollectionViewSource.View.Refresh();

                if (addEditCategoryFormViewModel != null)
                {
                    addEditCategoryFormViewModel.SelectedCategory = new(Guid.NewGuid(), "Kategorie wählen");
                    addEditCategoryFormViewModel.EditSelectedCategory = addEditCategoryFormViewModel.SelectedCategory.Name;
                    OnPropertyChanged(nameof(addEditCategoryFormViewModel.CanEdit));
                }
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Kategorie nicht möglich.");
            }
        }

        private void CategoryStore_CategoryDeleted(Guid guidID, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            var categoryToDelete = _categories.FirstOrDefault(y => y.GuidID == guidID);

            if (categoryToDelete != null)
            {
                _categories.Remove(categoryToDelete);
                _categoryCollectionViewSource.View.Refresh();
                addEditCategoryFormViewModel.SelectedCategory = new(Guid.NewGuid(), "Kategorie wählen");
                addEditCategoryFormViewModel.EditSelectedCategory = addEditCategoryFormViewModel.SelectedCategory.Name;
            }
            else
            {
                throw new InvalidOperationException("Löschen der Kategorie nicht möglich.");
            }
        }


        protected override void Dispose()
        {
            _categoryStore.CategoriesLoaded -= CategoryStore_CategoriesLoaded;
            _categoryStore.CategoryAdded -= CategoryStore_CategoryAdded;
            _categoryStore.CategoryUpdated -= CategoryStore_CategoryUpdated;
            _categoryStore.CategoryDeleted -= CategoryStore_CategoryDeleted;

            _seasonStore.SeasonsLoaded -= SeasonStore_SeasonsLoaded;
            _seasonStore.SeasonAdded -= SeasonStore_SeasonAdded;
            _seasonStore.SeasonUpdated -= SeasonStore_SeasonUpdated;
            _seasonStore.SeasonDeleted -= SeasonStore_SeasonDeleted;

            base.Dispose();
        }
    }
}
