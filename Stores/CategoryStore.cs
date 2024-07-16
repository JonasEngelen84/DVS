using DVS.Models;
using DVS.ViewModels.Forms;

namespace DVS.Stores
{
    public class CategoryStore
    {
        private readonly List<CategoryModel> _categories;
        public IEnumerable<CategoryModel> Categories => _categories;

        public event Action CategoriesLoaded;
        public event Action<CategoryModel, AddEditCategoryFormViewModel> CategoryAdded;
        public event Action<CategoryModel, AddEditCategoryFormViewModel> CategoryEdited;
        public event Action<CategoryModel, AddEditCategoryFormViewModel> CategoryDeleted;
        public event Action<AddEditCategoryFormViewModel> AllCategoriesDeleted;


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

        public async Task Add(CategoryModel category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            _categories.Add(category);
            CategoryAdded.Invoke(category, addEditCategoryFormViewModel);
        }

        public async Task Edit(CategoryModel category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            int index = _categories.FindIndex(y => y.GuidID == category.GuidID);

            if (index > -1)
            {
                _categories[index] = category;
                CategoryEdited.Invoke(category, addEditCategoryFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Kategorie nicht möglich.");
            }
        }

        public async Task Delete(CategoryModel category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            var categoryToDelete = _categories.FirstOrDefault(y => y.GuidID == category.GuidID);

            if (categoryToDelete != null)
            {
                _categories.Remove(category);
                CategoryDeleted.Invoke(category, addEditCategoryFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Kategorie nicht möglich.");
            }
        }

        public async Task ClearCategories(AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            if (_categories != null)
            {
                _categories.Clear();
                AllCategoriesDeleted.Invoke(addEditCategoryFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Kategorien nicht möglich.");
            }
        }
    }
}
