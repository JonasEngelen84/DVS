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
        public event Action<CategoryModel> CategoryDeleted;
        public event Action AllCategoriesDeleted;


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
        {//TODO: Bedingung zum Adden hinzufügen
            CategoryAdded.Invoke(category);
            _categories.Add(category);
        }

        public async Task Edit(CategoryModel oldCategory, string editedCategory)
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

        public async Task Delete(CategoryModel category)
        {
            var categoryToDelete = _categories.FirstOrDefault(y => y.Name == category.Name);

            if (categoryToDelete != null)
            {
                CategoryDeleted.Invoke(category);
                _categories.Remove(categoryToDelete);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Kategorie nicht möglich.");
            }
        }
        public async Task ClearCategories()
        {
            if (_categories != null)
            {
                AllCategoriesDeleted.Invoke();
                _categories.Clear();
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Kategorien nicht möglich.");
            }
        }
    }
}
