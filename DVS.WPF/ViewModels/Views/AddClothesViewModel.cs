using DVS.EntityFramework;
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
        public AddEditClothesListingViewModel AddEditListingViewModel { get; }
        public ICommand CloseAddEditClothes { get; }

        public AddClothesViewModel(ModalNavigationStore modalNavigationStore,
                                   SizeStore sizeStore,
                                   CategoryStore categoryStore,
                                   SeasonStore seasonStore,
                                   ClothesStore clothesStore,
                                   ClothesSizeStore clothesSizeStore,
                                   EmployeeClothesSizesStore employeeClothesSizesStore,
                                   EmployeeStore employeeStore,
                                   DVSListingViewModel dVSListingViewModel,
                                   DVSDbContextFactory dVSDbContextFactory)
        {
            AddEditListingViewModel = new(null, sizeStore, categoryStore, seasonStore);

            CloseAddEditClothes = new CloseAddEditClothesCommand(modalNavigationStore);

            ICommand addClothes = new AddClothesCommand(this, clothesStore, modalNavigationStore);

            ICommand openAddEditCategories = new OpenAddEditCategoriesCommand(modalNavigationStore,
                                                                              categoryStore,
                                                                              seasonStore,
                                                                              sizeStore,
                                                                              clothesStore,
                                                                              clothesSizeStore,
                                                                              employeeClothesSizesStore,
                                                                              employeeStore,
                                                                              this,
                                                                              null,
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
                                                                        this,
                                                                        null,
                                                                        AddEditListingViewModel);

            AddEditClothesFormViewModel = new(null,
                                              addClothes,
                                              openAddEditCategories,
                                              openAddEditSeasons,
                                              AddEditListingViewModel)
            {
                Id = "Id",
                Name = "Name",
                Comment = "Kommentar"
            };
        }
    }
}
