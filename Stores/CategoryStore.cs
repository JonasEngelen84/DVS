using DVS.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace DVS.Stores
{
    public class CategoryStore
    {
        private readonly ObservableCollection<CategoryModel> _categories;
        private readonly CollectionViewSource _categoryCollectionViewSource;
        public IEnumerable<CategoryModel> Categories => _categoryCollectionViewSource.View.Cast<CategoryModel>();

        public event Action CategoriesLoaded;
        public event Action<CategoryModel> CategoryAdded;


        public CategoryStore()
        {
            _categories = [ new("Pullover"),  new("Shirt"), new("Jacke"), new("Kopfbedeckung"), new("Hose") ];
            _categoryCollectionViewSource = new CollectionViewSource { Source = _categories };
            _categoryCollectionViewSource.SortDescriptions.Add(new SortDescription(nameof(CategoryModel.Name), ListSortDirection.Ascending));
        }


        public async Task Load()
        {
            CategoriesLoaded?.Invoke();
        }

        public async Task Add(CategoryModel category)
        {
            _categories.Add(category);
            CategoryAdded?.Invoke(category);
        }
    }
}
