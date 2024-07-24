using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.AddEditEmployeeCommands
{
    public class AddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel, EmployeeStore employeeStore,
        ModalNavigationStore modalNavigationStore) : AsyncCommandBase
    {
        private readonly AddEmployeeViewModel _addEmployeeViewModel = addEmployeeViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditEmployeeFormViewModel addEmployeeFormViewModel = _addEmployeeViewModel.AddEditEmployeeFormViewModel;

            addEmployeeFormViewModel.ErrorMessage = null;
            addEmployeeFormViewModel.IsSubmitting = true;

            EmployeeModel employee = new(Guid.NewGuid(),
                                         addEmployeeFormViewModel.ID,
                                         addEmployeeFormViewModel.Firstname,
                                         addEmployeeFormViewModel.Lastname,
                                         addEmployeeFormViewModel.Comment);

            foreach (DetailedClothesListingItemModel item in
                _addEmployeeViewModel.AddEditEmployeeFormViewModel.DVSListingViewModel.NewEmployeeListingItemCollection)
            {
                ClothesModel existingClothes = employee.Clothes.FirstOrDefault(clothes => clothes.GuidID == item.Clothes.GuidID);

                if (existingClothes != null)
                {
                    existingClothes.Sizes.Add(new ClothesSizeModel(item.Size) { Quantity = item.Quantity, IsSelected = true });
                }
                else
                {
                    ClothesModel newClothes = new(item.Clothes.GuidID, item.ID, item.Name, item.Clothes.Category, item.Clothes.Season, null);
                    newClothes.Sizes.Add(new ClothesSizeModel(item.Size) { Quantity = item.Quantity, IsSelected = true });
                    employee.Clothes.Add(newClothes);
                }
            }

            try
            {
                await _employeeStore.Add(employee);
            }
            catch (Exception)
            {
                addEmployeeFormViewModel.ErrorMessage = "Erstellen des Mitarbeiters ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEmployeeFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
