﻿using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.ListViewItems;
using System.Windows;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class ClearSizesCommand(ClothesListingItemViewModel clothesListingItemViewModel,
        ClothesStore clothesStore) : AsyncCommandBase
    {
        private readonly ClothesListingItemViewModel _clothesListingItemViewModel = clothesListingItemViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = $"Alle Größen der Bekleidung  \"{_clothesListingItemViewModel.Name}\"  werden gelöscht!" +
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

                Clothes clothes = _clothesListingItemViewModel.Clothes;
               
                foreach (ClothesSize size in clothes.Sizes)
                {
                    size.Size.ClothesSizes.Remove(size);
                }

                clothes.Sizes.Clear();

                try
                {
                    await _clothesStore.Update(clothes);
                }
                catch (Exception)
                {
                    _clothesListingItemViewModel.ErrorMessage = "Löschen der Größen ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    _clothesListingItemViewModel.IsDeleting = false;
                }
            }
        }
    }
}
