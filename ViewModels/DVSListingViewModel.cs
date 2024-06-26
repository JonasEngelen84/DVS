using DVS.Commands.DragNDropCommands;
using DVS.Models;
using DVS.Stores;
using DVS.ViewModels.ListViewItems;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels
{
    public class DVSListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ClothesListingItemViewModel> _clothesListingItemCollection;
        public IEnumerable<ClothesListingItemViewModel> ClothesListingItemCollection => _clothesListingItemCollection;

        private readonly ObservableCollection<DetailedClothesListingItemModel> _detailedClothesListingItemCollection;
        public IEnumerable<DetailedClothesListingItemModel> DetailedClothesListingItemCollection => _detailedClothesListingItemCollection;

        private readonly ObservableCollection<EmployeeListingItemViewModel> _employeeListingItemCollection;
        public IEnumerable<EmployeeListingItemViewModel> EmployeeListingItemCollection => _employeeListingItemCollection;

        private readonly ObservableCollection<DetailedEmployeeListingItemModel> _detailedEmployeeListingItemCollection;
        public IEnumerable<DetailedEmployeeListingItemModel> DetailedEmployeeListingItemCollection => _detailedEmployeeListingItemCollection;
        
        private readonly ObservableCollection<DetailedClothesListingItemModel> _newEmployeeListingItemCollection;
        public IEnumerable<DetailedClothesListingItemModel> NewEmployeeListingItemCollection => _newEmployeeListingItemCollection;

        private DetailedClothesListingItemModel _incomingClothesListingItemModel;
        public DetailedClothesListingItemModel IncomingClothesListingItemModel
        {
            get
            {
                return _incomingClothesListingItemModel;
            }
            set
            {
                _incomingClothesListingItemModel = value;
                OnPropertyChanged(nameof(IncomingClothesListingItemModel));
            }
        }

        private DetailedClothesListingItemModel _removedClothesListingItemModel;
        public DetailedClothesListingItemModel RemovedClothesListingItemModel
        {
            get
            {
                return _removedClothesListingItemModel;
            }
            set
            {
                _removedClothesListingItemModel = value;
                OnPropertyChanged(nameof(RemovedClothesListingItemModel));
            }
        }

        private DetailedClothesListingItemModel _targetClothesListingItemModel;
        public DetailedClothesListingItemModel TargetClothesListingItemModel
        {
            get
            {
                return _targetClothesListingItemModel;
            }
            set
            {
                _targetClothesListingItemModel = value;
                OnPropertyChanged(nameof(TargetClothesListingItemModel));
            }
        }

        //private DetailedClothesListingItemModel _insertedClothesListingItemModel;
        //public DetailedClothesListingItemModel InsertedClothesListingItemModel
        //{
        //    get
        //    {
        //        return _insertedClothesListingItemModel;
        //    }
        //    set
        //    {
        //        _insertedClothesListingItemModel = value;
        //        OnPropertyChanged(nameof(InsertedClothesListingItemModel));
        //    }
        //}

        private readonly ClothesStore _clothesStore;
        private readonly EmployeeStore _employeeStore;

        public ICommand ClothesItemReceivedCommand { get; }
        public ICommand ClothesItemRemovedCommand { get; }
        //public ICommand ClothesItemInsertedCommand { get; }


        public DVSListingViewModel(ClothesStore clothesStore, EmployeeStore employeeStore)
        {
            _clothesListingItemCollection = [];
            _detailedClothesListingItemCollection = [];
            _employeeListingItemCollection = [];
            _detailedEmployeeListingItemCollection = [];
            _newEmployeeListingItemCollection = [];

            _clothesStore = clothesStore;
            _employeeStore = employeeStore;

            ClothesItemReceivedCommand = new ClothesItemReceivedCommand(this);
            ClothesItemRemovedCommand = new ClothesItemRemovedCommand(this);
            //ClothesItemInsertedCommand = new ClothesItemInsertedCommand(this);

            _clothesStore.ClothesLoaded += ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded += ClothesStore_ClothesAdded;

            _employeeStore.EmployeeAdded += EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeesLoaded += EmployeeStore_EmployeesLoaded;
        }

        public void AddClothesItemToNewEmployeeListingItemCollection(DetailedClothesListingItemModel item)
        {
            if (item == null || _newEmployeeListingItemCollection.Contains(item))
            {
                return;
            }

            var existingItem = _newEmployeeListingItemCollection
                .FirstOrDefault(modelItem => modelItem.ID == item.ID && modelItem.Size == item.Size);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                DetailedClothesListingItemModel newItem = new(
                item.ID, item.Name, item.Categorie, item.Season, item.Size, 1, null);

                _newEmployeeListingItemCollection.Add(newItem);
            }
        }
        
        public void AddClothesItemToDetailedClothesListingItemCollection(DetailedClothesListingItemModel item)
        {
            if (item == null || _detailedClothesListingItemCollection.Contains(item))
            {
                return;
            }

            var existingItem = _detailedClothesListingItemCollection
                .FirstOrDefault(modelItem => modelItem.ID == item.ID && modelItem.Size == item.Size);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                DetailedClothesListingItemModel newItem = new(
                item.ID, item.Name, item.Categorie, item.Season, item.Size, 1, null);

                _detailedClothesListingItemCollection.Add(newItem);
            }
        }

        public void RemoveClothesItemFromNewEmployeeListingItemCollection(DetailedClothesListingItemModel item)
        {
            if (item == null || !_newEmployeeListingItemCollection.Contains(item))
            {
                return;
            }

            if (item.Quantity > 1)
                item.Quantity--;
            else
                _newEmployeeListingItemCollection.Remove(item);
        }

        public void RemoveClothesItemFromDetailedClothesListingItemCollection(DetailedClothesListingItemModel item)
        {
            if (item == null || !_detailedClothesListingItemCollection.Contains(item))
            {
                // Das Element ist nicht in der Liste, nichts tun
                return;
            }

            if (item.Quantity > 1)
                item.Quantity--;
            else
                _detailedClothesListingItemCollection.Remove(item);
        }

        //public void InsertClothesItem(DetailedClothesListingItemModel insertedClothesItem,
        //                              DetailedClothesListingItemModel targetClothesItem)
        //{
        //    if (insertedClothesItem == targetClothesItem)
        //    {
        //        return;
        //    }

        //    int oldIndex = _detailedClothesListingItemCollection.IndexOf(insertedClothesItem);
        //    int nextIndex = _detailedClothesListingItemCollection.IndexOf(targetClothesItem);

        //    if (oldIndex != -1 && nextIndex != -1)
        //    {
        //        _detailedClothesListingItemCollection.Move(oldIndex, nextIndex);
        //    }
        //}

        public void Load()
        {
            ClothesStore_ClothesLoaded();
            EmployeeStore_EmployeesLoaded();
        }

        protected override void Dispose()
        {
            _clothesStore.ClothesLoaded -= ClothesStore_ClothesLoaded;
            _clothesStore.ClothesAdded -= ClothesStore_ClothesAdded;

            _employeeStore.EmployeeAdded -= EmployeeStore_EmployeeAdded;
            _employeeStore.EmployeesLoaded -= EmployeeStore_EmployeesLoaded;

            base.Dispose();
        }

        public void ClothesStore_ClothesLoaded()
        {
            _clothesListingItemCollection.Clear();
            _detailedClothesListingItemCollection.Clear();

            foreach (ClothesModel clothes in _clothesStore.Clothes)
            {
                ClothesStore_ClothesAdded(clothes);
            }
        }

        private void ClothesStore_ClothesAdded(ClothesModel clothes)
        {
            ClothesListingItemViewModel item = new(clothes);
            _clothesListingItemCollection.Add(item);

            foreach (ClothesSizeModel size in clothes.Sizes)
            {
                _detailedClothesListingItemCollection.Add(new DetailedClothesListingItemModel(
                    clothes.ID, clothes.Name, clothes.Categorie,
                    clothes.Season, size.Size, size.Quantity, clothes.Comment));
            }
        }

        private void ClotheStore_ClothesEdit(ClothesModel clothes)
        {

        }

        public void EmployeeStore_EmployeesLoaded()
        {
            _employeeListingItemCollection.Clear();
            _detailedEmployeeListingItemCollection.Clear();

            foreach (EmployeeModel employee in _employeeStore.Employees)
            {
                EmployeeStore_EmployeeAdded(employee);
            }
        }

        public void EmployeeStore_EmployeeAdded(EmployeeModel employee)
        {
            EmployeeListingItemViewModel item = new(employee);
            _employeeListingItemCollection.Add(item);

            //TODO: Employee_Added verbessern
            if (employee.Clothes.Count == 0)
            {
                string employeeID = employee.ID;
                string employeeLastname = employee.Lastname;
                string employeeFirstname = employee.Firstname;
                string? clothesID = null;
                string? clothesName = null;
                string? clothesSize = null;
                int? clothesQuantity = null;
                string? clothesComment = null;

                _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemModel(
                    employeeID, employeeLastname, employeeFirstname, clothesID,
                    clothesName, clothesSize, clothesQuantity, clothesComment));
            }
            else
            {
                foreach (DetailedClothesListingItemModel clothes in employee.Clothes)
                {
                    string employeeID = employee.ID;
                    string employeeLastname = employee.Lastname;
                    string employeeFirstname = employee.Firstname;
                    string clothesID = clothes.ID;
                    string clothesName = clothes.Name;
                    string clothesSize = clothes.Size;
                    int clothesQuantity = clothes.Quantity;
                    string? clothesComment = clothes.Comment;

                    _detailedEmployeeListingItemCollection.Add(new DetailedEmployeeListingItemModel(
                        employeeID, employeeLastname, employeeFirstname, clothesID,
                        clothesName, clothesSize, clothesQuantity, clothesComment));
                }

            }
        }

        private void EmployeeStore_EmployeeEdit(ClothesModel clothes)
        {

        }
    }
}
