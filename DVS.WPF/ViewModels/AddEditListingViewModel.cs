using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace DVS.WPF.ViewModels
{
    public class AddEditListingViewModel : ViewModelBase
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


        public AddEditListingViewModel(Clothes clothes, SizeStore sizeStore, CategoryStore categoryStore, SeasonStore seasonStore)
        {
            _clothes = clothes;
            _sizeStore = sizeStore;
            _categoryStore = categoryStore;
            _seasonStore = seasonStore;

            _categoryCollectionViewSource = new CollectionViewSource { Source = _categories };
            _categoryCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(Category.Name), ListSortDirection.Ascending));

            _seasonCollectionViewSource = new CollectionViewSource { Source = _seasons };
            _seasonCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(Season.Name), ListSortDirection.Ascending));

            //_availableSizesEU =
            //[
            //    new Size("44", true),
            //    new Size("46", true),
            //    new Size("48", true),
            //    new Size("50", true),
            //    new Size("52", true),
            //    new Size("54", true),
            //    new Size("56", true),
            //    new Size("58", true),
            //    new Size("60", true),
            //    new Size("62", true)
            //];

            //_availableSizesUS =
            //[
            //    new Size("XS", false),
            //    new Size("S", false),
            //    new Size("M", false),
            //    new Size("L", false),
            //    new Size("XL", false),
            //    new Size("XLL", false),
            //    new Size("3XL", false),
            //    new Size("4XL", false),
            //    new Size("5XL", false),
            //    new Size("6XL", false)
            //];

            //CategoryStore_CategoriesLoaded();
            //SeasonStore_SeasonsLoaded();
            SizeSore_SizesLoaded();

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


        private void SizeSore_SizesLoaded()
        {
            _availableSizesEU.Clear();
            _availableSizesUS.Clear();

            foreach (SizeModel size in _sizeStore.Sizes)
            {
                if (size.IsSizeSystemEU)
                    _availableSizesEU.Add(size);
                else
                    _availableSizesUS.Add(size);
            }

            // Nur bei UpdateClothes
            if (_clothes != null)
            {
                foreach (var size in _clothes.Sizes)
                {
                    var matchingSize = _availableSizesEU.FirstOrDefault(s => s.Size == size.Size.Size) ??
                                       _availableSizesUS.FirstOrDefault(s => s.Size == size.Size.Size);

                    if (matchingSize != null)
                    {
                        matchingSize.IsSelected = true;
                        matchingSize.Quantity = size.Quantity;
                    }
                }
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

        private void SeasonStore_SeasonAdded(Season season,  AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            AddSeason(season, addEditSeasonFormViewModel);
            OnPropertyChanged(nameof(addEditSeasonFormViewModel.CanDeleteAll));
        }

        private void SeasonStore_SeasonEdited(Season season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            Season seasonToUpdate = _seasons.FirstOrDefault(y => y.GuidID == season.GuidID);

            if (seasonToUpdate != null)
            {
                int index = _seasons.IndexOf(seasonToUpdate);
                _seasons[index] = season;
                _seasonCollectionViewSource.View.Refresh();
                addEditSeasonFormViewModel.SelectedSeason = new(null, "Saison wählen");
                addEditSeasonFormViewModel.EditSelectedSeason = addEditSeasonFormViewModel.SelectedSeason.Name;
                OnPropertyChanged(nameof(addEditSeasonFormViewModel.CanEdit));
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
                addEditSeasonFormViewModel.SelectedSeason = new(null, "Saison wählen");
                addEditSeasonFormViewModel.EditSelectedSeason = addEditSeasonFormViewModel.SelectedSeason.Name;
                addEditSeasonFormViewModel.SeasonCollectionChanged();
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
                addEditSeasonFormViewModel.EditSelectedSeason = addEditSeasonFormViewModel.SelectedSeason.Name;
                addEditSeasonFormViewModel.SeasonCollectionChanged();
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Kategorien nicht möglich.");
            }
        }

        private void AddSeason(Season newSeason, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            _seasons.Add(newSeason);
            _seasonCollectionViewSource.View.Refresh();
            addEditSeasonFormViewModel.AddNewSeason = "Neue Saison";
            addEditSeasonFormViewModel.SeasonCollectionChanged();
        }


        private void CategoryStore_CategoriesLoaded()
        {
            _categories.Clear();

            foreach (Category category in _categoryStore.Categories)
            {
                _categories.Add(category);
            }
        }

        private void CategoryStore_CategoryAdded(Category category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            AddCategory(category, addEditCategoryFormViewModel);
            OnPropertyChanged(nameof(addEditCategoryFormViewModel.CanDeleteAll));
        }

        private void CategoryStore_CategoryEdited(Category category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            Category categoryToUpdate = _categories.FirstOrDefault(y => y.GuidID == category.GuidID);

            if (categoryToUpdate != null)
            {
                int index = _categories.IndexOf(categoryToUpdate);
                _categories[index] = category;
                _categoryCollectionViewSource.View.Refresh();
                addEditCategoryFormViewModel.SelectedCategory = new(null, "Kategorie wählen");
                addEditCategoryFormViewModel.EditSelectedCategory = addEditCategoryFormViewModel.SelectedCategory.Name;
                OnPropertyChanged(nameof(addEditCategoryFormViewModel.CanEdit));
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
                addEditCategoryFormViewModel.SelectedCategory = new(null, "Kategorie wählen");
                addEditCategoryFormViewModel.EditSelectedCategory = addEditCategoryFormViewModel.SelectedCategory.Name;
                addEditCategoryFormViewModel.CategoryCollectionChanged();
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
                addEditCategoryFormViewModel.EditSelectedCategory = addEditCategoryFormViewModel.SelectedCategory.Name;
                addEditCategoryFormViewModel.CategoryCollectionChanged();
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Kategorien nicht möglich.");
            }
        }

        private void AddCategory(Category newCategory, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            _categories.Add(newCategory);
            _categoryCollectionViewSource.View.Refresh();
            addEditCategoryFormViewModel.AddNewCategory = "Neue Kategorie";
            addEditCategoryFormViewModel.CategoryCollectionChanged();
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
