using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Stores
{
    public class CategoryStore
    {
        private readonly List<String> _categories;
        public IEnumerable<string> Categories => _categories;

        public event Action CategoriesLoaded;

        public CategoryStore()
        {
            _categories = ["Sweatshirt", "Hose", "Pullover", "Kopfbedeckung", "Jacke", "Schuhwerk", "Hemd"];
        }

        public void Load()
        {
            CategoriesLoaded?.Invoke();
        }

        public void Add(string category)
        {
            _categories.Add(category);
        }
    }
}
