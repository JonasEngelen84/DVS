using DVS.Domain.Models;
using DVS.WPF.Stores;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class CommentEmployeeClothesFormViewModel(ICommand submitComment,
                                                     SelectedDetailedEmployeeClothesItemStore selectedDetailedEmployeeClothesItemStore)
                                                     : ViewModelBase
    {
        private readonly SelectedDetailedEmployeeClothesItemStore _selectedDetailedEmployeeClothesItemStore = selectedDetailedEmployeeClothesItemStore;

        private DetailedEmployeeListingItemViewModel SelectedDetailedEmployeeItem => _selectedDetailedEmployeeClothesItemStore.SelectedDetailedEmployeeItem;

        public bool HasSelectedDetailedEmployeeListingItem => SelectedDetailedEmployeeItem != null;
        public Guid? EmployeeClothesSizeGuidID => SelectedDetailedEmployeeItem.EmployeeClothesSizeGuidID;
        public Employee Employee => SelectedDetailedEmployeeItem.Employee;
        public string EmployeeID => SelectedDetailedEmployeeItem.Employee.ID;
        public string EmployeeLastname => SelectedDetailedEmployeeItem.Lastname;
        public string EmployeeFirstname => SelectedDetailedEmployeeItem.Firstname;
        public EmployeeClothesSize EmployeeClothesSize => SelectedDetailedEmployeeItem.EmployeeClothesSize;
        public string? ClothesID => SelectedDetailedEmployeeItem.ClothesID;
        public string? ClothesName => SelectedDetailedEmployeeItem.ClothesName;
        public string? Size => SelectedDetailedEmployeeItem.Size;
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
                OnPropertyChanged(nameof(CanSubmit));
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

        public bool CanSubmit => !Comment.Equals(EmployeeClothesSize.Comment);

        public bool HasError;

        public ICommand SubmitComment { get; } = submitComment;
    }
}
