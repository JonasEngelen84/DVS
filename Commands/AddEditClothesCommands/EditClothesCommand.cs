using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.AddEditClothesCommands
{
    public class EditClothesCommand(EditClothesViewModel addEditClothesViewModel,
                                    ClothesStore clothesStore,
                                    ModalNavigationStore modalNavigationStore,
                                    Guid ID)
                                    : AsyncCommandBase
    {
        private readonly EditClothesViewModel _editClothesViewModel = addEditClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly Guid _guidID = ID;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditClothesFormViewModel addEditClothesFormViewModel = _editClothesViewModel.AddEditClothesFormViewModel;

            addEditClothesFormViewModel.ErrorMessage = null;
            addEditClothesFormViewModel.IsSubmitting = true;

            ClothesModel clothes = new(_guidID,
                                       addEditClothesFormViewModel.ID,
                                       addEditClothesFormViewModel.Name,
                                       addEditClothesFormViewModel.Category,
                                       addEditClothesFormViewModel.Season,
                                       addEditClothesFormViewModel.Comment) ;

            // Alle ausgewählten Größen in eine ZwischenListe speichern.
            // Diese wird der GrößenListe (Size) des ClothesModel hinzugefügt.
            var selectedSizes = addEditClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                ? addEditClothesFormViewModel.AddEditListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                : addEditClothesFormViewModel.AddEditListingViewModel.AvailableSizesEU.Where(size => size.IsSelected);

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
