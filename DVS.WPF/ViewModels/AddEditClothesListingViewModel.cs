using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Collections.ObjectModel;

namespace DVS.WPF.ViewModels
{
    public class AddEditClothesListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<Category> _categories = [];
        public IEnumerable<Category> Categories => _categories;

        private readonly ObservableCollection<Season> _seasons = [];
        public IEnumerable<Season> Seasons => _seasons;

        private readonly List<SizeModel> _availableSizesEU;
        public List<SizeModel> AvailableSizesEU => _availableSizesEU;
        

        private readonly List<SizeModel> _availableSizesUS;
        public List<SizeModel> AvailableSizesUS => _availableSizesUS;

        private readonly Clothes? _clothes;
        //private readonly  SizeStore _sizeStore;
        private readonly CategoryStore _categoryStore;
        private readonly SeasonStore _seasonStore;


        public AddEditClothesListingViewModel(
            Clothes? clothes,
            SizeStore sizeStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore)
        {
            _availableSizesEU =
            [
                new SizeModel("44"),
                new SizeModel("46"),
                new SizeModel("48"),
                new SizeModel("50"),
                new SizeModel("52"),
                new SizeModel("54"),
                new SizeModel("56"),
                new SizeModel("58"),
                new SizeModel("60"),
                new SizeModel("62"),
            ];
            
            _availableSizesUS =
            [
                new SizeModel("XS"),
                new SizeModel("S"),
                new SizeModel("M"),
                new SizeModel("L"),
                new SizeModel("XL"),
                new SizeModel("XXL"),
                new SizeModel("3XL"),
                new SizeModel("4XL"),
                new SizeModel("5XL"),
                new SizeModel("6XL"),
            ];

            _clothes = clothes;
            //_sizeStore = sizeStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;

            LoadSeasons();
            LoadCategories();

            _categoryStore.CategoryAdded += CategoryStore_CategoryAdded;
            _categoryStore.CategoryUpdated += CategoryStore_CategoryUpdated;
            _categoryStore.CategoryDeleted += CategoryStore_CategoryDeleted;

            _seasonStore.SeasonAdded += SeasonStore_SeasonAdded;
            _seasonStore.SeasonUpdated += SeasonStore_SeasonUpdated;
            _seasonStore.SeasonDeleted += SeasonStore_SeasonDeleted;
        }


        //private void LoadSizes()
        //{
        //    _availableSizesEU.Clear();
        //    _availableSizesUS.Clear();

        //    foreach (SizeModel size in _sizeStore.Sizes)
        //    {
        //        // Wenn eine Clothes-Instanz übergeben wurde => prüfen ob sie die aktuelle Größe beinhaltet
        //        var matchingSize = _clothes?.Sizes.FirstOrDefault(s => s.Size.Size == size.Size);

        //        if (matchingSize != null)
        //        {
        //            size.IsSelected = true;
        //            size.Quantity = matchingSize.Quantity;
        //        }
        //        else
        //        {
        //            size.IsSelected = false;
        //            size.Quantity = 0;
        //        }

        //        if (size.IsSizeSystemEU)
        //            _availableSizesEU.Add(size);
        //        else
        //            _availableSizesUS.Add(size);
        //    }
        //}


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
            Season seasonToUpdate = _seasons.First(y => y.GuidId == season.GuidId);

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
            var seasonToDelete = _seasons.FirstOrDefault(y => y.GuidId == GuidId);

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
            Category categoryToUpdate = _categories.FirstOrDefault(y => y.GuidId == category.GuidId);

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
            var categoryToDelete = _categories.FirstOrDefault(y => y.GuidId == GuidId);

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
