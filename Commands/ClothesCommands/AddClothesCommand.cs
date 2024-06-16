using DVS.Models;
using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.ClothesCommands
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
            AddClothesFormViewModel addClothesFormViewModel = _addClothesViewModel.AddClothesFormViewModel;

            addClothesFormViewModel.ErrorMessage = null;
            addClothesFormViewModel.IsSubmitting = true;

            // Alle ausgewählten Größen in eine ZwischenListe speichern.
            // Diese wird der GrößenListe (Size) des ClothesModel hinzugefügt.
            var selectedSizes = addClothesFormViewModel.AvailableSizesUS.Any(size => size.IsSelected)
            ? addClothesFormViewModel.AvailableSizesUS.Where(size => size.IsSelected)
            : addClothesFormViewModel.AvailableSizesEU.Where(size => size.IsSelected);

            ClothesModel clothes = new(addClothesFormViewModel.ID,
                                       addClothesFormViewModel.Name,
                                       addClothesFormViewModel.Category,
                                       addClothesFormViewModel.Season,
                                       addClothesFormViewModel.Comment);

            foreach (SizeOption size in selectedSizes)
            {
                clothes.Sizes.Add(new ClothesSizeModel(size.Size, size.Quantity, size.Comment));
            }

            try
            {
                await _clothesStore.Add(clothes);
            }
            catch (Exception)
            {
                addClothesFormViewModel.ErrorMessage = "Erstellen der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addClothesFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
