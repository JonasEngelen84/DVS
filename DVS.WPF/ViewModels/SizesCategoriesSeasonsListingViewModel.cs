using DVS.Domain.Models;
using DVS.WPF.Stores;
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
        private void SeasonStore_SeasonAdded(Season newSeason)
        {
            _seasons.Add(newSeason);
        }
        private void SeasonStore_SeasonUpdated(Season editedSeason)
        {
            Season seasonToUpdate = _seasons.First(s => s.Id == editedSeason.Id);
            _seasons.Remove(seasonToUpdate);
            _seasons.Add(editedSeason);
        }
        private void SeasonStore_SeasonDeleted(Season seasonToDelete)
        {
            Season sToDelete = _seasons.First(s => s.Id == seasonToDelete.Id);
            _seasons.Remove(sToDelete);
        }
        
        private void LoadCategories()
        {
            _categories.Clear();

            foreach (Category category in _categoryStore.Categories)
            {
                _categories.Add(category);
            }
        }
        private void CategoryStore_CategoryAdded(Category newCategory)
        {
            _categories.Add(newCategory);
        }
        private void CategoryStore_CategoryUpdated(Category editedCategory)
        {
            Category categoryToUpdate = _categories.First(c => c.Id == editedCategory.Id);
            _categories.Remove(categoryToUpdate);
            _categories.Add(editedCategory);
        }
        private void CategoryStore_CategoryDeleted(Category categoryToDelete)
        {
            var cToDelete = _categories.First(c => c.Id == categoryToDelete.Id);
            _categories.Remove(cToDelete);
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
