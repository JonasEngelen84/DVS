using DVS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Stores
{
    public class SelectedCategoryStore
    {
        private string _selectedCategory;
        public string SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                SelectedCategoryChanged?.Invoke();
            }
        }

        public event Action SelectedCategoryChanged;
    }
}
