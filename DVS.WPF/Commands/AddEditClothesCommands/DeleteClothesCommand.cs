using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;
using System.Windows;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class DeleteClothesCommand(ClothesListingItemViewModel clothesListingItemViewModel,
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
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;

        public override async Task ExecuteAsync(object parameter)
        {
            if (ConfirmDeleteClothes())
            {
                await ProcessDeleteClothesAsync();
            }
        }

        private bool ConfirmDeleteClothes()
        {
            string messageBoxText = $"Die Bekleidung  \"{_clothesListingItemViewModel.Name}\"  wird gelöscht!" +
                $"\nDie Kleidungsstücke, dieser Bekleidung, bleiben den Mitarbeitern erhalten." +
                $"\n\nLöschen fortsetzen?";
            string caption = "Bekleidung löschen";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);
            return dialog == MessageBoxResult.Yes;
        }

        private async Task ProcessDeleteClothesAsync()
        {
            _clothesListingItemViewModel.HasError = false;
            _clothesListingItemViewModel.IsDeleting = true;

            DeleteClothesSizes();

            await UpdateCategoryAndSeasonAsync();

            await DeleteClothesAsync();
        }

        private void DeleteClothesSizes()
        {
            foreach (ClothesSize size in _clothesListingItemViewModel.Clothes.Sizes)
            {
                size.Size.ClothesSizes.Remove(size);
            }
        }

        private async Task UpdateCategoryAndSeasonAsync()
        {
            _clothesListingItemViewModel.Clothes.Category.Clothes.Remove(_clothesListingItemViewModel.Clothes);
            _clothesListingItemViewModel.Clothes.Season.Clothes.Remove(_clothesListingItemViewModel.Clothes);

            try
            {
                await _categoryStore.Update(_clothesListingItemViewModel.Clothes.Category, null);
                await _seasonStore.Update(_clothesListingItemViewModel.Clothes.Season, null);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Löschen der Bekleidung ist fehlgeschlagen!", "Bekleidung löschen");

                _clothesListingItemViewModel.HasError = true;
            }
        }

        private async Task DeleteClothesAsync()
        {
            try
            {
                await _clothesStore.Delete(_clothesListingItemViewModel.Clothes);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Löschen der Bekleidung ist fehlgeschlagen!", "Bekleidung löschen");

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
