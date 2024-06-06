using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class DVSEmployeesClothesListViewViewModel : ViewModelBase
    {
        private readonly SelectedClothesStore _selectedClothesStore;
        private readonly SelectedEmployeeClothesStore _selectedEmployeeClothesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly DVSClothesListViewViewModel _clothesListViewViewModel;

        private readonly List<EmployeeModel> _employeeList;

        private readonly ObservableCollection<EmployeeClothesListViewItemViewModel> _employeeClothesListViewItemCollection;
        public IEnumerable<EmployeeClothesListViewItemViewModel> EmployeeClothesListViewItemCollection => _employeeClothesListViewItemCollection;

        public DVSEmployeesClothesListViewViewModel(SelectedClothesStore selectedClothesStore,
                                                 SelectedEmployeeClothesStore selectedEmployeeClothesStore,
                                                 ModalNavigationStore modalNavigationStore)
        {
            _selectedClothesStore = selectedClothesStore;
            _selectedEmployeeClothesStore = selectedEmployeeClothesStore;
            _modalNavigationStore = modalNavigationStore;

            _employeeList = [];
            _employeeClothesListViewItemCollection = [];
        }

        //public EmployeeClothesListViewItemViewModel SelectedEmployeeClothesListViewItemViewModel
        //{
        //    get
        //    {
        //        return _employeeClothesCollection
        //            .FirstOrDefault(y => y.Employee?.Id == _selectedEmployeeClothesStore.SelectedEmployeeClothes?.Id);
        //    }
        //    set
        //    {
        //        _selectedEmployeeClothesStore.SelectedEmployeeClothes = value?.Employee;
        //    }
        //}

        //TODO: AddEmployee
        private void AddEmployeeClothesListViewItem(EmployeeModel employee)
        {
            foreach(ClothesModel clothes in employee.Clothes)
            {
                _employeeClothesListViewItemCollection.Add(new EmployeeClothesListViewItemViewModel(employee, clothes));
            }
        }
    }
}
