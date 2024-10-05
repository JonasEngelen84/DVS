using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class ClearSizesCommand(ClothesListingItemViewModel clothesListingItemViewModel,
                                   SizeStore sizeStore,
                                   ClothesStore clothesStore,
                                   CategoryStore categoryStore,
                                   SeasonStore seasonStore,
                                   ClothesSizeStore clothesSizeStore,
                                   EmployeeClothesSizesStore employeeClothesSizesStore,
                                   EmployeeStore employeeStore)
                                   : AsyncCommandBase
    {
        private readonly ClothesListingItemViewModel _clothesListingItemViewModel = clothesListingItemViewModel;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly EmployeeStore _employeeStore = employeeStore;

        public override async Task ExecuteAsync(object parameter)
        {
            if (Confirm($"Alle Größen der Bekleidung  \"{_clothesListingItemViewModel.Name}\"  werden gelöscht!" +
                $"\nDie Kleidungsstücke, dieser Bekleidung, bleiben den Mitarbeitern erhalten." +
                $"\n\nLöschen fortsetzen?", "Alle Bekleidungsgrößen löschen"))
            {
                _clothesListingItemViewModel.IsDeleting = true;
                _clothesListingItemViewModel.HasError = false;

                Clothes updatedClothes = CreateUpdatedClothesInstance();
                await DeleteClothesSizesAsync();
                await UpdateClothesAsync(updatedClothes);
                await UpdateCategoryAsync(updatedClothes);
                await UpdateSeasonAsync(updatedClothes);

                _clothesListingItemViewModel.IsDeleting = false;
            }
        }

        private Clothes CreateUpdatedClothesInstance()
        {
             return new Clothes(_clothesListingItemViewModel.Clothes.GuidID,
                                _clothesListingItemViewModel.ID,
                                _clothesListingItemViewModel.Name,
                                _clothesListingItemViewModel.Category,
                                _clothesListingItemViewModel.Season,
                                _clothesListingItemViewModel.Comment)
            {
                Sizes = []
            };
        }

        private async Task DeleteClothesSizesAsync()
        {
            foreach (ClothesSize cs in _clothesListingItemViewModel.Clothes.Sizes)
            {
                cs.Size.ClothesSizes.Remove(cs);

                try
                {
                    await _clothesSizeStore.Delete(cs);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der ClothesSize aus Datenbank ist fehlgeschlagen!", "ClearSizesCommand DeleteClothesSizesAsync");

                    _clothesListingItemViewModel.HasError = false;
                }
            }
        }

        private async Task UpdateClothesAsync(Clothes updatedClothes)
        {
            try
            {
                await _clothesStore.Update(updatedClothes);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Updaten der Clothes in Datenbank ist fehlgeschlagen!", "ClearSizesCommand UpdateClothesAsync");

                _clothesListingItemViewModel.HasError = true;
            }
        }

        private async Task UpdateCategoryAsync(Clothes updatedClothes)
        {
            updatedClothes.Category?.Clothes.Remove(_clothesListingItemViewModel.Clothes);
            updatedClothes.Category?.Clothes.Add(updatedClothes);

            try
            {
                await _categoryStore.Update(updatedClothes.Category, null);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Updaten der Category in Datenbank ist fehlgeschlagen!", "ClearSizesCommand UpdateCategoryAsync");

                _clothesListingItemViewModel.HasError = true;
            }
        }
        
        private async Task UpdateSeasonAsync(Clothes updatedClothes)
        {
            updatedClothes.Season?.Clothes.Remove(_clothesListingItemViewModel.Clothes);
            updatedClothes.Season?.Clothes.Add(updatedClothes);

            try
            {
                await _seasonStore.Update(updatedClothes.Season, null);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Updaten der Season in Datenbank ist fehlgeschlagen!", "ClearSizesCommand UpdateSeasonAsync");

                _clothesListingItemViewModel.HasError = true;
            }
        }
    }
}
