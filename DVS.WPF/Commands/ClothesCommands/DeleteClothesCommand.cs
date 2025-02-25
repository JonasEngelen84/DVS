using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class DeleteClothesCommand(ClothesListingItemViewModel clothesListingItemViewModel, ClothesStore clothesStore) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            if (Confirm($"Die Bekleidung  \"{clothesListingItemViewModel.Name}\"  wird gelöscht!" +
                $"\nDie Kleidungsstücke, dieser Bekleidung, bleiben den Mitarbeitern erhalten." +
                $"\n\nLöschen fortsetzen?", "Bekleidung löschen"))
            {
                clothesListingItemViewModel.HasError = false;
                clothesListingItemViewModel.IsDeleting = true;

                try
                {
                    await clothesStore.Delete(clothesListingItemViewModel.Clothes);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Bekleidung ist fehlgeschlagen!", "Bekleidung löschen");

                    clothesListingItemViewModel.HasError = true;
                }

                clothesListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
