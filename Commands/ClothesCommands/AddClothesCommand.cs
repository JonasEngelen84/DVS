using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.Forms;
using DVS.ViewModels.Views;

namespace DVS.Commands.ClothesCommands
{
    public class AddClothesCommand(AddClothesViewModel addClothesViewModel, ClothesStore clothesStore) : AsyncCommandBase
    {
        private readonly AddClothesViewModel _addClothesViewModel = addClothesViewModel;
        private readonly ClothesStore _clothesStore = clothesStore;

        public override async Task ExecuteAsync(object parameter)
        {
            AddClothesFormViewModel addClothesFormViewModel = _addClothesViewModel.AddClothesFormViewModel;

            addClothesFormViewModel.ErrorMessage = null;
            addClothesFormViewModel.IsSubmitting = true;

            ClothesModel clothes = new(
                addClothesFormViewModel.Id,
                addClothesFormViewModel.Name,
                addClothesFormViewModel.SelectedCategory,
                addClothesFormViewModel.Size,
                addClothesFormViewModel.SelectedSeason,
                addClothesFormViewModel.Quantity,
                addClothesFormViewModel.Comment);

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
            }
        }
    }
}
