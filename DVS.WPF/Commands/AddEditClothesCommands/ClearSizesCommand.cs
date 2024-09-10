using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;
using System.Windows;

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
            if (ConfirmClearSizes())
            {
                await ProcessClearSizesAsync();
            }
        }

        private bool ConfirmClearSizes()
        {
            string messageBoxText = $"Alle Größen der Bekleidung  \"{_clothesListingItemViewModel.Name}\"  werden gelöscht!" +
                $"\nDie Kleidungsstücke, dieser Bekleidung, bleiben den Mitarbeitern erhalten." +
                $"\n\nLöschen fortsetzen?";
            string caption = "Alle Bekleidungsgrößen löschen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);
            return dialog == MessageBoxResult.Yes;
        }

        private async Task ProcessClearSizesAsync()
        {
            _clothesListingItemViewModel.IsDeleting = true;
            _clothesListingItemViewModel.HasError = false;

            Clothes updatedClothes = CreateUpdatedClothesInstance();

            await DeleteClothesSizesAsync();

            await UpdateClothesAsync(updatedClothes);
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

        private async Task  DeleteClothesSizesAsync()
        {
            foreach (ClothesSize cs in _clothesListingItemViewModel.Clothes.Sizes)
            {
                cs.Size.ClothesSizes.Remove(cs);

                try
                {
                    await _clothesSizeStore.Delete(cs.GuidID);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox($"Update der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Alle Bekleidungsgrößen löschen");

                    _clothesListingItemViewModel.HasError = false;
                }
            }
        }

        private async Task UpdateCategoryAndSeasonAsync(Clothes updatedClothes)
        {
            updatedClothes.Category?.Clothes.Remove(_clothesListingItemViewModel.Clothes);
            updatedClothes.Category?.Clothes.Add(updatedClothes);
            updatedClothes.Season?.Clothes.Remove(_clothesListingItemViewModel.Clothes);
            updatedClothes.Season?.Clothes.Add(updatedClothes);

            try
            {
                await _categoryStore.Update(updatedClothes.Category, null);
                await _seasonStore.Update(updatedClothes.Season, null);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Löschen aller Bekleidungsgrößen ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Alle Bekleidungsgrößen löschen");

                _clothesListingItemViewModel.HasError = true;
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
                ShowErrorMessageBox($"Löschen aller Bekleidungsgrößen ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Alle Bekleidungsgrößen löschen");

                _clothesListingItemViewModel.HasError = true;
            }
            finally
            {
                _clothesListingItemViewModel.IsDeleting = false;
            }
        }

        private void ShowErrorMessageBox(string message, string title)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(message, title, button, icon);
        }
    }
}
