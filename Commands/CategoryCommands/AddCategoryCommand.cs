using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.CategoryCommands
{
    public class AddCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel,
                                    CategoryStore categoryStore)
                                    : CommandBase
    {
        public override async void Execute(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = addEditCategoryViewModel.AddEditCategoryFormViewModel;
            addEditCategoryFormViewModel.ErrorMessage = null;
            addEditCategoryFormViewModel.IsSubmitting = true;
            string newCategory = addEditCategoryFormViewModel.AddNewCategory;

            try
            {
                await categoryStore.Add(newCategory);
            }
            catch (Exception)
            {
                addEditCategoryFormViewModel.ErrorMessage = "Erstellen der Kategorie ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEditCategoryFormViewModel.IsSubmitting = false;
            }
        }
    }
}
