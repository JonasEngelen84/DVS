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
        private readonly ObservableCollection<String> _categories;
        public IEnumerable<string> Categories => _categories;

        public CategoryStore()
        {
            _categories = ["Sweatshirt", "Hose", "Pullover", "Kopfbedeckung", "Jacke", "Schuhwerk", "Hemd"];
        }

        public void Add(string category)
        {
            _categories.Add(category);
        }
    }
}
