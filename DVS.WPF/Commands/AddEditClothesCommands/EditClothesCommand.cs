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
                                    ModalNavigationStore modalNavigationStore) 
                                    : AsyncCommandBase
    {
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
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
            var clothesSizesToRemove = IdentifySizesToRemove(editClothesFormViewModel, selectedSizes);

            await RemoveOldSizesAsync(editClothesFormViewModel, clothesSizesToRemove);

            Clothes updatedClothes = CreateUpdatedClothesInstance(editClothesFormViewModel);
            
            AddNewSizes(editClothesFormViewModel, selectedSizes, updatedClothes);

            await UpdateSizesAndClothesAsync(editClothesFormViewModel, updatedClothes);

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

        private List<ClothesSize> IdentifySizesToRemove(AddEditClothesFormViewModel editClothesFormViewModel, List<SizeModel> selectedSizes)
        {
            return editClothesFormViewModel.Clothes.Sizes
                .Where(cs => selectedSizes.All(y => y.GuidID != cs.SizeGuidID))
                .ToList();
        }

        private async Task RemoveOldSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel, List<ClothesSize> clothesSizesToRemove)
        {
            foreach (var cs in clothesSizesToRemove)
            {
                cs.Size.ClothesSizes.Remove(cs);
                editClothesFormViewModel.Clothes.Sizes.Remove(cs);

                try
                {
                    await _clothesStore.DeleteClothesSize(cs.GuidID);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Bearbeiten der Bekleidung ist fehlgeschlagen!", "Bekleidung bearbeiten");
                    editClothesFormViewModel.HasError = true;
                }
            }
        }

        private Clothes CreateUpdatedClothesInstance(AddEditClothesFormViewModel editClothesFormViewModel)
        {
            return new Clothes(
                editClothesFormViewModel.Clothes.GuidID,
                editClothesFormViewModel.ID,
                editClothesFormViewModel.Name,
                editClothesFormViewModel.Category,
                editClothesFormViewModel.Season,
                editClothesFormViewModel.Clothes.Comment
            );
        }

        private void AddNewSizes(AddEditClothesFormViewModel editClothesFormViewModel, List<SizeModel> selectedSizes, Clothes updatedClothes)
        {
            foreach (SizeModel size in selectedSizes)
            {
                ClothesSize? itemToUpdate = editClothesFormViewModel.Clothes.Sizes.FirstOrDefault(y => y.SizeGuidID == size.GuidID);

                if (itemToUpdate == null)
                {
                    ClothesSize newClothesSize = new(Guid.NewGuid(), updatedClothes, size, size.Quantity, null);
                    updatedClothes.Sizes.Add(newClothesSize);
                    size.ClothesSizes.Add(newClothesSize);
                }
            }
        }

        private async Task UpdateSizesAndClothesAsync(AddEditClothesFormViewModel editClothesFormViewModel, Clothes updatedClothes)
        {
            updatedClothes.Sizes = new ObservableCollection<ClothesSize>(
                editClothesFormViewModel.Clothes.Sizes.Select(cs => new ClothesSize(
                    cs.GuidID, updatedClothes, cs.Size, cs.Quantity, cs.Comment)
                {
                    EmployeeClothesSizes = new ObservableCollection<EmployeeClothesSize>(
                        cs.EmployeeClothesSizes.Select(ecs => new EmployeeClothesSize(
                            ecs.GuidID,
                            ecs.Employee,
                            editClothesFormViewModel.Clothes.Sizes.FirstOrDefault(s => s.Size == ecs.ClothesSize.Size),
                            ecs.Quantity,
                            ecs.Comment)))
                }));

            updatedClothes.Category?.Clothes.Remove(editClothesFormViewModel.Clothes);
            updatedClothes.Category?.Clothes.Add(updatedClothes);
            updatedClothes.Season?.Clothes.Remove(editClothesFormViewModel.Clothes);
            updatedClothes.Season?.Clothes.Add(updatedClothes);

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
