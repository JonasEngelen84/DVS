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

        private string _editedCategory;
        public string EditedCategory
        {
            get => _editedCategory;
            set
            {
                _editedCategory = value;
            }
        }
    }
}
