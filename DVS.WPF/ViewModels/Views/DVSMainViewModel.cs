using DVS.Domain.Services.Interfaces;
using DVS.WPF.Commands.ClothesCommands;
using DVS.WPF.Commands.EmployeeCommands;
using DVS.WPF.Stores;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Views
{
    public class DVSMainViewModel(
        DVSListingViewModel dVSListingViewModel,
        ModalNavigationStore modalNavigationStore,
        CategoryStore categoryStore,
        SeasonStore seasonStore,
        ClothesStore clothesStore,
        ClothesSizeStore clothesSizeStore,
        EmployeeClothesSizeStore employeeClothesSizesStore,
        EmployeeStore employeeStore,
        IDirtyEntitySaver dirtyEntitySaver)
        : ViewModelBase
    {
        public DVSListingViewModel DVSListingViewModel { get; } = dVSListingViewModel;
        
        private string _searchClothes;
        public string SearchClothes
        {
            get => _searchClothes;
            set
            {
                if (value != _searchClothes)
                {
                    _searchClothes = value;
                    OnPropertyChanged(nameof(SearchClothes));
                    DVSListingViewModel.ApplyClothesFilter(_searchClothes);
                }
            }
        }

        private string _searchEmployee;
        public string SearchEmployee
        {
            get => _searchEmployee;
            set
            {
                if (value != _searchEmployee)
                {
                    _searchEmployee = value;
                    OnPropertyChanged(nameof(SearchEmployee));
                    DVSListingViewModel.ApplyEmployeeFilter(_searchEmployee);
                }
            }
        }

        public ICommand OpenAddEmployee { get; } = new OpenAddEmployeeCommand(
            employeeStore,
            clothesStore,
            clothesSizeStore,
            employeeClothesSizesStore,
            modalNavigationStore,
            dVSListingViewModel);

        public ICommand OpenAddClothes { get; } = new OpenAddClothesCommand(
            modalNavigationStore,
            categoryStore,
            seasonStore,
            clothesStore,
            clothesSizeStore,
            employeeClothesSizesStore,
            employeeStore,
            dirtyEntitySaver);
    }
}
