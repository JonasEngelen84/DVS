using DVS.Domain.Models;
using DVS.WPF.Stores;
using System.Windows.Input;

namespace DVS.WPF.ViewModels.Forms
{
    public class CommentEmployeeClothesFormViewModel(ICommand submitComment,
                                                     SelectedEmployeeClothesSizeStore selectedDetailedEmployeeClothesItemStore)
                                                     : ViewModelBase
    {
        private readonly SelectedEmployeeClothesSizeStore _selectedEmployeeClothesSizeStore = selectedDetailedEmployeeClothesItemStore;

        private EmployeeClothesSize SelectedEmployeeClothesSize => _selectedEmployeeClothesSizeStore.SelectedEmployeeClothesSize;

        public bool HasSelectedDetailedEmployeeListingItem => SelectedEmployeeClothesSize != null;
        public Employee Employee => SelectedEmployeeClothesSize.Employee;
        public string EmployeeId => SelectedEmployeeClothesSize.Employee.Id;
        public string EmployeeLastname => SelectedEmployeeClothesSize.Employee.Lastname;
        public string EmployeeFirstname => SelectedEmployeeClothesSize.Employee.Firstname;
        public string? ClothesId => SelectedEmployeeClothesSize.ClothesSize.Clothes.Id;
        public string? ClothesName => SelectedEmployeeClothesSize.ClothesSize.Clothes.Name;
        public string? Size => SelectedEmployeeClothesSize.ClothesSize.Size.Size;
        public int? Quantity => SelectedEmployeeClothesSize.Quantity;

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

        public bool HasError;

        public ICommand SubmitComment { get; } = submitComment;
    }
}
