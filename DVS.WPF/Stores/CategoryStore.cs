using DVS.Domain.Commands.CategoryCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.WPF.ViewModels.Forms;
using System.Windows;

namespace DVS.WPF.Stores
{
    public class CategoryStore(IGetAllCategoriesQuery getAllCategoriesQuery,
                               ICreateCategoryCommand createCategoryCommand,
                               IUpdateCategoryCommand updateCategoryCommand,
                               IDeleteCategoryCommand deleteCategoryCommand)
    {
        public Category Categoryless { get; }
        
        private readonly IGetAllCategoriesQuery _getAllCategoriesQuery = getAllCategoriesQuery;
        private readonly ICreateCategoryCommand _createCategoryCommand = createCategoryCommand;
        private readonly IUpdateCategoryCommand _updateCategoryCommand = updateCategoryCommand; 
        private readonly IDeleteCategoryCommand _deleteCategoryCommand = deleteCategoryCommand;

        private readonly List<Category> _categories = [];
        public IEnumerable<Category> Categories => _categories;

        public event Action CategoriesLoaded;
        public event Action<Category, AddEditCategoryFormViewModel> CategoryAdded;
        public event Action<Category, AddEditCategoryFormViewModel> CategoryUpdated;
        public event Action<Guid, AddEditCategoryFormViewModel> CategoryDeleted;
        public event Action<AddEditCategoryFormViewModel> AllCategoriesDeleted;


        public async Task Load()
        {
            IEnumerable<Category> categorie = [];

            try
            {
                categorie = await _getAllCategoriesQuery.Execute();
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Laden der Categories von Datenbank ist fehlgeschlagen!", "CategoryStore, Load", button, icon);
            }

            _categories.Clear();

            if (categorie != null)
            {
                _categories.AddRange(categorie);
            }

            CategoriesLoaded?.Invoke();
        }

        public async Task Add(Category category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            try
            {
                await _createCategoryCommand.Execute(category);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Hinzufügen der Category in Datenbank ist fehlgeschlagen!", "CategoryStore, Add", button, icon);
            }

            _categories.Add(category);

            CategoryAdded.Invoke(category, addEditCategoryFormViewModel);
        }

        public async Task Update(Category updatedCategory, AddEditCategoryFormViewModel? addEditCategoryFormViewModel)
        {
            try
            {
                //await _updateCategoryCommand.Execute(category);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Updaten der Category in Datenbank ist fehlgeschlagen!", "CategoryStore, Update", button, icon);
            }

            int index = _categories.FindIndex(y => y.GuidID == updatedCategory.GuidID);

            if (index > -1)
            {
                _categories[index] = updatedCategory;
                CategoryUpdated.Invoke(updatedCategory, addEditCategoryFormViewModel != null ? addEditCategoryFormViewModel : null);
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Kategorie nicht möglich.");
            }
        }

        public async Task Delete(Category category, AddEditCategoryFormViewModel addEditCategoryFormViewModel)
        {
            try
            {
                await _deleteCategoryCommand.Execute(category);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Löschen der Category in Datenbank ist fehlgeschlagen!", "CategoryStore, Delete", button, icon);
            }

            var categoryToDelete = _categories.FirstOrDefault(y => y.GuidID == category.GuidID);

            if (categoryToDelete != null)
            {
                _categories.RemoveAll(y => y.GuidID == category.GuidID); ;
                CategoryDeleted.Invoke(category.GuidID, addEditCategoryFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Kategorie nicht möglich.");
            }
        }
    }
}
