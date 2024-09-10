using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Collections.ObjectModel;
using System.Windows;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class EditClothesCommand(EditClothesViewModel editClothesViewModel,
                                    ClothesStore clothesStore,
                                    SizeStore sizeStore,
                                    CategoryStore categoryStore,
                                    SeasonStore seasonStore,
                                    ClothesSizeStore clothesSizeStore,
                                    ModalNavigationStore modalNavigationStore) 
                                    : AsyncCommandBase
    {
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        
        public override async Task ExecuteAsync(object parameter)
        {
            if (ConfirmEditClothes())
            {
                await ProcessEditClothesAsync();
            }
        }

        private bool ConfirmEditClothes()
        {
            string messageBoxText = "Bekleidung bearbeiten?";
            string caption = "Bekleidung bearbeiten";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);
            return dialog == MessageBoxResult.Yes;
        }

        private async Task ProcessEditClothesAsync()
        {
            AddEditClothesFormViewModel editClothesFormViewModel = _editClothesViewModel.AddEditClothesFormViewModel;
            editClothesFormViewModel.HasError = false;
            editClothesFormViewModel.IsSubmitting = true;

            var selectedSizes = GetSelectedSizes(editClothesFormViewModel);
            var clothesSizesToRemove = IdentifyRemovedSizes(editClothesFormViewModel, selectedSizes);

            Clothes updatedClothes = CreateUpdatedClothesInstance(editClothesFormViewModel);

            await RemoveRemovedSizesAsync(editClothesFormViewModel, clothesSizesToRemove);
            
            await AddClothesSizesAsync(editClothesFormViewModel, selectedSizes, updatedClothes);

            await UpdateCategoryDbAndSeasonDbAsync(editClothesFormViewModel, updatedClothes);

            await UpdateClothesDbAsync(editClothesFormViewModel, updatedClothes);

            editClothesFormViewModel.IsSubmitting = false;

            _modalNavigationStore.Close();
        }

        private List<SizeModel> GetSelectedSizes(AddEditClothesFormViewModel editClothesFormViewModel)
        {
            return (editClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? editClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : editClothesFormViewModel.AddEditListingViewModel.AvailableSizesEU.Where(size => size.IsSelected))
                    .ToList();
        }

        private List<ClothesSize> IdentifyRemovedSizes(AddEditClothesFormViewModel editClothesFormViewModel, List<SizeModel> selectedSizes)
        {
            return editClothesFormViewModel.Clothes.Sizes
                .Where(cs => selectedSizes.All(y => y.GuidID != cs.SizeGuidID))
                .ToList();
        }
        
        private Clothes CreateUpdatedClothesInstance(AddEditClothesFormViewModel editClothesFormViewModel)
        {
            return new Clothes(editClothesFormViewModel.Clothes.GuidID,
                               editClothesFormViewModel.ID,
                               editClothesFormViewModel.Name,
                               editClothesFormViewModel.Category,
                               editClothesFormViewModel.Season,
                               editClothesFormViewModel.Clothes.Comment);
        }

        private async Task RemoveRemovedSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel, List<ClothesSize> clothesSizesToRemove)
        {
            foreach (ClothesSize cs in clothesSizesToRemove)
            {
                cs.Size.ClothesSizes.Remove(cs);
                editClothesFormViewModel.Clothes.Sizes.Remove(cs);

                try
                {
                    await _sizeStore.Update(cs.Size);
                    await _clothesSizeStore.Delete(cs.GuidID);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!", "Bekleidung bearbeiten");
                    editClothesFormViewModel.HasError = true;
                }
            }
        }

        private async Task AddClothesSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel, List<SizeModel> selectedSizes, Clothes updatedClothes)
        {
            foreach (SizeModel size in selectedSizes)
            {
                ClothesSize existingClothesSize = editClothesFormViewModel.Clothes.Sizes.FirstOrDefault(cs => cs.Size.GuidID == size.GuidID);
                SizeModel existingSize = existingClothesSize?.Size;
                ClothesSize newClothesSize;

                if (existingClothesSize != null)
                {
                    newClothesSize = new(existingSize.GuidID, updatedClothes, size, size.Quantity, existingClothesSize.Comment)
                    {
                        EmployeeClothesSizes = new ObservableCollection<EmployeeClothesSize>(existingClothesSize.EmployeeClothesSizes)
                    };
                }
                else
                {
                    newClothesSize = new(Guid.NewGuid(), updatedClothes, size, size.Quantity, "")
                    {
                        EmployeeClothesSizes = new ObservableCollection<EmployeeClothesSize>(existingClothesSize.EmployeeClothesSizes)
                    };
                }

                updatedClothes.Sizes.Add(newClothesSize);
                size.ClothesSizes.Add(newClothesSize);

                try
                {
                    await _sizeStore.Update(size);

                    if (existingClothesSize != null)
                    {
                        await _clothesSizeStore.Update(newClothesSize);
                    }
                    else
                    {
                        await _clothesSizeStore.Add(newClothesSize);
                    }
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung bearbeiten");
                    editClothesFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateCategoryDbAndSeasonDbAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes)
        {
            updatedClothes.Category.Clothes.Remove(editClothesFormViewModel.Clothes);
            updatedClothes.Category.Clothes.Add(updatedClothes);
            updatedClothes.Season.Clothes.Remove(editClothesFormViewModel.Clothes);
            updatedClothes.Season.Clothes.Add(updatedClothes);

            try
            {
                await _categoryStore.Update(updatedClothes.Category, null);
                await _seasonStore.Update(updatedClothes.Season, null);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Bekleidung erstellen");

                editClothesFormViewModel.HasError = true;
            }
        }

        private async Task UpdateClothesDbAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes)
        {            
            try
            {
                await _clothesStore.Update(updatedClothes);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!", "Bekleidung bearbeiten");
                editClothesFormViewModel.HasError = true;
            }
        }

        private void ShowErrorMessageBox(string message, string title)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(message, title, button, icon);
        }
    }
}
