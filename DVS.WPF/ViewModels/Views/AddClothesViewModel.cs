using DVS.Domain.Services.Interfaces;
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
        public AddClothesFormViewModel AddClothesFormViewModel { get; }
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
            IDirtyEntitySaver dirtyEntitySaver)
        {
            SizesCategoriesSeasonsListingViewModel = new(null, categoryStore, seasonStore);

            CloseAddEditClothes = new CloseAddEditClothesCommand(modalNavigationStore);

            ICommand addClothes = new AddClothesCommand(this, clothesStore, clothesSizeStore, modalNavigationStore);

            ICommand openAddEditCategories = new OpenAddEditCategoriesCommand(
                modalNavigationStore,
                categoryStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                this,
                null,
                SizesCategoriesSeasonsListingViewModel,
                dirtyEntitySaver);

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
                SizesCategoriesSeasonsListingViewModel,
                dirtyEntitySaver);

            AddClothesFormViewModel = new(
                addClothes,
                openAddEditCategories,
                openAddEditSeasons,
                SizesCategoriesSeasonsListingViewModel);
        }
    }
}
