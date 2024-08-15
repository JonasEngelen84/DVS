using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.ListViewItems;
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
        private readonly EditClothesViewModel _updateClothesViewModel = editClothesViewModel;
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
                AddEditClothesFormViewModel updateClothesFormViewModel = _updateClothesViewModel.AddEditClothesFormViewModel;

                updateClothesFormViewModel.IsSubmitting = true;

                // Alle neu ausgewählten ausgewählten Größen mit ihren Mengen in eine ZwischenListe speichern.
                var selectedSizes = (updateClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? updateClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : updateClothesFormViewModel.AddEditListingViewModel.AvailableSizesEU.Where(size => size.IsSelected))
                    .ToList();

                // Sämtliche ClothesSizes aus Listen und DB entfernen, die vom User enfernt wurden
                foreach (ClothesSize cs in updateClothesFormViewModel.Clothes.Sizes)
                {
                    SizeModel? matchingClothesSize = selectedSizes.FirstOrDefault(y => y.GuidID == cs.SizeGuidID);

                    if (matchingClothesSize == null)
                    {
                        cs.Size.ClothesSizes.Remove(cs);
                        updateClothesFormViewModel.Clothes.Sizes.Remove(cs);

                        try
                        {
                            await _clothesStore.DeleteClothesSize(cs.GuidID);
                        }
                        catch (Exception)
                        {
                            messageBoxText = $"Bearbeiten der Bekleidung ist fehlgeschlagen!";
                            caption = " Bekleidung bearbeiten";
                            button = MessageBoxButton.OK;
                            icon = MessageBoxImage.Warning;
                            dialog = MessageBox.Show(messageBoxText, caption, button, icon);
                        }
                    }
                }

                // Prüfen ob der User neue Größen hinzugefügt hat
                foreach (SizeModel size in selectedSizes)
                {
                    ClothesSize? itemToUpdate = updateClothesFormViewModel.Clothes.Sizes.FirstOrDefault(y => y.SizeGuidID == size.GuidID);

                    if (itemToUpdate == null)
                    {
                        ClothesSize newClothesSize = new(Guid.NewGuid(), updateClothesFormViewModel.Clothes, size, size.Quantity);
                        updateClothesFormViewModel.Clothes.Sizes.Add(newClothesSize);
                        size.ClothesSizes.Add(newClothesSize);
                    }
                }

                // Der neuen Clothes-Instanz die GuiID der alten Instanz übergeben 
                Clothes updatedClothes = new(updateClothesFormViewModel.Clothes.GuidID,
                                             updateClothesFormViewModel.ID,
                                             updateClothesFormViewModel.Name,
                                             updateClothesFormViewModel.Category,
                                             updateClothesFormViewModel.Season,
                                             updateClothesFormViewModel.Clothes.Comment);

                // Den ClothesSize-Instanzen ebenfalls die alte GuidID und die neu erstellte Clothes-Instanz übergeben.
                // EmployeeClothesSize-Liste der ClothesSize-Instanzen neu erstellen
                updatedClothes.Sizes = new ObservableCollection<ClothesSize>(
                    updateClothesFormViewModel.Clothes.Sizes.Select(cs => new ClothesSize(cs.GuidID, updatedClothes, cs.Size, selectedSizes
                    .FirstOrDefault(ss => ss.Size == cs.Size.Size).Quantity)
                    {
                        Comment = cs.Comment,
                        EmployeeClothesSizes = new ObservableCollection<EmployeeClothesSize>(
                            cs.EmployeeClothesSizes.Select(ecs => new EmployeeClothesSize(
                                ecs.GuidID,
                                ecs.Employee,
                                updateClothesFormViewModel.Clothes.Sizes.FirstOrDefault(s => s.Size == ecs.ClothesSize.Size),
                                ecs.Quantity)))
                    }));
                
                // Alle alten ClothesSize-Instanzen aus Size-List entfernen
                foreach (ClothesSize cs in updateClothesFormViewModel.Clothes.Sizes)
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

                        updateClothesFormViewModel.IsSubmitting = false;
                        return;
                    }
                }

                // Kategorie und Saison Listen mit der neuen Clothes-Instanz aktualisieren
                updatedClothes.Category?.Clothes.Remove(updateClothesFormViewModel.Clothes);
                updatedClothes.Category?.Clothes.Add(updatedClothes);
                updatedClothes.Season?.Clothes.Remove(updateClothesFormViewModel.Clothes);
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
                }
                finally
                {
                    updateClothesFormViewModel.IsSubmitting = false;
                    _modalNavigationStore.Close();
                }
            }
        }
    }
}
