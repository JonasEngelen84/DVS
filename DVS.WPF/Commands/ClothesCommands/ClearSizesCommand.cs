using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListingItems;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class ClearSizesCommand(ClothesListingItemViewModel clothesListingItemViewModel, ClothesSizeStore clothesSizeStore) : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            if (Confirm($"Alle Größen der Bekleidung  \"{clothesListingItemViewModel.Name}\"  werden gelöscht!" +
                $"\nDie Kleidungsstücke, dieser Bekleidung, bleiben den Mitarbeitern erhalten." +
                $"\n\nLöschen fortsetzen?", "Alle Bekleidungsgrößen löschen"))
            {
                clothesListingItemViewModel.IsDeleting = true;
                clothesListingItemViewModel.HasError = false;

                Clothes newClothes = new(clothesListingItemViewModel.Id,
                                         clothesListingItemViewModel.Name,
                                         clothesListingItemViewModel.Category,
                                         clothesListingItemViewModel.Season,
                                         clothesListingItemViewModel.Comment);

                // IEnumerable kann nicht in Foreach durchlaufen und bearbeitet werden!
                List<ClothesSize> ClothesSizesToDelete = new(clothesListingItemViewModel.Clothes.Sizes);
                foreach (ClothesSize clothesSize in ClothesSizesToDelete)
                {
                    try
                    {
                        await clothesSizeStore.Delete(clothesSize);
                    }
                    catch (Exception)
                    {
                        ShowErrorMessageBox("Löschen der Bekleidungsgrößen ist fehlgeschlagen!", "ClearSizesCommand DeleteClothesSizesAsync");

                        clothesListingItemViewModel.HasError = false;
                    }
                }

                // Aktualisieren der Clothes-Listen von Category und Season
                Clothes existingClothes = newClothes.Category.Clothes.FirstOrDefault(c => c.Id == clothesListingItemViewModel.Clothes.Id);
                if (existingClothes != null)
                {
                    newClothes.Category.Clothes.Remove(existingClothes);
                    newClothes.Category.Clothes.Add(newClothes);
                }

                existingClothes = newClothes.Season.Clothes.FirstOrDefault(c => c.Id == clothesListingItemViewModel.Clothes.Id);
                if (existingClothes != null)
                {
                    newClothes.Season.Clothes.Remove(existingClothes);
                    newClothes.Season.Clothes.Add(newClothes);
                }

                clothesListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
