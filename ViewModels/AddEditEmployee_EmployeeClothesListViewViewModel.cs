using DVS.Models;
using System.Collections.ObjectModel;

namespace DVS.ViewModels
{
    public class AddEditEmployee_EmployeeClothesListViewViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ClothesModel> _employeeclothes = [];
        public IEnumerable<ClothesModel> Employeeclothes => _employeeclothes;

        public void ClearCollection()
        {
            _employeeclothes.Clear();
        }
    }
}
