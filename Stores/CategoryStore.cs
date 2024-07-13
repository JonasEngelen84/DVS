using DVS.Models;

namespace DVS.Stores
{
    public class CategoryStore
    {
        private readonly List<CategoryModel> _categories;
        public IEnumerable<CategoryModel> Categories => _categories;

        public event Action CategoriesLoaded;
        public event Action<CategoryModel> CategoryAdded;
        public event Action<CategoryModel> CategoryEdited;
        public event Action<CategoryModel> CategoryDeleted;
        public event Action AllCategoriesDeleted;


        public CategoryStore()
        {
            _categories = [new(Guid.NewGuid(), "Pullover"),  new(Guid.NewGuid(), "Shirt"),
                new(Guid.NewGuid(), "Jacke"), new(Guid.NewGuid(), "Kopfbedeckung"), new(Guid.NewGuid(), "Hose") ];
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

        public async Task Edit(CategoryModel category)
        {
            int index = _categories.FindIndex(y => y.GuidID == category.GuidID);

            if (index > -1)
            {
                _categories[index] = category;
                CategoryEdited.Invoke(category);
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Kategorie nicht möglich.");
            }
        }

        public async Task Delete(CategoryModel category)
        {
            var categoryToDelete = _categories.FirstOrDefault(y => y.GuidID == category.GuidID);

            if (categoryToDelete != null)
            {
                _categories.Remove(category);
                CategoryDeleted.Invoke(category);
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
