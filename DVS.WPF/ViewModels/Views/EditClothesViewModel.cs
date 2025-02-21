using DVS.WPF.Commands.AddEditClothesCommands;
using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;
using DVS.WPF.Commands.CategoryCommands;
using DVS.WPF.Commands.ClothesCommands;
using DVS.WPF.Commands.SeasonCommands;

namespace DVS.WPF.ViewModels.Views
{
    public class EditClothesViewModel : ViewModelBase
    {
        public EditClothesFormViewModel EditClothesFormViewModel { get; }
        public SizesCategoriesSeasonsListingViewModel SizesCategoriesSeasonsListingViewModel { get; }
        public ICommand CloseAddEditClothes { get; }

        public EditClothesViewModel(
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
            SizesCategoriesSeasonsListingViewModel = new(clothes, categoryStore, seasonStore);
            CloseAddEditClothes = new CloseAddEditClothesCommand(modalNavigationStore);

            ICommand updatedClothes = new EditClothesCommand(
                this,
                employeeStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                modalNavigationStore);

            ICommand openAddEditCategories = new OpenAddEditCategoriesCommand(
                modalNavigationStore,
                categoryStore,
                seasonStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                null,
                this,
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
                null,
                this,
                SizesCategoriesSeasonsListingViewModel);

            EditClothesFormViewModel = new(
                clothes,
                updatedClothes,
                openAddEditCategories,
                openAddEditSeasons,
                SizesCategoriesSeasonsListingViewModel)
            {
                Id = clothes.Id,
                Name = clothes.Name,
                Category = SizesCategoriesSeasonsListingViewModel.Categories
                    .First(c => c.GuidId == clothes.Category?.GuidId),
                Season = SizesCategoriesSeasonsListingViewModel.Seasons
                    .First(s => s.GuidId == clothes.Season?.GuidId),
                Comment = clothes.Comment
            };
        }
    }
}
