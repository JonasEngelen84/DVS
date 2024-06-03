namespace DVS.Stores
{
    public class CategoryStore
    {
        private readonly List<String> _categories;
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
