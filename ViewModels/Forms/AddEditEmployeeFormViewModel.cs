using DVS.Models;
using DVS.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditEmployeeFormViewModel : ViewModelBase
    {
        private string _iD;
        public string ID
        {
            get => _iD;
            set
            {
                _iD = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private string _lastname;
        public string Lastname
        {
            get => _lastname;
            set
            {
                _lastname = value;
                OnPropertyChanged(nameof(Lastname));
            }
        }

        private string _firstname;
        public string Firstname
        {
            get => _firstname;
            set
            {
                _firstname = value;
                OnPropertyChanged(nameof(Firstname));
            }
        }

        private string? _comment;
        public string? Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        private bool _isSubmitting;
        public bool IsSubmitting
        {
            get
            {
                return _isSubmitting;
            }
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        //TODO: CanSubmit
        //public bool CanSubmit => !string.IsNullOrEmpty(Username);

        private readonly ObservableCollection<DetailedClothesListingItemModel> _newEmployeeClothes = [];
        public IEnumerable<DetailedClothesListingItemModel> NewEmployeeClothes => _newEmployeeClothes;

        public DVSClothesListingViewModel DVSClothesListingViewModel { get; }

        public ICommand AddEmployeeCommand { get; }
        public ICommand EditEmployeeCommand { get; }
        public ICommand ClearEmployeeClothesListCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }

        public AddEditEmployeeFormViewModel(ClothesStore clothesStore,
                                        ICommand addEmployeeCommand,
                                        ICommand editEmployeeCommand,
                                        ICommand clearEmployeeClothesListCommand,
                                        ICommand deleteEmployeeCommand)
        {
            DVSClothesListingViewModel = new(clothesStore);

            _newEmployeeClothes = [new DetailedClothesListingItemModel("951",
                                                                       "Test",
                                                                       "Schuhe",
                                                                       "Winter",
                                                                       "46",
                                                                       1,
                                                                       "Testweise")];

            AddEmployeeCommand = addEmployeeCommand;
            EditEmployeeCommand = editEmployeeCommand;
            ClearEmployeeClothesListCommand = clearEmployeeClothesListCommand;
            DeleteEmployeeCommand = deleteEmployeeCommand;
        }


        public void AddClothesToEmployee(DetailedClothesListingItemModel clothes)
        {
            if (clothes != null)
            {
                if (clothes != null && !_newEmployeeClothes.Contains(clothes))
                {
                    _newEmployeeClothes.Add(clothes);
                    OnPropertyChanged(nameof(NewEmployeeClothes));
                }

                DVSClothesListingViewModel.RemoveClothes(clothes);
            }
        }

        public void RemoveClothesFromEmployee(DetailedClothesListingItemModel clothes)
        {
            if (clothes != null)
            {
                if (clothes != null && _newEmployeeClothes.Contains(clothes))
                {
                    _newEmployeeClothes.Remove(clothes);
                    OnPropertyChanged(nameof(NewEmployeeClothes));
                }

                DVSClothesListingViewModel.AddClothes(clothes);
            }
        }
    }
}
