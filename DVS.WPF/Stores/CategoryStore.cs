using DVS.Domain.Commands.CategoryCommands;
using DVS.Domain.Models;

namespace DVS.WPF.Stores
{
    public class CategoryStore(ICreateCategoryCommand createCategoryCommand,
                               IDeleteCategoryCommand deleteCategoryCommand)
    {
        private readonly List<Category> _categories = [];
        public IEnumerable<Category> Categories => _categories;

        public event Action<Category> CategoryAdded;
        public event Action<Category> CategoryUpdated;
        public event Action<Category> CategoryDeleted;


        public void Load(List<Category> categorie)
        {
            _categories.Clear();

            if (categorie != null)
            {
                _categories.AddRange(categorie);
            }
        }

        public async Task Add(Category newCategory)
        {
            await createCategoryCommand.Execute(newCategory);

            _categories.Add(newCategory);

            CategoryAdded.Invoke(newCategory);
        }

        public void Update(Category editedCategory)
        {
            int index = _categories.FindIndex(y => y.Id == editedCategory.Id);

            if (index > -1)
            {
                _categories[index] = editedCategory;
                CategoryUpdated.Invoke(editedCategory);
            }

            editedCategory.IsDirty = true;
        }

        public async Task Delete(Category CategoryToDelete)
        {
            await deleteCategoryCommand.Execute(CategoryToDelete);

            int index = _categories.FindIndex(y => y.Id == CategoryToDelete.Id);

            if (index > -1)
            {
                _categories.RemoveAll(y => y.Id == CategoryToDelete.Id);
                CategoryDeleted.Invoke(CategoryToDelete);
            }           
        }
    }
}
