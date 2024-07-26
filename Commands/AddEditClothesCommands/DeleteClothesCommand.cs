using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Windows;

namespace DVS.Commands.AddEditClothesCommands
{
    public class DeleteClothesCommand(ClothesListingItemViewModel clothesListingItemViewModel,
        ClothesStore clothesStore) : AsyncCommandBase
    {
        private readonly ClothesListingItemViewModel _clothesListingItemViewModel = clothesListingItemViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = $"Die Bekleidung  \"{_clothesListingItemViewModel.Name}\"  wird gelöscht!" +
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

                try
                {
                    await _clothesStore.Delete(clothes.GuidID);
                }
                catch (Exception)
                {
                    _clothesListingItemViewModel.ErrorMessage = "Löschen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    _clothesListingItemViewModel.IsDeleting = false;
                }
            }
        }
    }
}
