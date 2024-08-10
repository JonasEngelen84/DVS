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
                                        AddClothesViewModel addClothesViewModel,
                                        EditClothesViewModel editClothesViewModel,
                                        AddEditListingViewModel addEditListingViewModel)
        {
            ICommand addCategory = new AddCategoryCommand(this, categoryStore);
            ICommand editCategory = new EditCategoryCommand(this, categoryStore);
            ICommand deleteCategory = new DeleteCategoryCommand(this, categoryStore);
            ICommand clearCategoryList = new ClearCategoryListCommand(this, categoryStore);

            CloseAddEditCategory = new CloseAddEditCategoryCommand(modalNavigationStore,
                                                                   addClothesViewModel,
                                                                   editClothesViewModel);

            AddEditCategoryFormViewModel = new AddEditCategoryFormViewModel(addCategory,
                                                                            editCategory,
                                                                            deleteCategory,
                                                                            clearCategoryList,
                                                                            addEditListingViewModel)
            {
                AddNewCategory = "Neue Kategorie",
                EditSelectedCategory = "Kategorie wählen",
                SelectedCategory = new(Guid.NewGuid(), "Kategorie wählen")
            };
        }
    }
}
