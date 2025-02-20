using DVS.WPF.Commands.AddEditClothesCommands;
using DVS.WPF.Commands.CategoryCommands;
using DVS.WPF.Commands.ClothesCommands;
using DVS.WPF.Commands.SeasonCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class AddClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public SizesCategoriesSeasonsListingViewModel SizesCategoriesSeasonsListingViewModel { get; }
        public ICommand CloseAddEditClothes { get; }

        public AddClothesViewModel(
            ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore,
            SeasonStore seasonStore,
            ClothesStore clothesStore,
            ClothesSizeStore clothesSizeStore,
            EmployeeClothesSizeStore employeeClothesSizesStore,
            EmployeeStore employeeStore,
            DVSListingViewModel dVSListingViewModel)
        {
            SizesCategoriesSeasonsListingViewModel = new(null, categoryStore, seasonStore);

            CloseAddEditClothes = new CloseAddEditClothesCommand(modalNavigationStore);

            ICommand addClothes = new AddClothesCommand(this, clothesStore, clothesSizeStore, modalNavigationStore);

            ICommand openAddEditCategories = new OpenAddEditCategoriesCommand(
                modalNavigationStore,
                categoryStore,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                this,
                null,
                SizesCategoriesSeasonsListingViewModel,
                dVSListingViewModel);

            ICommand openAddEditSeasons = new OpenAddEditSeasonsCommand(
                modalNavigationStore,
                categoryStore,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                this,
                null,
                SizesCategoriesSeasonsListingViewModel);

            AddEditClothesFormViewModel = new(
                null,
                addClothes,
                openAddEditCategories,
                openAddEditSeasons,
                SizesCategoriesSeasonsListingViewModel)
            {
                Id = "Id",
                Name = "Name",
                Comment = "Kommentar"
            };
        }
    }
}
