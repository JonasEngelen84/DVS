using DVS.Domain.Commands.CategoryCommands;
using DVS.Domain.Models;
using DVS.WPF.ViewModels.Forms;

namespace DVS.WPF.Stores
{
    public class CategoryStore(ICreateCategoryCommand createCategoryCommand,
                               IDeleteCategoryCommand deleteCategoryCommand)
    {
        public Category Categoryless { get; }

        private readonly List<Category> _categories = [];
        public IEnumerable<Category> Categories => _categories;

        public event Action<Category, AddEditCategoryFormViewModel> CategoryAdded;
        public event Action<Category, AddEditCategoryFormViewModel> CategoryUpdated;
        public event Action<Guid, AddEditCategoryFormViewModel> CategoryDeleted;
        public event Action<AddEditCategoryFormViewModel> AllCategoriesDeleted;


        public void Load(List<Category> categorie)
        {
            _categories.Clear();

            if (categorie != null)
            {
                _categories.AddRange(categorie);
            }
        }

        public async Task Add(Category category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            await createCategoryCommand.Execute(category);

            _categories.Add(category);

            CategoryAdded.Invoke(category, addEditCategoryFormViewModel);
        }

        public void Update(Category editedCategory, AddEditCategoryFormViewModel? addEditCategoryFormViewModel)
        {
            int index = _categories.FindIndex(y => y.Id == editedCategory.Id);

            if (index > -1)
            {
                _categories[index] = editedCategory;
                CategoryUpdated.Invoke(editedCategory, addEditCategoryFormViewModel != null ? addEditCategoryFormViewModel : null);
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Kategorie nicht möglich.");
            }

            editedCategory.IsDirty = true;
        }

        public async Task Delete(Category category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            await deleteCategoryCommand.Execute(category);

            int index = _categories.FindIndex(y => y.Id == category.Id);

            if (index > -1)
            {
                _categories.RemoveAll(y => y.Id == category.Id);
                CategoryDeleted.Invoke(category.Id, addEditCategoryFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Kategorie nicht möglich.");
            }            
        }
    }
}
