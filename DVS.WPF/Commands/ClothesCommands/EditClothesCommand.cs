using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListingItems;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class EditClothesCommand(
        EditClothesViewModel editClothesViewModel,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizeStore,
        EmployeeStore employeeStore,
        ModalNavigationStore modalNavigationStore)
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            EditClothesFormViewModel editClothesFormViewModel = editClothesViewModel.EditClothesFormViewModel;
            editClothesFormViewModel.HasError = false;

            if (!Confirm($"Soll die Bekleidung  {editClothesFormViewModel.Id}, {editClothesFormViewModel.Clothes.Name}  " +
                "wirklich bearbeitet werden?", "Bekleidung bearbeiten"))
            {
                return;
            }

            editClothesFormViewModel.IsSubmitting = true;

            List<ClothesSize> OldClothesSizes = new(editClothesFormViewModel.Clothes.Sizes);
            List<SizeListingItemViewModel> newSelectedSizes = GetSelectedSizes(editClothesFormViewModel);
            List<EmployeeClothesSize> EditedEcs = [];
            bool clothesPropertyChanged = false;

            EditClothes(ref clothesPropertyChanged, editClothesFormViewModel);

            if (OldClothesSizes.Count > 0 && newSelectedSizes.Count > 0)
            {
                await DeleteOrUpdateClothesSizes(
                    OldClothesSizes,
                    newSelectedSizes,
                    clothesPropertyChanged,
                    editClothesFormViewModel);
            }

            if (newSelectedSizes.Count > 0)
                await AddNewClothesSizesAsync(newSelectedSizes, editClothesFormViewModel);

            if (clothesPropertyChanged)
            {
                UpdateEmployeeClothesSizes(EditedEcs, editClothesFormViewModel);
                UpdateEmployees(EditedEcs);
                clothesStore.Update(editClothesFormViewModel.Clothes);
            }

            editClothesFormViewModel.IsSubmitting = false;
            modalNavigationStore.Close();
        }

        private static void EditClothes(ref bool clothesPropertyChanged, EditClothesFormViewModel editClothesFormViewModel)
        {
            if (!editClothesFormViewModel.Clothes.Name.Equals(editClothesFormViewModel.Name))
            {
                editClothesFormViewModel.Clothes.Name = editClothesFormViewModel.Name;
                clothesPropertyChanged = true;
            }

            if (!editClothesFormViewModel.Clothes.Category.Name.Equals(editClothesFormViewModel.Category.Name))
            {
                editClothesFormViewModel.Clothes.Category = editClothesFormViewModel.Category;
                editClothesFormViewModel.Clothes.CategoryGuidId = editClothesFormViewModel.Category.Id;
                clothesPropertyChanged = true;
            }

            if (!editClothesFormViewModel.Clothes.Season.Name.Equals(editClothesFormViewModel.Season.Name))
            {
                editClothesFormViewModel.Clothes.Season = editClothesFormViewModel.Season;
                editClothesFormViewModel.Clothes.SeasonGuidId = editClothesFormViewModel.Season.Id;
                clothesPropertyChanged = true;
            }

            if (!editClothesFormViewModel.Clothes.Comment.Equals(editClothesFormViewModel.Comment))
            {
                editClothesFormViewModel.Clothes.Comment = editClothesFormViewModel.Comment;
                clothesPropertyChanged = true;
            }
        }

        private static List<SizeListingItemViewModel> GetSelectedSizes(EditClothesFormViewModel editClothesFormViewModel)
        {
            return new List<SizeListingItemViewModel>(editClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesUS
                .Any(size => size.IsChecked)
                ? editClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesUS.Where(size => size.IsChecked)
                : editClothesFormViewModel.SizesCategoriesSeasonsListingViewModel.LoadedSizesEU.Where(size => size.IsChecked))
                .ToList();
        }

        private async Task DeleteOrUpdateClothesSizes(
            List<ClothesSize> oldClothesSizes,
            List<SizeListingItemViewModel> newSelectedSizes,
            bool clothesPropertyChanged,
            EditClothesFormViewModel editClothesFormViewModel)
        {            
            foreach (ClothesSize oldClothesSize in oldClothesSizes)
            {
                SizeListingItemViewModel? newSelectedSize = newSelectedSizes
                    .FirstOrDefault(slivm => slivm.Size == oldClothesSize.Size);

                if (newSelectedSize == null)
                {
                    EmployeeClothesSize? assignedClothesSize = employeeClothesSizeStore.EmployeeClothesSizes
                    .FirstOrDefault(ecs => ecs.ClothesSizeGuidId == oldClothesSize.Id);

                    if (assignedClothesSize != null)
                    {
                        ShowErrorMessageBox($"Größe  \"{oldClothesSize.Size}\"  kann nicht enfernt werden, " +
                            "da diese noch vergeben ist!\nDer Bestand wird auf \"0\" gesetzt.", " Bekleidung bearbeiten");

                        UpdateClothesSize(true, oldClothesSize, null, clothesPropertyChanged, editClothesFormViewModel);
                    }
                    else
                        await DeleteClothesSizeAsync(oldClothesSize, editClothesFormViewModel);
                }
                else
                {
                    UpdateClothesSize(false, oldClothesSize, newSelectedSize, clothesPropertyChanged, editClothesFormViewModel);
                    newSelectedSizes.Remove(newSelectedSize);
                }
            }
        }
                
        private async Task DeleteClothesSizeAsync(ClothesSize clothesSizeToDelete, EditClothesFormViewModel editClothesFormViewModel)
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

            editClothesFormViewModel.Clothes.Sizes.Remove(clothesSizeToDelete);
        }

        private void UpdateClothesSize(
            bool makeQuantityZero,
            ClothesSize oldClothesSize,
            SizeListingItemViewModel? newSelectedSize,
            bool clothesPropertyChanged,
            EditClothesFormViewModel editClothesFormViewModel)
        {
            bool clothesSizePropertyChanged = false;

            if (makeQuantityZero)
            {
                oldClothesSize.Quantity = 0;
                clothesSizePropertyChanged = true;
            }
            else
            {
                if (oldClothesSize.Quantity != newSelectedSize.Quantity)
                {
                    oldClothesSize.Quantity = newSelectedSize.Quantity;
                    clothesSizePropertyChanged = true;
                }

                if (!string.Equals(oldClothesSize.Comment, newSelectedSize.Comment))
                {
                    oldClothesSize.Comment = newSelectedSize.Comment;
                    clothesSizePropertyChanged = true;
                }
            }

            if (clothesPropertyChanged)
            {
                oldClothesSize.Clothes = editClothesFormViewModel.Clothes;
                clothesSizePropertyChanged = true;
            }

            if (clothesSizePropertyChanged)
            {
                ClothesSize existingClothesSize = editClothesFormViewModel.Clothes.Sizes
                    .First(cs => cs.Id == oldClothesSize.Id);

                editClothesFormViewModel.Clothes.Sizes.Remove(existingClothesSize);
                editClothesFormViewModel.Clothes.Sizes.Add(oldClothesSize);
                clothesSizeStore.Update(oldClothesSize);
            }
        }

        private async Task AddNewClothesSizesAsync(List<SizeListingItemViewModel> newSelectedSizes, EditClothesFormViewModel editClothesFormViewModel)
        {
            foreach (SizeListingItemViewModel slivm in newSelectedSizes)
            {
                ClothesSize newClothesSize = new(Guid.NewGuid(), editClothesFormViewModel.Clothes, slivm.Size, slivm.Quantity, slivm.Comment)
                {
                    EmployeeClothesSizes = []
                };

                editClothesFormViewModel.Clothes.Sizes.Add(newClothesSize);

                try
                {
                    await clothesSizeStore.AddDataBase(newClothesSize);
                }
                catch
                {
                    ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!", " Bekleidung bearbeiten");
                    editClothesFormViewModel.HasError = true;
                }
            }
        }
        
        private void UpdateEmployeeClothesSizes(List<EmployeeClothesSize> editedEmployeeClothesSizes, EditClothesFormViewModel editClothesFormViewModel)
        {
            foreach (ClothesSize clothesSize in editClothesFormViewModel.Clothes.Sizes)
            {
                List<EmployeeClothesSize> assignedClothesSizes = employeeClothesSizeStore.EmployeeClothesSizes
                    .Where(ecs => ecs.ClothesSizeGuidId == clothesSize.Id)
                    .ToList();

                foreach (EmployeeClothesSize ecs in assignedClothesSizes)
                {
                    ecs.ClothesSize = clothesSize;
                    editedEmployeeClothesSizes.Add(ecs);
                    employeeClothesSizeStore.Update(ecs);
                }
            }
        }

        private void UpdateEmployees(List<EmployeeClothesSize> EditedEmployeeClothesSizes)
        {
            foreach (EmployeeClothesSize employeeClothesSize in EditedEmployeeClothesSizes)
            {
                EmployeeClothesSize existingEcs = employeeClothesSize.Employee.Clothes
                    .First(ecs => ecs.Id == employeeClothesSize.Id);

                employeeClothesSize.Employee.Clothes.Remove(existingEcs);
                employeeClothesSize.Employee.Clothes.Add(employeeClothesSize);
                employeeStore.Update(employeeClothesSize.Employee);
            }
        }
    }
}
