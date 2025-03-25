using DVS.Domain.Commands.CategoryCommands;
using DVS.Domain.Models;
using DVS.WPF.ViewModels.Forms;

namespace DVS.WPF.Stores
{
    public class CategoryStore(ICreateCategoryCommand createCategoryCommand,
                               IDeleteCategoryCommand deleteCategoryCommand)
    {
        private readonly List<Category> _categories = [];
        public IEnumerable<Category> Categories => _categories;

        public event Action<Category, AddEditCategoryFormViewModel> CategoryAdded;
        public event Action<Category, AddEditCategoryFormViewModel> CategoryUpdated;
        public event Action<AddEditCategoryFormViewModel> CategoryDeleted;


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

        public async Task Delete(AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            await deleteCategoryCommand.Execute(addEditCategoryFormViewModel.SelectedCategory);

            int index = _categories.FindIndex(y => y.Id == addEditCategoryFormViewModel.SelectedCategory.Id);

            if (index > -1)
            {
                _categories.RemoveAll(y => y.Id == addEditCategoryFormViewModel.SelectedCategory.Id);
                CategoryDeleted.Invoke(addEditCategoryFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Kategorie nicht möglich.");
            }            
        }
    }
}
