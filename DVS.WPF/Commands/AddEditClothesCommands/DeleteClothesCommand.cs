using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;

namespace DVS.WPF.Commands.AddEditClothesCommands
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

                // Aktualisieren der ClothesSize-Liste von Size
                foreach (ClothesSize clothesSize in _clothesListingItemViewModel.Clothes.Sizes)
                {
                    clothesSize.Size.ClothesSizes.Remove(clothesSize);
                }

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
