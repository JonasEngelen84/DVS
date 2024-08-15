using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;
using System.Windows;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class ClearSizesCommand(ClothesListingItemViewModel clothesListingItemViewModel, ClothesStore clothesStore) : AsyncCommandBase
    {
        private readonly ClothesListingItemViewModel _clothesListingItemViewModel = clothesListingItemViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = $"Alle Größen der Bekleidung  \"{_clothesListingItemViewModel.Name}\"  werden gelöscht!" +
                $"\nDie Kleidungsstücke, dieser Bekleidung, bleiben den Mitarbeitern erhalten." +
                $"\n\nLöschen fortsetzen?";
            string caption = "Bekleidung löschen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                _clothesListingItemViewModel.IsDeleting = true;

                // Sämtliche ClothesSizes aus SizeModel-Liste und DB entfernen
                foreach (ClothesSize cs in _clothesListingItemViewModel.Clothes.Sizes)
                {
                    cs.Size.ClothesSizes.Remove(cs);

                    try
                    {
                        await _clothesStore.DeleteClothesSize(cs.GuidID);
                    }
                    catch (Exception)
                    {
                        messageBoxText = $"Löschen der Größen ist fehlgeschlagen!";
                        caption = " Alle Bekleidungsgrößen löschen";
                        button = MessageBoxButton.OK;
                        icon = MessageBoxImage.Warning;
                        dialog = MessageBox.Show(messageBoxText, caption, button, icon);
                    }
                }

                Clothes updatedClothes = new(_clothesListingItemViewModel.Clothes.GuidID,
                                             _clothesListingItemViewModel.ID,
                                             _clothesListingItemViewModel.Name,
                                             _clothesListingItemViewModel.Category,
                                             _clothesListingItemViewModel.Season,
                                             _clothesListingItemViewModel.Comment)
                {
                    Sizes = []
                };

                // Kategorie und Saison Listen mit der neuen Clothes-Instanz aktualisieren
                updatedClothes.Category?.Clothes.Remove(_clothesListingItemViewModel.Clothes);
                updatedClothes.Category?.Clothes.Add(updatedClothes);
                updatedClothes.Season?.Clothes.Remove(_clothesListingItemViewModel.Clothes);
                updatedClothes.Season?.Clothes.Add(updatedClothes);

                try
                {
                    await _clothesStore.Update(updatedClothes);
                }
                catch (Exception)
                {
                    messageBoxText = $"Update der Bekleidung ist fehlgeschlagen!";
                    caption = " Alle Bekleidungsgrößen löschen";
                    button = MessageBoxButton.OK;
                    icon = MessageBoxImage.Warning;
                    dialog = MessageBox.Show(messageBoxText, caption, button, icon);
                }
                finally
                {
                    _clothesListingItemViewModel.IsDeleting = false;
                }
            }
        }
    }
}
