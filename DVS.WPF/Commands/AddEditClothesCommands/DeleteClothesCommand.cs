using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;
using System.Windows;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class DeleteClothesCommand(ClothesListingItemViewModel clothesListingItemViewModel, ClothesStore clothesStore) : AsyncCommandBase
    {
        private readonly ClothesListingItemViewModel _clothesListingItemViewModel = clothesListingItemViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override async Task ExecuteAsync(object parameter)
        {
            _clothesListingItemViewModel.HasError = false;
            _clothesListingItemViewModel.IsDeleting = true;

            string messageBoxText = $"Die Bekleidung  \"{_clothesListingItemViewModel.Name}\"  wird gelöscht!" +
                $"\nDie Kleidungsstücke, dieser Bekleidung, bleiben den Mitarbeitern erhalten." +
                $"\n\nLöschen fortsetzen?";
            string caption = "Bekleidung löschen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                Clothes clothes = _clothesListingItemViewModel.Clothes;

                foreach (ClothesSize size in clothes.Sizes)
                {
                    size.Size.ClothesSizes.Remove(size);
                }

                clothes.Category?.Clothes.Remove(clothes);
                clothes.Season?.Clothes.Remove(clothes);

                try
                {
                    await _clothesStore.Delete(clothes);
                }
                catch (Exception)
                {
                    messageBoxText = $"Bearbeiten der Bekleidung ist fehlgeschlagen!";
                    caption = " Bekleidung bearbeiten";
                    button = MessageBoxButton.OK;
                    icon = MessageBoxImage.Warning;
                    dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                    _clothesListingItemViewModel.HasError = true;
                }
                finally
                {
                    _clothesListingItemViewModel.IsDeleting = false;
                }
            }
        }
    }
}
