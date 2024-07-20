using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Windows;

namespace DVS.Commands.DVSHeadViewCommands
{
    public class ClearSizesCommand(ClothesListingItemViewModel clothesListingItemViewModel,
        ClothesStore clothesStore) : AsyncCommandBase
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
                _clothesListingItemViewModel.ErrorMessage = null;
                _clothesListingItemViewModel.IsDeleting = true;

                ClothesModel clothes = _clothesListingItemViewModel.Clothes;
                clothes.Sizes.Clear();

                try
                {
                    await _clothesStore.Update(clothes);
                }
                catch (Exception)
                {
                    _clothesListingItemViewModel.ErrorMessage = "Löschen der Größen ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    _clothesListingItemViewModel.IsDeleting = false;
                }
            }
        }
    }
}
