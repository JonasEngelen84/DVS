using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class DeleteClothesCommand(ClothesListingItemViewModel clothesListingItemViewModel, ClothesStore clothesStore) : AsyncCommandBase
    {
        private readonly ClothesListingItemViewModel _clothesListingItemViewModel = clothesListingItemViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override async Task ExecuteAsync(object parameter)
        {
            if (Confirm($"Die Bekleidung  \"{_clothesListingItemViewModel.Name}\"  wird gelöscht!" +
                $"\nDie Kleidungsstücke, dieser Bekleidung, bleiben den Mitarbeitern erhalten." +
                $"\n\nLöschen fortsetzen?", "Bekleidung löschen"))
            {
                _clothesListingItemViewModel.HasError = false;
                _clothesListingItemViewModel.IsDeleting = true;

                try
                {
                    await _clothesStore.Delete(_clothesListingItemViewModel.Clothes);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Bekleidung ist fehlgeschlagen!", "Bekleidung löschen");

                    _clothesListingItemViewModel.HasError = true;
                }

                _clothesListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
