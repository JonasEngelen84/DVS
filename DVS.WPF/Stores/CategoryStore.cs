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
        private readonly IGetAllCategoriesQuery _getAllCategoriesQuery;
        private readonly ICreateCategoryCommand _createCategoryCommand = createCategoryCommand;
        private readonly IUpdateCategoryCommand _updateCategoryCommand = updateCategoryCommand;
        private readonly IDeleteCategoryCommand _deleteCategoryCommand = deleteCategoryCommand;

        private readonly List<Category> _categories = [new(Guid.NewGuid(), "OHNE")];
        public IEnumerable<Category> Categories => _categories;

        public event Action CategoriesLoaded;
        public event Action<Category, AddEditCategoryFormViewModel> CategoryAdded;
        public event Action<Category, AddEditCategoryFormViewModel> CategoryUpdated;
        public event Action<Guid, AddEditCategoryFormViewModel> CategoryDeleted;
        public event Action<AddEditCategoryFormViewModel> AllCategoriesDeleted;

        public async Task Load()
        {
            try
            {
                IEnumerable<Category> categorie = await _getAllCategoriesQuery.Execute();

                _categories.Clear();

                if (categorie != null)
                {
                    _categories.AddRange(categorie);
                }

                CategoriesLoaded?.Invoke();
            }
            catch (Exception ex)
            {
                //TODO: Fehlerbehandlung beim laden der aus DB
                Console.WriteLine($"Fehler beim Laden der Kategorien: {ex.Message}");
            }
        }

        public async Task Add(Category newCategory, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            //await _createCategoryCommand.Execute(category);
            _categories.Add(newCategory);
            CategoryAdded.Invoke(newCategory, addEditCategoryFormViewModel);
        }

        public async Task Update(Category updatedCategory, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            //await _updateCategoryCommand.Execute(category);

            int index = _categories.FindIndex(y => y.GuidID == updatedCategory.GuidID);

            if (index > -1)
            {
                _categories[index] = updatedCategory;
                CategoryUpdated.Invoke(updatedCategory, addEditCategoryFormViewModel);
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
    }
}
