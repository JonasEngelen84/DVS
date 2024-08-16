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
            string messageBoxText = "Bekleidung bearbeiten?";
            string caption = "Bekleidung bearbeiten";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                AddEditClothesFormViewModel editClothesFormViewModel = _editClothesViewModel.AddEditClothesFormViewModel;
                editClothesFormViewModel.HasError = false;
                editClothesFormViewModel.IsSubmitting = true;

                // Alle neu ausgewählten ausgewählten Größen mit ihren Mengen in eine ZwischenListe speichern.
                var selectedSizes = (editClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? editClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : editClothesFormViewModel.AddEditListingViewModel.AvailableSizesEU.Where(size => size.IsSelected))
                    .ToList();

                // Sämtliche ClothesSizes aus Listen und DB entfernen, die vom User enfernt wurden
                foreach (ClothesSize cs in editClothesFormViewModel.Clothes.Sizes)
                {
                    SizeModel? matchingClothesSize = selectedSizes.FirstOrDefault(y => y.GuidID == cs.SizeGuidID);

                    if (matchingClothesSize == null)
                    {
                        cs.Size.ClothesSizes.Remove(cs);
                        editClothesFormViewModel.Clothes.Sizes.Remove(cs);

                        try
                        {
                            await _clothesStore.DeleteClothesSize(cs.GuidID);
                        }
                        catch (Exception)
                        {
                            messageBoxText = $"Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                            caption = " Bekleidung bearbeiten";
                            button = MessageBoxButton.OK;
                            icon = MessageBoxImage.Warning;
                            dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                            editClothesFormViewModel.HasError = true;
                        }
                    }
                }

                // Prüfen ob der User neue Größen hinzugefügt hat
                foreach (SizeModel size in selectedSizes)
                {
                    ClothesSize? itemToUpdate = editClothesFormViewModel.Clothes.Sizes.FirstOrDefault(y => y.SizeGuidID == size.GuidID);

                    if (itemToUpdate == null)
                    {
                        ClothesSize newClothesSize = new(Guid.NewGuid(), editClothesFormViewModel.Clothes, size, size.Quantity, null);
                        editClothesFormViewModel.Clothes.Sizes.Add(newClothesSize);
                        size.ClothesSizes.Add(newClothesSize);
                    }
                }

                // Der neuen Clothes-Instanz die GuiID der alten Instanz übergeben 
                Clothes updatedClothes = new(editClothesFormViewModel.Clothes.GuidID,
                                             editClothesFormViewModel.ID,
                                             editClothesFormViewModel.Name,
                                             editClothesFormViewModel.Category,
                                             editClothesFormViewModel.Season,
                                             editClothesFormViewModel.Clothes.Comment);

                // Den ClothesSize-Instanzen ebenfalls die alte GuidID und die neu erstellte Clothes-Instanz übergeben.
                // EmployeeClothesSize-Liste der ClothesSize-Instanzen neu erstellen
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
                
                // Alle alten ClothesSize-Instanzen aus Size-List entfernen
                foreach (ClothesSize cs in editClothesFormViewModel.Clothes.Sizes)
                {
                    cs.Size.ClothesSizes.Remove(cs);
                }

                // Alle neuen ClothesSize-Instanzen der Size-Liste hinzufügen und DB updaten
                foreach (ClothesSize cs in updatedClothes.Sizes)
                {
                    cs.Size.ClothesSizes.Add(cs);

                    try
                    {
                        await _clothesStore.UpdateClothesSize(cs);
                    }
                    catch (Exception)
                    {
                        messageBoxText = $"Bearbeiten der Bekleidung ist fehlgeschlagen!";
                        caption = " Bekleidung bearbeiten";
                        button = MessageBoxButton.OK;
                        icon = MessageBoxImage.Warning;
                        dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                        editClothesFormViewModel.HasError = true;
                    }
                }

                // Kategorie und Saison Listen mit der neuen Clothes-Instanz aktualisieren
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
                    messageBoxText = $"Update der Bekleidung ist fehlgeschlagen!";
                    caption = " Bekleidung bearbeiten";
                    button = MessageBoxButton.OK;
                    icon = MessageBoxImage.Warning;
                    dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                    editClothesFormViewModel.HasError = true;
                }
                finally
                {
                    editClothesFormViewModel.IsSubmitting = false;
                    _modalNavigationStore.Close();
                }
            }
        }
    }
}
