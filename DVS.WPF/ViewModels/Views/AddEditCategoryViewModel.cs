using DVS.WPF.Commands.AddEditCategoryCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class AddEditCategoryViewModel : ViewModelBase
    {
        public AddEditCategoryFormViewModel AddEditCategoryFormViewModel { get; }
        public ICommand CloseAddEditCategory { get; }

        public AddEditCategoryViewModel(ModalNavigationStore modalNavigationStore,
                                        CategoryStore categoryStore,
                                        SeasonStore seasonStore,
                                        SizeStore sizeStore,
                                        ClothesStore clothesStore,
                                        ClothesSizeStore clothesSizeStore,
                                        EmployeeClothesSizesStore employeeClothesSizesStore,
                                        EmployeeStore employeeStore,
                                        AddClothesViewModel addClothesViewModel,
                                        EditClothesViewModel editClothesViewModel,
                                        AddEditClothesListingViewModel addEditListingViewModel,
                                        DVSListingViewModel dVSListingViewModel)
        {
            ICommand addCategory = new AddCategoryCommand(this, categoryStore);
            ICommand updateCategory = new EditCategoryCommand(this, categoryStore);
            ICommand deleteCategory = new DeleteCategoryCommand(this,
                                                                categoryStore,
                                                                seasonStore,
                                                                sizeStore,
                                                                clothesStore,
                                                                clothesSizeStore,
                                                                employeeClothesSizesStore,
                                                                employeeStore,
                                                                dVSListingViewModel);

            CloseAddEditCategory = new CloseAddEditCategoryCommand(modalNavigationStore,
                                                                   addClothesViewModel,
                                                                   editClothesViewModel);

            AddEditCategoryFormViewModel = new AddEditCategoryFormViewModel(addCategory,
                                                                            updateCategory,
                                                                            deleteCategory,
                                                                            addEditListingViewModel)
            {
                AddNewCategory = "Neue Kategorie",
                SelectedCategory = new(Guid.NewGuid(), "Kategorie wählen")
            };
        }
    }
}
