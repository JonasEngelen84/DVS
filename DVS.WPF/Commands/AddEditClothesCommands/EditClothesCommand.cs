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
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        //private readonly EmployeeStore _employeeStore = employeeStore;
        //private readonly SizeStore _sizeStore = sizeStore;
        //private readonly CategoryStore _categoryStore = categoryStore;
        //private readonly SeasonStore _seasonStore = seasonStore;
        //private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        
        public override async Task ExecuteAsync(object parameter)
        {
            AddEditClothesFormViewModel editClothesFormViewModel = _editClothesViewModel.AddEditClothesFormViewModel;

            if (Confirm($"Soll die Bekleidung  \"{editClothesFormViewModel.Name}\"  und Ihre Schnittstellen bearbeiten werden?", "Bekleidung bearbeiten"))
            {
                editClothesFormViewModel.HasError = false;
                editClothesFormViewModel.IsSubmitting = true;

                Clothes updatedClothes = new(editClothesFormViewModel.Clothes.GuidId,
                                             editClothesFormViewModel.Id,
                                             editClothesFormViewModel.Name,
                                             editClothesFormViewModel.Category,
                                             editClothesFormViewModel.Season,
                                             editClothesFormViewModel.Comment)
                {
                    Sizes = []
                };

                // Die vom User gewählten Größen/SizeModel auflisten
                List<SizeModel> selectedSizes = (editClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? editClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : editClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesEU.Where(size => size.IsSelected))
                    .ToList();

                //await DeleteRemovedClothesSizesAsync(editClothesFormViewModel, selectedSizes);
                await CreateUpdateOrAddClothesSizesAsync(editClothesFormViewModel, updatedClothes, selectedSizes);

                // Aktualisieren der Clothes-Liste von Category und Season
                Clothes ClothesToRemove = updatedClothes.Category.Clothes.FirstOrDefault(c => c.GuidId == updatedClothes.GuidId);
                updatedClothes.Category.Clothes.Remove(ClothesToRemove);
                updatedClothes.Category.Clothes.Add(updatedClothes);

                ClothesToRemove = updatedClothes.Season.Clothes.FirstOrDefault(c => c.GuidId == updatedClothes.GuidId);
                updatedClothes.Season.Clothes.Remove(ClothesToRemove);
                updatedClothes.Season.Clothes.Add(updatedClothes);

                try
                {
                    await _clothesStore.Update(updatedClothes);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!", "EditClothesCommand UpdateClothesAsync");

                    editClothesFormViewModel.HasError = true;
                }

                editClothesFormViewModel.IsSubmitting = false;

                _modalNavigationStore.Close();
            }
        }

        private async Task DeleteRemovedClothesSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel, List<SizeModel> selectedSizes)
        {
            // IEnumerable kann nicht in Foreach durchlaufen und bearbeitet werden!
            List<ClothesSize> ClothesSizesToDelete = new(editClothesFormViewModel.Clothes.Sizes);
            foreach (ClothesSize clothesSize in ClothesSizesToDelete)
            {
                SizeModel existingSize = selectedSizes.FirstOrDefault(sm => sm.GuidId == clothesSize.SizeGuidId);

                if (existingSize == null)
                {
                    // Aktualisieren der ClothesSize-Liste des SizeModel
                    ClothesSize existingClothesSize = clothesSize.Size.ClothesSizes.FirstOrDefault(cs => cs.Size.GuidId == clothesSize.Size.GuidId);
                    clothesSize.Size.ClothesSizes.Remove(clothesSize);

                    try
                    {
                        await _clothesSizeStore.Delete(clothesSize);
                    }
                    catch (Exception)
                    {
                        ShowErrorMessageBox("Löschen der Bekleidungsgröße ist fehlgeschlagen!", "EditClothesCommand DeleteRemovedClothesSizesAsync");

                        editClothesFormViewModel.HasError = true;
                    }
                }
            }
        }

        private async Task CreateUpdateOrAddClothesSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes, List<SizeModel> selectedSizes)
        {
            foreach (SizeModel size in selectedSizes)
            {
                ClothesSize existingClothesSize = editClothesFormViewModel.Clothes.Sizes.FirstOrDefault(cs => cs.Size.GuidId == size.GuidId);
                
                if (existingClothesSize == null)
                {
                    ClothesSize newClothesSize = new(Guid.NewGuid(), updatedClothes, size, size.Quantity, "");

                    //try
                    //{
                    //    await _clothesSizeStore.Add(newClothesSize);
                    //}
                    //catch
                    //{
                    //    ShowErrorMessageBox("Erstellen der Bekleidungsgröße ist fehlgeschlagen!", "EditClothesCommand CreateAndAddNewClothesSizesAsync");

                    //    editClothesFormViewModel.HasError = true;
                    //}

                    // Den ClothesSize-Listen des SizeModel und des neu erstellten Clothes, das neu erstellte ClothesSize hinzufügen
                    updatedClothes.Sizes.Add(newClothesSize);
                    //size.ClothesSizes.Add(newClothesSize);
                }
                else
                {
                    if (existingClothesSize.Quantity != size.Quantity)
                    {
                        ClothesSize updatedClothesSize = new(existingClothesSize.GuidId, updatedClothes, size, size.Quantity, existingClothesSize.Comment)
                        {
                            EmployeeClothesSizes = new ObservableCollection<EmployeeClothesSize>(existingClothesSize.EmployeeClothesSizes)
                        };

                        //try
                        //{
                        //    await _clothesSizeStore.Update(updatedClothesSize);
                        //}
                        //catch (Exception)
                        //{
                        //    ShowErrorMessageBox("Bearbeiten der Bekleidungsgröße ist fehlgeschlagen!", "EditClothesCommand UpdateClothesSizesAsync");

                        //    editClothesFormViewModel.HasError = true;
                        //}

                        // Der ClothesSize-Liste des neu erstellten Clothes, das neu erstellte ClothesSize hinzufügen
                        updatedClothes.Sizes.Add(updatedClothesSize);

                        // Aktualisieren der ClothesSize-Liste des SizeModel
                        ClothesSize ClothesSizeToRemove = updatedClothesSize.Size.ClothesSizes.FirstOrDefault(cs => cs.GuidId == updatedClothesSize.GuidId);
                        size.ClothesSizes.Remove(ClothesSizeToRemove);
                        size.ClothesSizes.Add(updatedClothesSize);
                    }
                    else
                        updatedClothes.Sizes.Add(existingClothesSize);
                }
            }
        }

        //private async Task UpdateSizeAsync(AddEditClothesFormViewModel editClothesFormViewModel, List<SizeModel> selectedSizes)
        //{
        //    foreach (SizeModel sm in selectedSizes)
        //    {
        //        try
        //        {
        //            await _sizeStore.Update(sm);
        //        }
        //        catch (Exception)
        //        {
        //            ShowErrorMessageBox("Updaten der Size in Datenbank ist fehlgeschlagen!", "EditClothesCommand UpdateSizeAsync");
                    
        //            editClothesFormViewModel.HasError = true;
        //        }
        //    }
        //}

        //private async Task UpdateCategoryAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes)
        //{
        //    try
        //    {
        //        await _categoryStore.Update(updatedClothes.Category, null);
        //    }
        //    catch (Exception)
        //    {
        //        ShowErrorMessageBox("Updaten der Category in Datenbank ist fehlgeschlagen!", "EditClothesCommand UpdateCategoryAsync");
                
        //        editClothesFormViewModel.HasError = true;
        //    }
        //}

        //private async Task UpdateSeasonAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes)
        //{
        //    try
        //    {
        //        await _seasonStore.Update(updatedClothes.Season, null);
        //    }
        //    catch (Exception)
        //    {
        //        ShowErrorMessageBox("Updaten der Season in Datenbank ist fehlgeschlagen!", "EditClothesCommand UpdateSeasonAsync");

        //        editClothesFormViewModel.HasError = true;
        //    }
        //}

        //private async Task UpdateEmployeeClothesSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes)
        //{
        //    foreach (EmployeeClothesSize employeeClothesSize in _employeeClothesSizesStore.EmployeeClothesSizes)
        //    {
        //        ClothesSize existingItem = updatedClothes.Sizes.FirstOrDefault(cs => cs.GuidID == employeeClothesSize.ClothesSizeGuidID);

        //        if (existingItem != null)
        //        {
        //            EmployeeClothesSize UpdatedEmployeeClothesSize = new(employeeClothesSize.GuidID,
        //                                                                 employeeClothesSize.Employee,
        //                                                                 existingItem,
        //                                                                 (int)employeeClothesSize.Quantity,
        //                                                                 employeeClothesSize.Comment);

        //            EmployeeClothesSize itemToRemove = employeeClothesSize.Employee.Clothes.FirstOrDefault(ecs => ecs.GuidID == UpdatedEmployeeClothesSize.GuidID);

        //            if (itemToRemove != null)
        //            {
        //                employeeClothesSize.Employee.Clothes.Remove(itemToRemove);
        //                employeeClothesSize.Employee.Clothes.Add(UpdatedEmployeeClothesSize);
        //            }


        //            itemToRemove = UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.FirstOrDefault(ecs => ecs.GuidID == UpdatedEmployeeClothesSize.GuidID);

        //            if (itemToRemove != null)
        //            {
        //                UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.Remove(itemToRemove);
        //                UpdatedEmployeeClothesSize.ClothesSize.EmployeeClothesSizes.Add(UpdatedEmployeeClothesSize);
        //            }


        //            try
        //            {
        //                await _employeeClothesSizesStore.Update(UpdatedEmployeeClothesSize);
        //            }
        //            catch (Exception)
        //            {
        //                ShowErrorMessageBox("Updaten der EmployeeClothesSize in Datenbank ist fehlgeschlagen!", "EditClothesCommand UpdateEmployeeClothesSizesAsync");

        //                editClothesFormViewModel.HasError = true;
        //            }

        //            UpdateEmployeeAsync(editClothesFormViewModel, employeeClothesSize.Employee);
        //        }
        //    }
        //}

        //private async Task UpdateEmployeeAsync(AddEditClothesFormViewModel editClothesFormViewModel, Employee updatedEmployee)
        //{
        //    try
        //    {
        //        await _employeeStore.Update(updatedEmployee);
        //    }
        //    catch (Exception)
        //    {
        //        ShowErrorMessageBox("Updaten des Employee in Datenbank ist fehlgeschlagen!", "EditClothesCommand UpdateEmployeeAsync");

        //        editClothesFormViewModel.HasError = true;
        //    }
        //}

    }
}
