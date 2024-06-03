using System.Collections.ObjectModel;

namespace DVS.Stores
{
    public class CategoryStore
    {
        private readonly ObservableCollection<string> _categories;
        public IEnumerable<string> Categories => _categories;

        public event Action CategoriesLoaded;
        public event Action<string> CategoryAdded;

        public CategoryStore()
        {
            _categories = [];
        }

        public async Task Load()
        {
            CategoriesLoaded?.Invoke();
        }

        public async Task Add(string category)
        {
            _categories.Add(category);
            CategoryAdded?.Invoke(category);
        }
    }
}
