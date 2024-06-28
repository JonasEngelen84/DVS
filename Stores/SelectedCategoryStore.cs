namespace DVS.Stores
{
    public class SelectedCategoryStore
    {
        private string _selectedCategory;
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                SelectedClothesModelChanged?.Invoke();
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

        public event Action SelectedClothesModelChanged;
    }
}
