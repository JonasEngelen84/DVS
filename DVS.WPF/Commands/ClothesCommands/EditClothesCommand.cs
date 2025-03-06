using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class EditClothesCommand(
        EditClothesViewModel editClothesViewModel,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            EditClothesFormViewModel editClothesFormViewModel = editClothesViewModel.EditClothesFormViewModel;
            editClothesFormViewModel.HasError = false;

            if (!Confirm($"Soll die Bekleidung  {editClothesFormViewModel.Id}, {editClothesFormViewModel.Name}  " +
                "wirklich bearbeitet werden?", "Bekleidung bearbeiten"))
            {
                return;
            }

            editClothesFormViewModel.IsSubmitting = true;

            List<ClothesSize> OldClothesSizes = new(editClothesFormViewModel.Clothes.Sizes);
            List<ClothesSize> EqualClothesSizes = [];
            List<ClothesSize> NewClothesSizes = [];
            List<EmployeeClothesSize> EditedEmployeeClothesSizes = [];

            Clothes editedClothes = CreateEditedClothes(editClothesFormViewModel);
            List<SizeListingItemViewModel> newSelectedSizes = GetSelectedSizes(editClothesFormViewModel);

            if (OldClothesSizes.Count > 0)
            {
                await CompareAndSortClothesSizes(
                    OldClothesSizes,
                    newSelectedSizes,
                    EqualClothesSizes,
                    editedClothes,
                    editClothesFormViewModel);
            }

            await UpdateClothesSizes(editedClothes, editClothesFormViewModel);

            if (newSelectedSizes.Count > 0)
            {
                CreateNewClothesSizes(newSelectedSizes, editedClothes, NewClothesSizes);
            }

            await UpdateEmployeeClothesSizes(editedClothes, EditedEmployeeClothesSizes, editClothesFormViewModel);
            await UpdateEmployees(EditedEmployeeClothesSizes, editClothesFormViewModel);
            AddEqualClothesSizes(editedClothes, EqualClothesSizes);
            await UpdateClothes(editedClothes, editClothesFormViewModel);
            await AddNewClothesSizes(NewClothesSizes, editClothesFormViewModel);

            editClothesFormViewModel.IsSubmitting = false;
            modalNavigationStore.Close();
        }

        private static Clothes CreateEditedClothes(EditClothesFormViewModel editClothesFormViewModel)
        {
            return new Clothes(
                editClothesFormViewModel.Id,
                editClothesFormViewModel.Name,
                editClothesFormViewModel.Category,
                editClothesFormViewModel.Season,
                editClothesFormViewModel.Comment)
            {
                Sizes = []
            };
        }

        private static List<SizeListingItemViewModel> GetSelectedSizes(EditClothesFormViewModel editClothesFormViewModel)
        {
            return new List<SizeListingItemViewModel>(editClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesUS
                .Any(size => size.Quantity > 0)
                ? editClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesUS.Where(size => size.Quantity > 0)
                : editClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesEU.Where(size => size.Quantity > 0))
                .ToList();
        }

        private async Task CompareAndSortClothesSizes(
            List<ClothesSize> oldClothesSizes,
            List<SizeListingItemViewModel> newSelectedSizes,
            List<ClothesSize> equalClothesSizes,
            Clothes editedClothes,
            EditClothesFormViewModel editClothesFormViewModel)
        {
            foreach (ClothesSize oldClothesSize in oldClothesSizes)
            {
                SizeListingItemViewModel? existingSize = newSelectedSizes
                    .FirstOrDefault(slivm => slivm.Size == oldClothesSize.Size);

                if (existingSize == null)
                {
                    EmployeeClothesSize? assignedClothesSize = employeeClothesSizeStore.EmployeeClothesSizes
                    .FirstOrDefault(ecs => ecs.ClothesSizeGuidId == oldClothesSize.GuidId);

                    if (assignedClothesSize != null)
                        editedClothes.Sizes.Add(CreateEditedClothesSize(oldClothesSize, editedClothes, 0, existingSize.Comment));
                    else
                        await DeleteClothesSize(oldClothesSize, editClothesFormViewModel);
                }
                else
                {
                    if (oldClothesSize.Quantity == existingSize.Quantity && string.Equals(oldClothesSize.Comment, existingSize.Comment))
                        equalClothesSizes.Add(oldClothesSize);
                    else
                        editedClothes.Sizes.Add(CreateEditedClothesSize(oldClothesSize, editedClothes, existingSize.Quantity, existingSize.Comment));

                    newSelectedSizes.Remove(existingSize);
                }
            }
        }

        private static ClothesSize CreateEditedClothesSize(ClothesSize oldClothesSize, Clothes editedClothes, int quantity, string comment)
        {
            return new ClothesSize(
                    oldClothesSize.GuidId,
                    editedClothes,
                    oldClothesSize.Size,
                    quantity,
                    comment)
            {
                EmployeeClothesSizes = oldClothesSize.EmployeeClothesSizes
            };
        }

        private async Task DeleteClothesSize(ClothesSize clothesSizeToDelete, EditClothesFormViewModel editClothesFormViewModel)
        {
            try
            {
                await clothesSizeStore.Delete(clothesSizeToDelete);
            }
            catch
            {
                ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!", " Bekleidung bearbeiten");
                editClothesFormViewModel.HasError = true;
            }
        }

        private async Task UpdateClothesSizes(Clothes editedClothes, EditClothesFormViewModel editClothesFormViewModel)
        {
            foreach (ClothesSize editedClothesSize in editedClothes.Sizes)
            {
                try
                {
                    await clothesSizeStore.Update(editedClothesSize);
                }
                catch
                {
                    ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!", " Bekleidung bearbeiten");
                    editClothesFormViewModel.HasError = true;
                }
            }
        }

        private static void CreateNewClothesSizes(
            List<SizeListingItemViewModel> newSelectedSizes,
            Clothes editedClothes,
            List<ClothesSize> NewClothesSizes)
        {
            foreach (SizeListingItemViewModel slivm in newSelectedSizes)
            {
                ClothesSize newClothesSize = new(Guid.NewGuid(), editedClothes, slivm.Size, slivm.Quantity, slivm.Comment)
                {
                    EmployeeClothesSizes = []
                };

                editedClothes.Sizes.Add(newClothesSize);
                NewClothesSizes.Add(newClothesSize);
            }
        }

        private async Task UpdateEmployeeClothesSizes(
            Clothes editedClothes,
            List<EmployeeClothesSize> EditedEmployeeClothesSizes,
            EditClothesFormViewModel editClothesFormViewModel)
        {
            List<EmployeeClothesSize> assignedClothesSizes = [];

            foreach (ClothesSize clothesSize in editedClothes.Sizes)
            {
                assignedClothesSizes = employeeClothesSizeStore.EmployeeClothesSizes
                    .Where(ecs => ecs.ClothesSizeGuidId == clothesSize.GuidId)
                    .ToList();

                foreach (EmployeeClothesSize ecs in assignedClothesSizes)
                {
                    EmployeeClothesSize editedEcs = new(
                        ecs.GuidId,
                        ecs.Employee,
                        clothesSize,
                        ecs.Quantity,
                        ecs.Comment);

                    EditedEmployeeClothesSizes.Add(editedEcs);

                    try
                    {
                        await employeeClothesSizeStore.Update(editedEcs);
                    }
                    catch
                    {
                        ShowErrorMessageBox("Löschen der Bekleidungs-Größen ist fehlgeschlagen!", "Bekleidungs-Größen löschen");
                        editClothesFormViewModel.HasError = true;
                    }
                }
            }
        }

        private async Task UpdateEmployees(
            List<EmployeeClothesSize> EditedEmployeeClothesSizes,
            EditClothesFormViewModel editClothesFormViewModel)
        {
            List<EmployeeClothesSize> ecsOwner = [];

            foreach (EmployeeClothesSize employeeClothesSize in EditedEmployeeClothesSizes)
            {
                ecsOwner = EditedEmployeeClothesSizes
                    .Where(ecs => ecs.EmployeeId == employeeClothesSize.EmployeeId)
                    .ToList();

                Employee editedEmployee = new(
                    employeeClothesSize.EmployeeId,
                    employeeClothesSize.Employee.Lastname,
                    employeeClothesSize.Employee.Firstname,
                    employeeClothesSize.Employee.Comment)
                {
                    Clothes = new(employeeClothesSize.Employee.Clothes)
                };

                foreach (EmployeeClothesSize employeeCS in ecsOwner)
                {
                    EmployeeClothesSize existingEcs = editedEmployee.Clothes
                        .First(ecs => ecs.GuidId == employeeCS.GuidId);

                    editedEmployee.Clothes.Remove(existingEcs);
                    editedEmployee.Clothes.Add(employeeCS);
                }

                try
                {
                    await employeeStore.Update(editedEmployee);
                }
                catch
                {
                    ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!", " Bekleidung bearbeiten");
                    editClothesFormViewModel.HasError = true;
                }
            }
        }

        private static void AddEqualClothesSizes(Clothes editedClothes, List<ClothesSize> EqualClothesSizes)
        {
            foreach (ClothesSize cs in EqualClothesSizes)
            {
                editedClothes.Sizes.Add(cs);
            }
        }

        private async Task UpdateClothes(Clothes editedClothes, EditClothesFormViewModel editClothesFormViewModel)
        {
            try
            {
                await clothesStore.Update(editedClothes);
            }
            catch
            {
                ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!", "AddClothesCommand CreateAndAddNewClothesAsync");
                editClothesFormViewModel.HasError = true;
            }
        }

        private async Task AddNewClothesSizes(List<ClothesSize> NewClothesSizes, EditClothesFormViewModel editClothesFormViewModel)
        {
            foreach (ClothesSize newClothesSize in NewClothesSizes)
            {
                try
                {
                    await clothesSizeStore.Add(newClothesSize);
                }
                catch
                {
                    ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!", " Bekleidung bearbeiten");
                    editClothesFormViewModel.HasError = true;
                }
            }
        }
    }
}
