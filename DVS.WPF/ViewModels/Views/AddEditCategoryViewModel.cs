using DVS.Domain.Services.Interfaces;
using DVS.WPF.Commands.CategoryCommands;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class AddEditCategoryViewModel : ViewModelBase
    {
        public AddEditCategoryFormViewModel AddEditCategoryFormViewModel { get; }
        public ICommand CloseAddEditCategory { get; }

        public AddEditCategoryViewModel(
            ModalNavigationStore modalNavigationStore,
            CategoryStore categoryStore,
            ClothesStore clothesStore,
            ClothesSizeStore clothesSizeStore,
            EmployeeClothesSizeStore employeeClothesSizesStore,
            EmployeeStore employeeStore,
            AddClothesViewModel addClothesViewModel,
            EditClothesViewModel editClothesViewModel,
            SizesCategoriesSeasonsListingViewModel SizesCategoriesSeasonsListingViewModel,
            IDirtyEntitySaver dirtyEntitySaver)
        {
            ICommand addCategory = new AddCategoryCommand(this, categoryStore);
            ICommand updateCategory = new EditCategoryCommand(
                this,
                categoryStore,
                clothesStore,
                clothesSizeStore,
                employeeStore,
                employeeClothesSizesStore);
            ICommand deleteCategory = new DeleteCategoryCommand(
                this,
                categoryStore,
                clothesStore,
                clothesSizeStore,
                employeeStore,
                employeeClothesSizesStore,
                dirtyEntitySaver);

            CloseAddEditCategory = new CloseAddEditCategoryCommand(
                modalNavigationStore,
                addClothesViewModel,
                editClothesViewModel);

            AddEditCategoryFormViewModel = new AddEditCategoryFormViewModel(
                addCategory,
                updateCategory,
                deleteCategory,
                SizesCategoriesSeasonsListingViewModel)
            {
                NewCategory = "Neue Kategorie",
                SelectedCategory = new(Guid.NewGuid(), "Kategorie wählen")
            };
        }
    }
}
