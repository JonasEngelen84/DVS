using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace DVS.Stores
{
    public class CategoryStore
    {
        private readonly ObservableCollection<string> _categories;
        private readonly CollectionViewSource _categoryCollectionViewSource;
        public IEnumerable<string> Categories => _categoryCollectionViewSource.View.Cast<string>();

        public event Action CategoriesLoaded;
        public event Action<string> CategoryAdded;


        public CategoryStore()
        {
            _categories = ["Hose", "Pullover", "Shirt", "Jacke", "Kopfbedeckung"];
            _categoryCollectionViewSource = new CollectionViewSource { Source = _categories };
            _categoryCollectionViewSource.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        }


        public async Task Load()
        {
            CategoriesLoaded?.Invoke();
        }

        public async Task Add(string category)
        {
            _categories.Add(category);
            CategoryAdded?.Invoke(category);
        }
    }
}
