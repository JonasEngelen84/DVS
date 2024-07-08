using DVS.Models;

namespace DVS.Stores
{
    public class CategoryStore
    {
        private readonly List<CategoryModel> _categories;
        public IEnumerable<CategoryModel> Categories => _categories;

        public event Action CategoriesLoaded;
        public event Action<CategoryModel> CategoryAdded;
        public event Action<CategoryModel, string> CategoryEdited;


        public CategoryStore()
        {
            _categories = [ new("Pullover"),  new("Shirt"), new("Jacke"), new("Kopfbedeckung"), new("Hose") ];
        }


        public async Task Load()
        {
            _categories.Clear();
            CategoriesLoaded?.Invoke();
        }

        public async Task Add(CategoryModel category)
        {
            _categories.Add(category);
            CategoryAdded?.Invoke(category);
        }

        public async Task Update(CategoryModel oldCategory, string editedCategory)
        {
            var categoryToUpdate = _categories.FirstOrDefault(y => y.Name == oldCategory.Name);

            if (categoryToUpdate != null)
            {
                CategoryEdited.Invoke(oldCategory, editedCategory);
                categoryToUpdate.Name = editedCategory;
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Kategorie nicht möglich.");
            }
        }
    }
}
