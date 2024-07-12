using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.AddEditClothesCommands
{
    public class EditClothesCommand(AddEditClothesViewModel addEditClothesViewModel,
                                    ClothesStore clothesStore,
                                    ModalNavigationStore modalNavigationStore)
                                    : AsyncCommandBase
    {
        private readonly AddEditClothesViewModel _addEditClothesViewModel = addEditClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditClothesFormViewModel addEditClothesFormViewModel = _addEditClothesViewModel.AddEditClothesFormViewModel;

            addEditClothesFormViewModel.ErrorMessage = null;
            addEditClothesFormViewModel.IsSubmitting = true;

            ClothesModel clothes = new(Guid.NewGuid(),
                                       addEditClothesFormViewModel.ID,
                                       addEditClothesFormViewModel.Name,
                                       addEditClothesFormViewModel.Category,
                                       addEditClothesFormViewModel.Season,
                                       addEditClothesFormViewModel.Comment) ;

            // Alle ausgewählten Größen in eine ZwischenListe speichern.
            // Diese wird der GrößenListe (Size) des ClothesModel hinzugefügt.
            var selectedSizes = addEditClothesFormViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                ? addEditClothesFormViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                : addEditClothesFormViewModel.AvailableSizesEU.Where(size => size.IsSelected);

            foreach (ClothesSizeModel sizeModel in selectedSizes)
            {
                clothes.Sizes.Add(sizeModel);
            }

            try
            {
                await _clothesStore.Edit(clothes);
            }
            catch (Exception)
            {
                addEditClothesFormViewModel.ErrorMessage = "Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
            }
            finally
            {
                addEditClothesFormViewModel.IsSubmitting = false;
                _modalNavigationStore.Close();
            }
        }
    }
}
