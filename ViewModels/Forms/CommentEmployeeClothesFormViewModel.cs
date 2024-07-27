using DVS.Models;
using DVS.Stores;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class CommentEmployeeClothesFormViewModel : ViewModelBase
    {
        private readonly SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore;

        private DetailedEmployeeListingItemViewModel SelectedDetailedEmployeeItem => _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem;

        public bool HasSelectedDetailedEmployeeListingItem => SelectedDetailedEmployeeItem != null;
        public EmployeeModel Employee => SelectedDetailedEmployeeItem.Employee;
        public string EmployeeID => SelectedDetailedEmployeeItem.ID;
        public string EmployeeLastname => SelectedDetailedEmployeeItem.Lastname;
        public string EmployeeFirstname => SelectedDetailedEmployeeItem.Firstname;
        public string? ClothesID => SelectedDetailedEmployeeItem.ClothesID;
        public string? ClothesName => SelectedDetailedEmployeeItem.ClothesName;
        public string Size => SelectedDetailedEmployeeItem.Size;
        public int? Quantity => SelectedDetailedEmployeeItem.Quantity;

        private string? _comment;
        public string? Comment
        {
            get
            {
                return _comment;
            }
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

        public bool CanSubmit => !string.IsNullOrEmpty(Comment);

        public ICommand SubmitComment { get; }

        public CommentEmployeeClothesFormViewModel(ICommand submitComment,
            SelectedDetailedEmployeeClothesItemStore selectedDetailedEmployeeClothesItemStore)
        {
            _selectedDetailedEmployeeClothesItemStore = selectedDetailedEmployeeClothesItemStore;
            SubmitComment = submitComment;

            _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItemChanged += SelectedDetailedEmployeeClothesItemStore_SelectedDetailedEmployeeItemChanged;
        }

        private void SelectedDetailedEmployeeClothesItemStore_SelectedDetailedEmployeeItemChanged()
        {
            OnPropertyChanged(nameof(HasSelectedDetailedEmployeeListingItem));
            OnPropertyChanged(nameof(EmployeeID));
            OnPropertyChanged(nameof(EmployeeLastname));
            OnPropertyChanged(nameof(EmployeeFirstname));
            OnPropertyChanged(nameof(ClothesID));
            OnPropertyChanged(nameof(ClothesName));
            OnPropertyChanged(nameof(Size));
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(Comment));
        }

        protected override void Dispose()
        {
            _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItemChanged -= SelectedDetailedEmployeeClothesItemStore_SelectedDetailedEmployeeItemChanged;

            base.Dispose();
        }
    }
}
