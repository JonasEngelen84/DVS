using DVS.Domain.Models;
using DVS.WPF.Stores;
using DVS.WPF.ViewModels;
using DVS.WPF.ViewModels.Forms;
using DVS.WPF.ViewModels.Views;
using System.Collections.ObjectModel;

namespace DVS.WPF.Commands.AddEditCategoryCommands
{
    public class DeleteCategoryCommand(AddEditCategoryViewModel addEditCategoryViewModel,
                                       CategoryStore categoryStore,
                                       SeasonStore seasonStore,
                                       SizeStore sizeStore,
                                       ClothesStore clothesStore,
                                       ClothesSizeStore clothesSizeStore,
                                       EmployeeClothesSizesStore employeeClothesSizesStore,
                                       EmployeeStore employeeStore,
                                       DVSListingViewModel dVSListingViewModel)
                                       : AsyncCommandBase
    {
        private readonly AddEditCategoryViewModel _addEditCategoryViewModel = addEditCategoryViewModel;
        private readonly CategoryStore _categoryStore = categoryStore;
        private readonly SeasonStore _seasonStore = seasonStore;
        private readonly SizeStore _sizeStore = sizeStore;
        private readonly ClothesStore _clothesStore = clothesStore;
        private readonly ClothesSizeStore _clothesSizeStore = clothesSizeStore;
        private readonly EmployeeClothesSizesStore _employeeClothesSizesStore = employeeClothesSizesStore;
        private readonly EmployeeStore _employeeStore = employeeStore;
        private readonly DVSListingViewModel _dVSListingViewModel = dVSListingViewModel;

        public override async Task ExecuteAsync(object parameter)
        {
            AddEditCategoryFormViewModel addEditCategoryFormViewModel = _addEditCategoryViewModel.AddEditCategoryFormViewModel;

            if (Confirm($"Die Kategorie \"{addEditCategoryFormViewModel.SelectedCategory.Name}\"" +
                $"wird gelöscht, und ihre Schnittstellen werden auf \"Kategorielos\" gesetzt.\n\nLöschen fortsetzen?", "Kategorie löschen"))
            {
                addEditCategoryFormViewModel.HasError = false;
                addEditCategoryFormViewModel.IsDeleting = true;

                var clothesToEdit = GetClothesToEdit(addEditCategoryFormViewModel, _dVSListingViewModel);
                var editedClothes = EditClothes(clothesToEdit);
                var clothesSizesToEdit = GetClothesSizesToEdit(addEditCategoryFormViewModel);
                var editedClothesSizes = EditClothesSizes(clothesSizesToEdit, editedClothes);
                var employeeClothesSizesToEdit = GetEmployeeClothesSizeToEdit(addEditCategoryFormViewModel, _dVSListingViewModel);
                var editedEmployeeClothesSize = EditEmployeeClothesSizes(employeeClothesSizesToEdit, editedClothesSizes);

                await UpdateClothesAsync(editedClothes, addEditCategoryFormViewModel);
                await DeleteCategory(addEditCategoryFormViewModel);
            }
        }

        private List<Clothes> GetClothesToEdit(AddEditCategoryFormViewModel addEditCategoryFormViewModel, DVSListingViewModel dVSListingViewModel)
        {
            return dVSListingViewModel.ClothesListingItemCollection
                .Where(clivm => clivm.Category == addEditCategoryFormViewModel.SelectedCategory)
                .Select(clivm => clivm.Clothes)
                .ToList();
        }

        private List<Clothes> EditClothes(List<Clothes> ClothesToEdit)
        {
            List<Clothes> editedClothes = [];

            foreach (Clothes clothes in ClothesToEdit)
            {
                Clothes newClothes = new(clothes.GuidId,
                                         clothes.Id,
                                         clothes.Name,
                                         _categoryStore.Categoryless,
                                         clothes.Season,
                                         clothes.Comment)
                {
                    Sizes = new ObservableCollection<ClothesSize>(clothes.Sizes)
                };

                editedClothes.Add(newClothes);
            }

            return editedClothes;
        }

        private List<ClothesSize> GetClothesSizesToEdit(AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            return dVSListingViewModel.EmployeeListingItemCollection
                .Where(elivm => elivm.Clothes.Any(ecs => ecs.ClothesSize.Clothes.Category == addEditCategoryFormViewModel.SelectedCategory))
                .SelectMany(elivm => elivm.Clothes)
                .Where(ecs => ecs.ClothesSize.Clothes.Category == addEditCategoryFormViewModel.SelectedCategory)
                .Select(ecs => ecs.ClothesSize)
                .ToList();
        }

        private List<ClothesSize> EditClothesSizes(List<ClothesSize> clothesSizesToEdit, List<Clothes> editedClothes)
        {
            List<ClothesSize> editedClothesSizes = [];

            foreach (ClothesSize cs in clothesSizesToEdit)
            {
                ClothesSize newClothesSize = new(cs.GuidId,
                                                 editedClothes.FirstOrDefault(c => c.GuidId == cs.Clothes.GuidId),
                                                 cs.Size,
                                                 cs.Quantity,
                                                 cs.Comment)
                {
                    EmployeeClothesSizes = new ObservableCollection<EmployeeClothesSize>(cs.EmployeeClothesSizes)
                };

                editedClothesSizes.Add(newClothesSize);
            }

            return editedClothesSizes;
        }

        private List<EmployeeClothesSize> GetEmployeeClothesSizeToEdit(AddEditCategoryFormViewModel addEditCategoryFormViewModel, DVSListingViewModel dVSListingViewModel)
        {
            return dVSListingViewModel.EmployeeListingItemCollection
                .SelectMany(elivm => elivm.Employee.Clothes)
                .Where(ecs => ecs.ClothesSize.Clothes.Category == addEditCategoryFormViewModel.SelectedCategory)
                .ToList();
        }

        private List<EmployeeClothesSize> EditEmployeeClothesSizes(List<EmployeeClothesSize> employeeClothesSizesToEdit, List<ClothesSize> editedClothesSizes)
        {
            List<EmployeeClothesSize> editedEmployeeClothesSizes = [];

            foreach (EmployeeClothesSize ecs in employeeClothesSizesToEdit)
            {
                EmployeeClothesSize newEmployeeClothesSize = new(ecs.GuidId,
                                                                 ecs.Employee,
                                                                 editedClothesSizes.FirstOrDefault(cs => cs.GuidId == ecs.ClothesSizeGuidId),
                                                                 ecs.Quantity,
                                                                 ecs.Comment);

                editedEmployeeClothesSizes.Add(newEmployeeClothesSize);
            }

            return editedEmployeeClothesSizes;
        }

        private async Task UpdateEmployeeClothesSizesAsync(List<ClothesSize> editedClothesSizes, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            foreach (ClothesSize cs in editedClothesSizes)
            {
                try
                {
                    await _clothesSizeStore.Update(cs);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Kategorie ist fehlgeschlagen!", "Kategorie löschen");
                    addEditCategoryFormViewModel.HasError = true;
                }
            }
        }
        
        private async Task UpdateEmployeeAsync(List<ClothesSize> editedClothesSizes, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            foreach (ClothesSize cs in editedClothesSizes)
            {
                try
                {
                    await _clothesSizeStore.Update(cs);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Kategorie ist fehlgeschlagen!", "Kategorie löschen");
                    addEditCategoryFormViewModel.HasError = true;
                }
            }
        }
        
        private async Task UpdateClothesSizesAsync(List<ClothesSize> editedClothesSizes, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            foreach (ClothesSize cs in editedClothesSizes)
            {
                try
                {
                    await _clothesSizeStore.Update(cs);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Kategorie ist fehlgeschlagen!", "Kategorie löschen");
                    addEditCategoryFormViewModel.HasError = true;
                }
            }
        }

        private async Task UpdateClothesAsync(List<Clothes> editedClothes, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            foreach (Clothes c in editedClothes)
            {
                try
                {
                    await _clothesStore.Update(c);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Kategorie ist fehlgeschlagen!", "Kategorie löschen");
                    addEditCategoryFormViewModel.HasError = true;
                }
            }
        }
        
        private async Task UpdateSizeAsync(List<ClothesSize> clothesSizesToEdit, List<ClothesSize> editedClothesSizes, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            foreach (ClothesSize cs in clothesSizesToEdit)
            {
                cs.Size.ClothesSizes.Remove(cs);
            }

            foreach (ClothesSize cs in editedClothesSizes)
            {
                cs.Size.ClothesSizes.Add(cs);

                try
                {
                    await _sizeStore.Update(cs.Size);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Kategorie ist fehlgeschlagen!", "Kategorie löschen");
                    addEditCategoryFormViewModel.HasError = true;
                }
            }
        }
        
        private async Task UpdateSeasonAsync(List<Clothes> clothesToEdit, List<Clothes> editedClothes, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            foreach (Clothes c in clothesToEdit)
            {
                c.Season.Clothes.Remove(c);
            }

            foreach (Clothes c in editedClothes)
            {
                c.Season.Clothes.Add(c);

                try
                {
                    await _seasonStore.Update(c.Season, null);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Kategorie ist fehlgeschlagen!", "Kategorie löschen");
                    addEditCategoryFormViewModel.HasError = true;
                }
            }
        }
        
        private async Task UpdateCategoryAsync(List<Clothes> clothesToEdit, List<Clothes> editedClothes, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            foreach (Clothes c in clothesToEdit)
            {
                _categoryStore.Categoryless.Clothes.Remove(c);
            }

            foreach (Clothes c in editedClothes)
            {
                _categoryStore.Categoryless.Clothes.Add(c);

                try
                {
                    await _categoryStore.Update(_categoryStore.Categoryless, null);
                }
                catch (Exception)
                {
                    ShowErrorMessageBox("Löschen der Kategorie ist fehlgeschlagen!", "Kategorie löschen");
                    addEditCategoryFormViewModel.HasError = true;
                }
            }
        }
        
        private async Task DeleteCategory(AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            try
            {
                await _categoryStore.Delete(addEditCategoryFormViewModel.SelectedCategory, addEditCategoryFormViewModel);
            }
            catch (Exception)
            {
                ShowErrorMessageBox("Löschen der Kategorie ist fehlgeschlagen!\nBitte versuchen Sie es erneut.", "Kategorie löschen");

                addEditCategoryFormViewModel.HasError = true;
            }
            finally
            {
                addEditCategoryFormViewModel.IsDeleting = false;
            }
        }
    }
}
