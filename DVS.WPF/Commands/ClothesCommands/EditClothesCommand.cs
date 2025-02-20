﻿using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;

namespace DVS.WPF.Commands.AddEditClothesCommands
{
    public class EditClothesCommand(
        EditClothesViewModel editClothesViewModel,
        EmployeeStore employeeStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        ModalNavigationStore modalNavigationStore) 
        : AsyncCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            AddEditClothesFormViewModel editClothesFormViewModel = editClothesViewModel.AddEditClothesFormViewModel;

            if (Confirm($"Soll die Bekleidung  \"{editClothesFormViewModel.Name}\"  und Ihre Schnittstellen bearbeiten werden?", "Bekleidung bearbeiten"))
            {
                editClothesFormViewModel.HasError = false;
                editClothesFormViewModel.IsSubmitting = true;

                Clothes newClothes = CreateClothes(editClothesFormViewModel);
                List<Size> selectedSizes = GetSizes(editClothesFormViewModel);

                if (selectedSizes != null)
                {
                    await DeleteClothesSizesAsync(editClothesFormViewModel);
                    CreateClothesSizes(selectedSizes, newClothes);
                    AddClothesSizeToStore(newClothes);
                }

                await AddClothesToDB(newClothes, editClothesFormViewModel);

                editClothesFormViewModel.IsSubmitting = false;
                modalNavigationStore.Close();
            }
        }

        private static Clothes CreateClothes(AddEditClothesFormViewModel editClothesFormViewModel)
        {
            return new Clothes(
                editClothesFormViewModel.Id,
                editClothesFormViewModel.Name,
                editClothesFormViewModel.Category,
                editClothesFormViewModel.Season,
                editClothesFormViewModel.Comment)
            {
                Sizes = []
            };
        }

        private static List<Size> GetSizes(AddEditClothesFormViewModel editClothesFormViewModel)
        {
            return new List<Size>(editClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesUS.Any(size => size.IsSelected)
                    ? editClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesUS.Where(size => size.IsSelected)
                    : editClothesFormViewModel.AddEditClothesListingViewModel.AvailableSizesEU.Where(size => size.IsSelected))
                    .ToList();
        }
        
        private async Task DeleteClothesSizesAsync(AddEditClothesFormViewModel editClothesFormViewModel)
        {
            List<ClothesSize> ClothesSizesToDelete = new(editClothesFormViewModel.Clothes.Sizes);
            foreach (ClothesSize clothesSize in ClothesSizesToDelete)
            {
                try
                {
                    await clothesSizeStore.Delete(clothesSize);
                }
                catch
                {
                    ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!", "AddClothesCommand CreateAndAddNewClothesAsync");
                    editClothesFormViewModel.HasError = true;
                }
            }
        }

        private static void CreateClothesSizes(List<Size> selectedSizes, Clothes newClothes)
        {
            foreach (Size size in selectedSizes)
            {
                ClothesSize newClothesSize = new(Guid.NewGuid(), newClothes, size.Name, size.Quantity, "");
                newClothes.Sizes.Add(newClothesSize);
            }
        }

        private async Task AddClothesToDB(Clothes newClothes, AddEditClothesFormViewModel editClothesFormViewModel)
        {
            try
            {
                await clothesStore.Add(newClothes);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Erstellen der Bekleidung ist fehlgeschlagen!", "AddClothesCommand CreateAndAddNewClothesAsync");
                editClothesFormViewModel.HasError = true;
            }
        }

        private void AddClothesSizeToStore(Clothes newClothes)
        {
            foreach (ClothesSize cs in newClothes.Sizes)
            {
                clothesSizeStore.AddToStore(cs);
            }
        }
    }
}
