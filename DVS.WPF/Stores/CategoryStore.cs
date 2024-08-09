using DVS.Domain.Commands.Category;
using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.WPF.ViewModels.Forms;

namespace DVS.WPF.Stores
{
    public class CategoryStore(IGetAllCategoriesQuery getAllCategoriesQuery,
                               ICreateCategoryCommand createCategoryCommand,
                               IUpdateCategoryCommand updateCategoryCommand,
                               IDeleteCategoryCommand deleteCategoryCommand)
    {
        private readonly List<Category> _categories = [];
        public IEnumerable<Category> Categories => _categories;

        private readonly IGetAllCategoriesQuery _getAllCategoriesQuery;
        private readonly ICreateCategoryCommand _createCategoryCommand = createCategoryCommand;
        private readonly IUpdateCategoryCommand _updateCategoryCommand = updateCategoryCommand;
        private readonly IDeleteCategoryCommand _deleteCategoryCommand = deleteCategoryCommand;

        public event Action CategoriesLoaded;
        public event Action<Category, AddEditCategoryFormViewModel> CategoryAdded;
        public event Action<Category, AddEditCategoryFormViewModel> CategoryEdited;
        public event Action<Guid, AddEditCategoryFormViewModel> CategoryDeleted;
        public event Action<AddEditCategoryFormViewModel> AllCategoriesDeleted;

        public async Task Load()
        {
            //await _getAllCategoriesQuery.Execute();
            _categories.Clear();
            CategoriesLoaded?.Invoke();
        }

        public async Task Add(Category category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            //await _createCategoryCommand.Execute(category);
            _categories.Add(category);
            CategoryAdded.Invoke(category, addEditCategoryFormViewModel);
        }

        public async Task Update(Category category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            //await _updateCategoryCommand.Execute(category);

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

        public async Task Delete(Guid guidID, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            //await _deleteCategoryCommand.Execute(guidID);

            var categoryToDelete = _categories.FirstOrDefault(y => y.GuidID == guidID);

            if (categoryToDelete != null)
            {
                _categories.RemoveAll(y => y.GuidID == guidID); ;
                CategoryDeleted.Invoke(guidID, addEditCategoryFormViewModel);
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
