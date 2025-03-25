using DVS.Domain.Models;
using DVS.Domain.Services.Interfaces;
using DVS.WPF.Commands.CategoryCommands;
using DVS.WPF.Commands.ClothesCommands;
using DVS.WPF.Commands.SeasonCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

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
            IDirtyEntitySaver dirtyEntitySaver)
        {            
            SizesCategoriesSeasonsListingViewModel = new(clothes, categoryStore, seasonStore);
            CloseAddEditClothes = new CloseAddEditClothesCommand(modalNavigationStore);

            ICommand updatedClothes = new EditClothesCommand(
                this,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                modalNavigationStore);

            ICommand openAddEditCategories = new OpenAddEditCategoriesCommand(
                modalNavigationStore,
                categoryStore,
                clothesStore,
                clothesSizeStore,
                employeeClothesSizesStore,
                employeeStore,
                null,
                this,
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
                null,
                this,
                SizesCategoriesSeasonsListingViewModel,
                dirtyEntitySaver);

            EditClothesFormViewModel = new(
                clothes,
                updatedClothes,
                openAddEditCategories,
                openAddEditSeasons,
                SizesCategoriesSeasonsListingViewModel)
            {
                Id = clothes.Id,
                Name = clothes.Name,
                Category = categoryStore.Categories
                    .First(c => c.Id == clothes.Category?.Id),
                Season = seasonStore.Seasons
                    .First(s => s.Id == clothes.Season?.Id),
                Comment = clothes.Comment
            };
        }
    }
}
