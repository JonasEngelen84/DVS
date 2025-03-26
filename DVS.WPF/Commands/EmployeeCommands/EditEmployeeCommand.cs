using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.EmployeeCommands
{
    public class EditEmployeeCommand(
        EditEmployeeViewModel editEmployeeViewModel,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        private readonly List<ClothesSize> editedClothesSizesList = [];

        public override async Task ExecuteAsync(object parameter)
        {
            EditEmployeeFormViewModel editEmployeeFormViewModel = editEmployeeViewModel.EditEmployeeFormViewModel;
            editEmployeeFormViewModel.HasError = false;

            if (!Confirm($"Soll der/die Mitarbeiter/in  \"{editEmployeeFormViewModel.Lastname}\", \"{editEmployeeFormViewModel.Firstname}\"" +
                "  bearbeiten werden?", "Mitarbeiter bearbeiten"))
            {                
                return;
            }

            editEmployeeFormViewModel.IsSubmitting = true;

            List<EmployeeClothesSize> oldEmployeeClothes = new(editEmployeeFormViewModel.Employee.Clothes);
            List<EmployeeClothesSizeListingItemViewModel> newEmployeeClothes = new(
                editEmployeeFormViewModel.AddEditEmployeeListingViewModel.EmployeeClothesList);

            bool employeePropertyChanged = false;

            EditEmployee(editEmployeeFormViewModel, ref employeePropertyChanged);

            if (oldEmployeeClothes.Count > 0 && newEmployeeClothes.Count > 0)
            {
                await DeleteOrUpdateEcs(oldEmployeeClothes, newEmployeeClothes, employeePropertyChanged, editEmployeeFormViewModel);
            }

            if (newEmployeeClothes.Count > 0)
                await AddNewEcsAsync(newEmployeeClothes, editEmployeeFormViewModel);

            UpdateClothesSizes(editEmployeeFormViewModel);
            UpdateClothes(editedClothesSizesList, editEmployeeFormViewModel);
            employeeStore.Update(editEmployeeFormViewModel.Employee);

            editEmployeeFormViewModel.IsSubmitting = false;
            modalNavigationStore.Close();
        }

        private static void EditEmployee(EditEmployeeFormViewModel editEmployeeFormViewModel, ref bool employeePropertyChanged)
        {
            if (!editEmployeeFormViewModel.Employee.Lastname.Equals(editEmployeeFormViewModel.Lastname))
            {
                editEmployeeFormViewModel.Employee.Lastname = editEmployeeFormViewModel.Lastname;
                employeePropertyChanged = true;
            }
            
            if (!editEmployeeFormViewModel.Employee.Firstname.Equals(editEmployeeFormViewModel.Firstname))
            {
                editEmployeeFormViewModel.Employee.Firstname = editEmployeeFormViewModel.Firstname;
                employeePropertyChanged = true;
            }
            
            if (!editEmployeeFormViewModel.Employee.Comment.Equals(editEmployeeFormViewModel.Comment))
            {
                editEmployeeFormViewModel.Employee.Comment = editEmployeeFormViewModel.Comment;
                employeePropertyChanged = true;
            }
        }

        private async Task DeleteOrUpdateEcs(
            List<EmployeeClothesSize> oldClothes,
            List<EmployeeClothesSizeListingItemViewModel> newClothes,
            bool employeePropertyChanged,
            EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSize oldEcs in oldClothes)
            {
                EmployeeClothesSizeListingItemViewModel? newEcs = newClothes
                    .FirstOrDefault(ecslivm => ecslivm.EmployeeClothesSizeGuidId == oldEcs.Id);

                if (newEcs == null)
                {
                    await DeleteEcs(oldEcs, editEmployeeFormViewModel);
                }
                else
                {
                    UpdateEmployeeClothesSize(oldEcs, newEcs, employeePropertyChanged, editEmployeeFormViewModel);
                    newClothes.Remove(newEcs);
                }
            }
        }

        private async Task DeleteEcs(EmployeeClothesSize oldEcs, EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            try
            {
                await employeeClothesSizeStore.Delete(oldEcs);
            }
            catch
            {
                ShowErrorMessageBox("Bearbeiten des/der Mitarbeiter/in ist fehlgeschlagen!", " Mitarbeiter/in bearbeiten");
                editEmployeeFormViewModel.HasError = true;
            }

            editEmployeeFormViewModel.Employee.Clothes.Remove(oldEcs);
        }

        private void UpdateEmployeeClothesSize(
            EmployeeClothesSize oldEcs,
            EmployeeClothesSizeListingItemViewModel newEcs,
            bool employeePropertyChanged,
            EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            bool ecsPropertyChanged = false;

            if (oldEcs.Quantity != newEcs.Quantity)
            {
                oldEcs.Quantity = newEcs.Quantity;
                ecsPropertyChanged = true;
            }

            if (!string.Equals(oldEcs.Comment, newEcs.Comment))
            {
                oldEcs.Comment = newEcs.Comment;
                ecsPropertyChanged = true;
            }

            if (employeePropertyChanged)
            {
                oldEcs.Employee = editEmployeeFormViewModel.Employee;
                ecsPropertyChanged = true;
            }

            if (ecsPropertyChanged)
            {
                EmployeeClothesSize existingEcs = editEmployeeFormViewModel.Employee.Clothes
                    .First(ecs => ecs.Id == oldEcs.Id);

                editEmployeeFormViewModel.Employee.Clothes.Remove(existingEcs);
                editEmployeeFormViewModel.Employee.Clothes.Add(oldEcs);
                employeeClothesSizeStore.Update(oldEcs);
            }
        }

        private async Task AddNewEcsAsync(
            List<EmployeeClothesSizeListingItemViewModel> newClothes,
            EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            foreach (EmployeeClothesSizeListingItemViewModel ecslivm in newClothes)
            {
                EmployeeClothesSize newEcs = new(
                    Guid.NewGuid(),
                    editEmployeeFormViewModel.Employee,
                    ecslivm.ClothesSize,
                    ecslivm.Quantity,
                    ecslivm.Comment);

                editEmployeeFormViewModel.Employee.Clothes.Add(newEcs);

                try
                {
                    await employeeClothesSizeStore.AddDataBase(newEcs);
                }
                catch
                {
                    ShowErrorMessageBox("Bearbeiten des/der Mitarbeiter/in ist fehlgeschlagen!", " Mitarbeiter/in bearbeiten");
                    editEmployeeFormViewModel.HasError = true;
                }
            }
        }

        private void UpdateClothesSizes(EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            List<AvailableClothesSizeItem> ClothesSizesToEdit = editEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllClothesSizesToEdit();

            foreach (AvailableClothesSizeItem acsi in ClothesSizesToEdit)
            {
                ClothesSize existingClothesSize = clothesSizeStore.ClothesSizes.First(cs => cs.Id == acsi.ClothesSizeId);

                existingClothesSize.Quantity = acsi.Quantity;

                editedClothesSizesList.Add(existingClothesSize);

                clothesSizeStore.Update(existingClothesSize);
            }
        }

        private void UpdateClothes(List<ClothesSize> editedClothesSizesList, EditEmployeeFormViewModel editEmployeeFormViewModel)
        {
            List<Clothes> clothesToEdited = editEmployeeFormViewModel.AddEditEmployeeListingViewModel.GetAllClothesToEdit();

            foreach (Clothes clothes in clothesToEdited)
            {
                foreach (ClothesSize editedClothesSize in editedClothesSizesList)
                {
                    if (editedClothesSize.ClothesId == clothes.Id)
                    {
                        ClothesSize existingClothesSize = clothes.Sizes.First(cs => cs.Id == editedClothesSize.Id);

                        if (existingClothesSize != null)
                        {
                            clothes.Sizes.Remove(existingClothesSize);
                            clothes.Sizes.Add(editedClothesSize);
                        }
                    }
                }

                clothesStore.Update(clothes);
            }
        }
    }
}
