using DVS.Domain.Models;
using DVS.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DVS.WPF.Commands.ClothesCommands;

namespace DVS.WPF.ViewModels.ListingItems
{
    public class ClothesListingItemViewModel : ViewModelBase
    {
        public Clothes Clothes { get; private set; }
        public string Id => Clothes.Id;
        public string Name => Clothes.Name;
        public Category Category => Clothes.Category;
        public Season Season => Clothes.Season;
        public string? Comment => Clothes.Comment;
        public ObservableCollection<ClothesSize> Sizes => Clothes.Sizes;

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

        public bool HasError;

        public ICommand OpenEditClothes { get; set; }
        public ICommand DeleteClothes { get; set; }
        public ICommand ClearClothesSizes { get; set; }
        public ICommand PrintClothes { get; set; }


        public ClothesListingItemViewModel(
            Clothes clothes,
            ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            ClothesStore clothesStore,
            ClothesSizeStore clothesSizeStore,
            EmployeeClothesSizeStore employeeClothesSizesStore,
            EmployeeStore employeeStore,
            DVSListingViewModel dVSListingViewModel)
        {
            Clothes = clothes;

            OpenEditClothes = new OpenEditClothesCommand(
                this,
                modalNavigationStore,
                categoryStore,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                dVSListingViewModel);

            DeleteClothes = new DeleteClothesCommand(this, clothesStore);
            ClearClothesSizes = new ClearSizesCommand(this, clothesSizeStore);
            PrintClothes = new OpenPrintClothesCommand();
        }

        public void Update(Clothes clothes)
        {
            Clothes = clothes;

            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Season));
            OnPropertyChanged(nameof(Comment));
            OnPropertyChanged(nameof(Sizes));
        }
    }
}
