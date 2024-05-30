using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DVS.ViewModels.Forms
{
    public class AddEditClothesFormViewModel(
        ICommand openAddEditCategoriesCommand,
        ICommand openAddEditSeasonsCommand,
        ICommand submitAddClothesCommand,
        ICommand closeModalCommand)
        : ViewModelBase
    {
        public ICommand OpenAddEditCategoriesCommand { get; } = openAddEditCategoriesCommand;
        public ICommand OpenAddEditSeasonsCommand { get; } = openAddEditSeasonsCommand;
        public ICommand SubmitAddEditClothesCommand { get; } = submitAddClothesCommand;
        public ICommand CloseModalCommand { get; } = closeModalCommand;

        private ObservableCollection<string> categories = ["Hose", "Pullover", "Shirt", "Jacke", "Kopfbedeckung"];
        public ObservableCollection<string> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> seasons = ["Sommer", "Winter", "Saisonlos"];
        public ObservableCollection<string> Seasons
        {
            get { return seasons; }
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedSeason;
        public string SelectedSeason
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        
        private string _size;
        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }
        
        private string _quantity;
        public string Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        
        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
    }
}
