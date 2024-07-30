using DVS.Domain.Models;
using DVS.Stores;
using DVS.ViewModels;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;
using System.Windows;

namespace DVS.Commands.AddEditClothesCommands
{
    public class EditClothesCommand(EditClothesViewModel editClothesViewModel, ClothesStore clothesStore,
        ModalNavigationStore modalNavigationStore, Guid guidID) : AsyncCommandBase
    {
        private readonly EditClothesViewModel _editClothesViewModel = editClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ModalNavigationStore _modalNavigationStore = modalNavigationStore;
        private readonly Guid _guidID = guidID;

        public override async Task ExecuteAsync(object parameter)
        {
            string messageBoxText = "Bekleidung bearbeiten?";
            string caption = "Bekleidung bearbeiten";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult dialog = MessageBox.Show(messageBoxText, caption, button, icon);

            if (dialog == MessageBoxResult.Yes)
            {
                AddEditClothesFormViewModel addEditClothesFormViewModel = _editClothesViewModel.AddEditClothesFormViewModel;

                addEditClothesFormViewModel.ErrorMessage = null;
                addEditClothesFormViewModel.IsSubmitting = true;

                ClothesModel clothesToEdit = new(_guidID,
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

                //TODO: Kommentare von DetailedItems werden entfernt bei einem update
                foreach (ClothesSizeModel sizeModel in selectedSizes)
                {
                    clothesToEdit.Sizes.Add(sizeModel);
                }

                try
                {
                    await _clothesStore.Update(clothesToEdit);
                }
                catch (Exception)
                {
                    addEditClothesFormViewModel.ErrorMessage =
                        "Bearbeiten der Bekleidung ist fehlgeschlagen!\nBitte versuchen Sie es erneut.";
                }
                finally
                {
                    addEditClothesFormViewModel.IsSubmitting = false;
                    _modalNavigationStore.Close();
                }
            }
        }
    }
}
