using DVS.Commands.DVSHeadViewCommands;
using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels.ListViewItems
{
    public class ClothesListingItemViewModel : ViewModelBase
    {
        public ClothesModel Clothes { get; private set; }
        public string ID => Clothes.ID;
        public string Name => Clothes.Name;
        public CategoryModel Category => Clothes.Category;
        public SeasonModel Season => Clothes.Season;
        public string? Comment => Clothes.Comment;
        public ObservableCollection<ClothesSizeModel> Sizes => Clothes.Sizes;

        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                if (value != _isDeleting)
                {
                    _isDeleting = value;
                    OnPropertyChanged(nameof(IsDeleting));
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

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    OnPropertyChanged(nameof(IsExpanded));
                }
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand OpenEditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearSizesCommand { get; set; }
        public ICommand PrintClothesCommand { get; set; }


        public ClothesListingItemViewModel(ClothesModel clothes,ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore, SeasonStore seasonStore, ClothesStore clothesStore)
        {
            Clothes = clothes;
            DeleteCommand = new DeleteClothesCommand(this, clothesStore);
            ClearSizesCommand = new ClearSizesCommand();
            PrintClothesCommand = new OpenPrintClothesCommand();
            OpenEditCommand = new OpenEditClothesCommand(this, modalNavigationStore, categoryStore, seasonStore, clothesStore);
        }


        public void Update(ClothesModel clothes)
        {
            Clothes = clothes;

            OnPropertyChanged(nameof(ID));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Season));
            OnPropertyChanged(nameof(Comment));
            OnPropertyChanged(nameof(Sizes));
        }
    }
}
