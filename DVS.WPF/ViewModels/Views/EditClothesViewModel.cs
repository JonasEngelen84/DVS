using DVS.WPF.Commands.AddEditCategoryCommands;
using DVS.WPF.Commands.AddEditClothesCommands;
using DVS.WPF.Commands.AddEditSeasonCommands;
using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class EditClothesViewModel : ViewModelBase
    {
        public AddEditClothesFormViewModel AddEditClothesFormViewModel { get; }
        public AddEditClothesListingViewModel AddEditListingViewModel { get; }
        public ICommand CloseAddEditClothes { get; }


        public EditClothesViewModel(Clothes clothes,
                                      ModalNavigationStore modalNavigationStore,
                                      SizeStore sizeStore,
                                      CategoryStore categoryStore,
                                      SeasonStore seasonStore,
                                      ClothesStore clothesStore,
                                      ClothesSizeStore clothesSizeStore,
                                      EmployeeClothesSizesStore employeeClothesSizesStore,
                                      EmployeeStore employeeStore,
                                      DVSListingViewModel dVSListingViewModel)
        {
            AddEditListingViewModel = new(clothes, sizeStore, categoryStore, seasonStore);
            CloseAddEditClothes = new CloseAddEditClothesCommand(modalNavigationStore);

            ICommand updatedClothes = new EditClothesCommand(this,
                                                             employeeStore,
                                                             clothesStore,
                                                             sizeStore,
                                                             categoryStore,
                                                             seasonStore,
                                                             clothesSizeStore,
                                                             employeeClothesSizesStore,
                                                             modalNavigationStore);

            ICommand openAddEditCategories = new OpenAddEditCategoriesCommand(modalNavigationStore,
                                                                              categoryStore,
                                                                              seasonStore,
                                                                              sizeStore,
                                                                              clothesStore,
                                                                              clothesSizeStore,
                                                                              employeeClothesSizesStore,
                                                                              employeeStore,
                                                                              null,
                                                                              this,
                                                                              AddEditListingViewModel,
                                                                              dVSListingViewModel);

            ICommand openAddEditSeasons = new OpenAddEditSeasonsCommand(modalNavigationStore,
                                                                        sizeStore,
                                                                        categoryStore,
                                                                        seasonStore,
                                                                        clothesStore,
                                                                        clothesSizeStore,
                                                                        employeeClothesSizesStore,
                                                                        employeeStore,
                                                                        null,
                                                                        this,
                                                                        AddEditListingViewModel);

            AddEditClothesFormViewModel = new(clothes,
                                              updatedClothes,
                                              openAddEditCategories,
                                              openAddEditSeasons,
                                              AddEditListingViewModel)
            {
                ID = clothes.ID,
                Name = clothes.Name,
                Category = clothes.Category,
                Season = clothes.Season,
                Comment = clothes.Comment
            };
        }
    }
}
