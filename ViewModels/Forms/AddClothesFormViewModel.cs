using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
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

        private string _quantity;
        public string Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
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

        public AddEditClothes_ClothesListViewViewModel AddEditClothes_ClothesListViewViewModel { get; }

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
            //_clothes = [];

            CategoryStore_CategoriesLoaded();
            SeasonStore_SeasonsLoaded();
            //ClothesStore_ClothesLoaded();
        }


        protected override void Dispose()
        {


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

        //private void ClothesStore_ClothesLoaded()
        //{
        //    _clothes.Clear();

        //    foreach (ClothesModel clothes in _clothesStore.Clothes)
        //    {
        //        AddClothes(clothes);
        //    }
        //}

        //private void ClothesStore_ClothesAdded(ClothesModel clothes)
        //{
        //    AddClothes(clothes);
        //}

        private void Edit_Clothes()
        {

        }

        //private void AddClothes(ClothesModel clothes)
        //{
        //    _clothes.Add(clothes);

        //    Id = "";
        //    Name = "";
        //    Size = "";
        //    Quantity = "";
        //    Comment = "";

        //    OnPropertyChanged(nameof(Clothes));
        //}
    }
}
