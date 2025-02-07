using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;

namespace DVS.WPF.Commands.ClothesCommands
{
    public class ClearSizesCommand(ClothesListingItemViewModel clothesListingItemViewModel, ClothesSizeStore clothesSizeStore) : AsyncCommandBase
    {
        private readonly ClothesListingItemViewModel _clothesListingItemViewModel = clothesListingItemViewModel;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;

        public override async Task ExecuteAsync(object parameter)
        {
            if (Confirm($"Alle Größen der Bekleidung  \"{_clothesListingItemViewModel.Name}\"  werden gelöscht!" +
                $"\nDie Kleidungsstücke, dieser Bekleidung, bleiben den Mitarbeitern erhalten." +
                $"\n\nLöschen fortsetzen?", "Alle Bekleidungsgrößen löschen"))
            {
                _clothesListingItemViewModel.IsDeleting = true;
                _clothesListingItemViewModel.HasError = false;

                Clothes newClothes = new(_clothesListingItemViewModel.Clothes.GuidId,
                                         _clothesListingItemViewModel.Id,
                                         _clothesListingItemViewModel.Name,
                                         _clothesListingItemViewModel.Category,
                                         _clothesListingItemViewModel.Season,
                                         _clothesListingItemViewModel.Comment);

                // IEnumerable kann nicht in Foreach durchlaufen und bearbeitet werden!
                List<ClothesSize> ClothesSizesToDelete = new(_clothesListingItemViewModel.Clothes.Sizes);
                foreach (ClothesSize clothesSize in ClothesSizesToDelete)
                {
                    try
                    {
                        await _clothesSizeStore.Delete(clothesSize);
                    }
                    catch (Exception)
                    {
                        ShowErrorMessageBox("Löschen der Bekleidungsgrößen ist fehlgeschlagen!", "ClearSizesCommand DeleteClothesSizesAsync");

                        _clothesListingItemViewModel.HasError = false;
                    }

                    // Aktualisieren der ClothesSize-Liste des SizeModel
                    ClothesSize existingClothesSize = clothesSize.Size.ClothesSizes.FirstOrDefault(cs => cs.Size.GuidId == clothesSize.Size.GuidId);
                    if (existingClothesSize != null)
                        clothesSize.Size.ClothesSizes.Remove(clothesSize);
                }

                // Aktualisieren der Clothes-Listen von Category und Season
                Clothes existingClothes = newClothes.Category.Clothes.FirstOrDefault(c => c.GuidId == _clothesListingItemViewModel.Clothes.GuidId);
                if (existingClothes != null)
                {
                    newClothes.Category.Clothes.Remove(existingClothes);
                    newClothes.Category.Clothes.Add(newClothes);
                }

                existingClothes = newClothes.Season.Clothes.FirstOrDefault(c => c.GuidId == _clothesListingItemViewModel.Clothes.GuidId);
                if (existingClothes != null)
                {
                    newClothes.Season.Clothes.Remove(existingClothes);
                    newClothes.Season.Clothes.Add(newClothes);
                }

                _clothesListingItemViewModel.IsDeleting = false;
            }
        }
    }
}
