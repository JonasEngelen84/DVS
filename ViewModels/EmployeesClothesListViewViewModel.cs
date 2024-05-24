using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class EmployeesClothesListViewViewModel : ViewModelBase
    {
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ClothesListViewViewModel _clothesListViewViewModel;

        private readonly List<EmployeeModel> _employeeList;

        private readonly ObservableCollection<EmployeeClothesListViewItemViewModel> _employeeClothesList;
        public IEnumerable<EmployeeClothesListViewItemViewModel> EmployeeClothesList => _employeeClothesList;

        public EmployeesClothesListViewViewModel(
            SelectedClothesStore selectedClothesStore,
            SelectedEmployeeClothesStore selectedEmployeeClothesStore,
            ModalNavigationStore modalNavigationStore,
            ClothesListViewViewModel clothesListViewViewModel)
        {
            _selectedClothesStore = selectedClothesStore;
            _selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            _modalNavigationStore = modalNavigationStore;
            _clothesListViewViewModel = clothesListViewViewModel;
            _employeeList = [];
            _employeeClothesList = [];
        }

        public EmployeeClothesListViewItemViewModel SelectedEmployeeClothesListViewItemViewModel
        {
            get
            {
                return _employeeClothesList
                    .FirstOrDefault(y => y.EmployeeModel?.Id == _selectedEmployeeClothesStore.SelectedEmployeeClothes?.Id);
            }
            set
            {
                _selectedEmployeeClothesStore.SelectedEmployeeClothes = value?.EmployeeModel;
            }
        }

        private void AddEmployeeClothesListViewItem()
        {
            
        }
    }
}
