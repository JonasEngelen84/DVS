using DVS.Models;

namespace DVS.Stores
{
    public class SelectedCategoryStore
    {
        private CategoryModel _selectedCategory;
        public CategoryModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
            }
        }

        private CategoryModel _editedCategory;
        public CategoryModel EditedCategory
        {
            get => _editedCategory;
            set
            {
                _editedCategory = value;
                SelectedClothesModelChanged?.Invoke();
            }
        }

        public event Action SelectedClothesModelChanged;
    }
}
