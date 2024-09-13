using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Collections.ObjectModel;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class EditClothesCommand(EditClothesViewModel editClothesViewModel,
                                    EmployeeStore employeeStore,
                                    ClothesStore clothesStore,
                                    SizeStore sizeStore,
                                    CategoryStore categoryStore,
                                    SeasonStore seasonStore,
                                    ClothesSizeStore clothesSizeStore,
                                    EmployeeClothesSizesStore employeeClothesSizesStore,
                                    ModalNavigationStore modalNavigationStore) 
                                    : AsyncCommandBase
    {
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        
        public override async Task ExecuteAsync(object parameter)
        {
            AddEditClothesFormViewModel editClothesFormViewModel = _editClothesViewModel.AddEditClothesFormViewModel;

            if (Confirm($"Soll die Bekleidung  \"{editClothesFormViewModel.Name}\"  und Ihre Schnittstellen bearbeiten werden?", "Bekleidung bearbeiten"))
            {
                editClothesFormViewModel.HasError = false;
                editClothesFormViewModel.IsSubmitting = true;

                var selectedSizes = GetSelectedSizes(editClothesFormViewModel);

                Clothes updatedClothes = CreateUpdatedClothesInstance(editClothesFormViewModel);

                await DeleteRemovedClothesSizesAsync(editClothesFormViewModel, updatedClothes, selectedSizes);
                await CreateAndAddNewClothesSizesAsync(editClothesFormViewModel, updatedClothes, selectedSizes);
                await UpdateClothesSizesAsync(editClothesFormViewModel, updatedClothes, selectedSizes);
                await UpdateSizeModelAsync(editClothesFormViewModel, selectedSizes);
                await UpdateClothesAsync(editClothesFormViewModel, updatedClothes);
                await UpdateCategoryAsync(editClothesFormViewModel, updatedClothes);
                await UpdateSeasonAsync(editClothesFormViewModel, updatedClothes);
                await UpdateEmployeeClothesSizesAsync(editClothesFormViewModel, updatedClothes);

                editClothesFormViewModel.IsSubmitting = false;

                _modalNavigationStore.Close();
            }
        }

        private static List<SizeModel> GetSelectedSizes(AddEditClothesFormViewModel editClothesFormViewModel)
        {
            return (editClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? editClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : editClothesFormViewModel.AddEditListingViewModel.AvailableSizesEU.Where(size => size.IsSelected))
                    .ToList();
        }

        private static Clothes CreateUpdatedClothesInstance(AddEditClothesFormViewModel editClothesFormViewModel)
        {
            return new Clothes(editClothesFormViewModel.Clothes.GuidID,
                               editClothesFormViewModel.ID,
                               editClothesFormViewModel.Name,
                               editClothesFormViewModel.Category,
                               editClothesFormViewModel.Season,
                               editClothesFormViewModel.Clothes.Comment)
            {
                Sizes = new ObservableCollection<ClothesSize>(editClothesFormViewModel.Clothes.Sizes)
            };
        }

        private async Task DeleteRemovedClothesSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes, List<SizeModel> selectedSizes)
        {
            foreach (ClothesSize cs in updatedClothes.Sizes)
            {
                SizeModel existingItem = selectedSizes.FirstOrDefault(sm => sm.GuidID == cs.SizeGuidID);

                if (existingItem == null)
                {
                    updatedClothes.Sizes.Remove(cs);
                    cs.Size.ClothesSizes.Remove(cs);

                    try
                    {
                        await _clothesSizeStore.Delete(cs.GuidID);
                    }
                    catch (Exception)
                    {
                        ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung bearbeiten");
                        editClothesFormViewModel.HasError = true;
                    }
                }
            }
        }

        private async Task CreateAndAddNewClothesSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes, List<SizeModel> selectedSizes)
        {
            foreach (SizeModel size in selectedSizes)
            {
                ClothesSize existingClothesSize = updatedClothes.Sizes.FirstOrDefault(cs => cs.Size.GuidID == size.GuidID);
                
                if (existingClothesSize == null)
                {
                    ClothesSize newClothesSize = new(Guid.NewGuid(), updatedClothes, size, size.Quantity, "");

                    updatedClothes.Sizes.Add(newClothesSize);
                    size.ClothesSizes.Add(newClothesSize);

                    try
                    {
                        await _clothesSizeStore.Add(newClothesSize);
                    }
                    catch (Exception)
                    {
                        ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung bearbeiten");
                        editClothesFormViewModel.HasError = true;
                    }
                }
            }
        }

        private async Task UpdateClothesSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes, List<SizeModel> selectedSizes)
        {
            foreach (SizeModel size in selectedSizes)
            {
                ClothesSize existingClothesSize = updatedClothes.Sizes.FirstOrDefault(cs => cs.Size.GuidID == size.GuidID);

                if (existingClothesSize != null)
                {
                    ClothesSize updatedClothesSize = new(existingClothesSize.GuidID, updatedClothes, size, size.Quantity, existingClothesSize.Comment)
                    {
                        EmployeeClothesSizes = new ObservableCollection<EmployeeClothesSize>(existingClothesSize.EmployeeClothesSizes)
                    };

                    ClothesSize itemToRemove = updatedClothes.Sizes.FirstOrDefault(cs => cs.GuidID == updatedClothesSize.GuidID);

                    updatedClothes.Sizes.Remove(itemToRemove);
                    updatedClothes.Sizes.Add(updatedClothesSize);

                    itemToRemove = updatedClothesSize.Size.ClothesSizes.FirstOrDefault(cs => cs.GuidID == updatedClothesSize.GuidID);

                    updatedClothesSize.Size.ClothesSizes.Remove(itemToRemove);
                    updatedClothesSize.Size.ClothesSizes.Add(itemToRemove);

                    try
                    {
                        await _clothesSizeStore.Update(updatedClothesSize);
                    }
                    catch (Exception)
                    {
                        ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung bearbeiten");
                        editClothesFormViewModel.HasError = true;
                    }
                }
            }
        }

        private async Task UpdateClothesAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes)
        {
            Clothes itemToRemove = updatedClothes.Category.Clothes.FirstOrDefault(c => c.GuidID == updatedClothes.GuidID);

            if (itemToRemove != null)
            {
                updatedClothes.Category.Clothes.Remove(itemToRemove);
                updatedClothes.Category.Clothes.Add(updatedClothes);
            }

            itemToRemove = updatedClothes.Season.Clothes.FirstOrDefault(c => c.GuidID == updatedClothes.GuidID);

            if (itemToRemove != null)
            {
                updatedClothes.Season.Clothes.Remove(itemToRemove);
                updatedClothes.Season.Clothes.Add(updatedClothes);
            }

            try
            {
                await _clothesStore.Update(updatedClothes);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung bearbeiten");
                editClothesFormViewModel.HasError = true;
            }
        }

        private async Task UpdateSizeModelAsync(AddEditClothesFormViewModel editClothesFormViewModel, List<SizeModel> selectedSizes)
        {
            foreach (SizeModel sm in selectedSizes)
            {
                try
                {
                    await _sizeStore.Update(sm);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung bearbeiten");
                    editClothesFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateCategoryAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes)
        {
            try
            {
                await _categoryStore.Update(updatedClothes.Category, null);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung bearbeiten");
                editClothesFormViewModel.HasError = true;
            }
        }

        private async Task UpdateSeasonAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes)
        {
            try
            {
                await _seasonStore.Update(updatedClothes.Season, null);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung bearbeiten");
                editClothesFormViewModel.HasError = true;
            }
        }

        private async Task UpdateEmployeeClothesSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes)
        {
            foreach (EmployeeClothesSize employeeClothesSize in _employeeClothesSizesStore.EmployeeClothesSizes)
            {
                ClothesSize existingItem = updatedClothes.Sizes.FirstOrDefault(cs => cs.GuidID == employeeClothesSize.ClothesSizeGuidID);

                if (existingItem != null)
                {
                    EmployeeClothesSize UpdatedEmployeeClothesSize = new(employeeClothesSize.GuidID,
                                                                         employeeClothesSize.Employee,
                                                                         existingItem,
                                                                         (int)employeeClothesSize.Quantity,
                                                                         employeeClothesSize.Comment);

                    EmployeeClothesSize itemToRemove = employeeClothesSize.Employee.Clothes.FirstOrDefault(ecs => ecs.GuidID == UpdatedEmployeeClothesSize.GuidID);

                    if (itemToRemove != null)
                    {
                        employeeClothesSize.Employee.Clothes.Remove(itemToRemove);
                        employeeClothesSize.Employee.Clothes.Add(UpdatedEmployeeClothesSize);
                    }


                    itemToRemove = UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.FirstOrDefault(ecs => ecs.GuidID == UpdatedEmployeeClothesSize.GuidID);

                    if (itemToRemove != null)
                    {
                        UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.Remove(itemToRemove);
                        UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.Add(UpdatedEmployeeClothesSize);
                    }


                    try
                    {
                        await _employeeClothesSizesStore.Update(UpdatedEmployeeClothesSize);
                    }
                    catch (Exception)
                    {
                        ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung bearbeiten");
                        editClothesFormViewModel.HasError = true;
                    }

                    UpdateEmployeeAsync(editClothesFormViewModel, employeeClothesSize.Employee);
                }
            }
        }

        private async Task UpdateEmployeeAsync(AddEditClothesFormViewModel editClothesFormViewModel, Employee updatedEmployee)
        {
            try
            {
                await _employeeStore.Update(updatedEmployee);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung bearbeiten");
                editClothesFormViewModel.HasError = true;
            }
        }

    }
}
