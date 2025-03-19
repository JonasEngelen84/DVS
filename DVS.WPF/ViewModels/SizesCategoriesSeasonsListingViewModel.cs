using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListingItems;
using System.Collections.ObjectModel;

namespace DVS.WPF.ViewModels
{
    public class SizesCategoriesSeasonsListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Category> _categories = [];
        public IEnumerable<Category> Categories => _categories;

        private readonly ObservableCollection<Season> _seasons = [];
        public IEnumerable<Season> Seasons => _seasons;

        private readonly List<string> _sizesEU;
        private readonly ObservableCollection<SizeListingItemViewModel> _loadedSizesEU = [];
        public IEnumerable<SizeListingItemViewModel> LoadedSizesEU => _loadedSizesEU;
        
        private readonly List<string> _sizesUS;
        private readonly ObservableCollection<SizeListingItemViewModel> _loadedSizesUS = [];
        public IEnumerable<SizeListingItemViewModel> LoadedSizesUS => _loadedSizesUS;

        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;


        public SizesCategoriesSeasonsListingViewModel(Clothes? clothes, CategoryStore categoryStore, SeasonStore seasonStore)
        {
            _sizesEU = [ "30", "32", "34", "36", "38", "40", "42", "44", "46", "48", "50", "52", "54", "56", "58", "60", "62" ];            
            _sizesUS = [ "XS", "S", "M", "L", "XL", "XXL", "3XL", "4XL", "5XL", "6XL" ];

            _categoryStore = categoryStore;
            _seasonStore = seasonStore;

            LoadSizes(clothes);
            LoadSeasons();
            LoadCategories();

            _categoryStore.CategoryAdded += CategoryStore_CategoryAdded;
            _categoryStore.CategoryUpdated += CategoryStore_CategoryUpdated;
            _categoryStore.CategoryDeleted += CategoryStore_CategoryDeleted;

            _seasonStore.SeasonAdded += SeasonStore_SeasonAdded;
            _seasonStore.SeasonUpdated += SeasonStore_SeasonUpdated;
            _seasonStore.SeasonDeleted += SeasonStore_SeasonDeleted;
        }


        private void LoadSizes(Clothes? clothes)
        {
            if (clothes != null)
            {
                foreach (var size in _sizesEU)
                {
                    ClothesSize? selectedSize = clothes.Sizes.FirstOrDefault(cs => cs.Size == size);
                    if (selectedSize != null)
                        _loadedSizesEU.Add(new SizeListingItemViewModel(size)
                        {
                            Quantity = selectedSize.Quantity,
                            Comment = selectedSize.Comment,
                            IsChecked = true
                        });
                    else
                        _loadedSizesEU.Add(new SizeListingItemViewModel(size));
                }

                foreach (var size in _sizesUS)
                {
                    ClothesSize? selectedSize = clothes.Sizes.FirstOrDefault(cs => cs.Size == size);
                    if (selectedSize != null)
                        _loadedSizesUS.Add(new SizeListingItemViewModel(size)
                        {
                            Quantity = selectedSize.Quantity,
                            Comment = selectedSize.Comment,
                            IsChecked = true
                        });
                    else
                        _loadedSizesUS.Add(new SizeListingItemViewModel(size));
                }
            }
            else
            {
                foreach (var size in _sizesEU)
                {
                    _loadedSizesEU.Add(new SizeListingItemViewModel(size));
                }

                foreach (var size in _sizesUS)
                {
                    _loadedSizesUS.Add(new SizeListingItemViewModel(size));
                }
            }
        }

        private void LoadSeasons()
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
            addEditSeasonFormViewModel.AddNewSeason = "Neue Saison";
        }
        private void SeasonStore_SeasonUpdated(Season season, AddEditSeasonFormViewModel? addEditSeasonFormViewModel)
        {
            Season seasonToUpdate = _seasons.First(y => y.Id == season.Id);

            if (seasonToUpdate != null)
            {
                int index = _seasons.IndexOf(seasonToUpdate);
                _seasons[index] = season;

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
        private void SeasonStore_SeasonDeleted(Guid GuidId, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            var seasonToDelete = _seasons.FirstOrDefault(y => y.Id == GuidId);

            if (seasonToDelete != null)
            {
                _seasons.Remove(seasonToDelete);
                addEditSeasonFormViewModel.SelectedSeason = new(Guid.NewGuid(), "Saison wählen");
                addEditSeasonFormViewModel.EditSelectedSeason = addEditSeasonFormViewModel.SelectedSeason.Name;
            }
            else
            {
                throw new InvalidOperationException("Löschen der Saison nicht möglich.");
            }
        }
        
        private void LoadCategories()
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
            addEditCategoryFormViewModel.AddNewCategory = "Neue Kategorie";
        }
        private void CategoryStore_CategoryUpdated(Category category, AddEditCategoryFormViewModel? addEditCategoryFormViewModel)
        {
            Category categoryToUpdate = _categories.FirstOrDefault(y => y.Id == category.Id);

            if (categoryToUpdate != null)
            {
                int index = _categories.IndexOf(categoryToUpdate);
                _categories[index] = category;

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
        private void CategoryStore_CategoryDeleted(Guid GuidId, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            var categoryToDelete = _categories.FirstOrDefault(y => y.Id == GuidId);

            if (categoryToDelete != null)
            {
                _categories.Remove(categoryToDelete);
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
            _categoryStore.CategoryAdded -= CategoryStore_CategoryAdded;
            _categoryStore.CategoryUpdated -= CategoryStore_CategoryUpdated;
            _categoryStore.CategoryDeleted -= CategoryStore_CategoryDeleted;

            _seasonStore.SeasonAdded -= SeasonStore_SeasonAdded;
            _seasonStore.SeasonUpdated -= SeasonStore_SeasonUpdated;
            _seasonStore.SeasonDeleted -= SeasonStore_SeasonDeleted;

            base.Dispose();
        }
    }
}
