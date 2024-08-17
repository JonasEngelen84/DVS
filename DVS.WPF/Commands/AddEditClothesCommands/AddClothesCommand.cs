using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Windows;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class AddClothesCommand(AddClothesViewModel addClothesViewModel,
                                   ClothesStore clothesStore,
                                   ModalNavigationStore modalNavigationStore)
                                   : AsyncCommandBase
    {
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditClothesFormViewModel addEditClothesFormViewModel = _addClothesViewModel.AddEditClothesFormViewModel;
            addEditClothesFormViewModel.HasError = false;
            addEditClothesFormViewModel.IsSubmitting = true;

            Clothes clothes = new(Guid.NewGuid(),
                                  addEditClothesFormViewModel.ID,
                                  addEditClothesFormViewModel.Name,
                                  addEditClothesFormViewModel.Category,
                                  addEditClothesFormViewModel.Season,
                                  addEditClothesFormViewModel.Comment);

            // Alle ausgewählten Größen in eine ZwischenListe speichern.
            // Diese wird der GrößenListe (Size) des ClothesModel hinzugefügt.
            var selectedSizes = addEditClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                ? addEditClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                : addEditClothesFormViewModel.AddEditListingViewModel.AvailableSizesEU.Where(size => size.IsSelected);

            // ClothesSizes den Listen von Clothes und SizeModel hinzufügen
            foreach (SizeModel size in selectedSizes)
            {
                ClothesSize clothesSize = new(Guid.NewGuid(), clothes, size, size.Quantity, null);

                clothes.Sizes.Add(clothesSize);
                size.ClothesSizes.Add(clothesSize);
            }

            // Erstellte Clothes-Instanz den Listen von category und saison hinzufügen
            clothes.Category?.Clothes.Add(clothes);
            clothes.Season?.Clothes.Add(clothes);

            try
            {
                await _clothesStore.Add(clothes);
            }
            catch (Exception)
            {
                string messageBoxText = "Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                string caption = "Bekleidung erstellen";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

                addEditClothesFormViewModel.HasError = true;
            }
            finally
            {
                addEditClothesFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
