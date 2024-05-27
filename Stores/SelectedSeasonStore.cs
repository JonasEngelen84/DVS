using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVS.Stores
{
    public class SelectedSeasonStore
    {
        private string _selectedSeason;
        public string SelectedSeason
        {
            get
            {
                return _selectedSeason;
            }
            set
            {
                _selectedSeason = value;
                SelectedSeasonChanged?.Invoke();
            }
        }

        public event Action SelectedSeasonChanged;
    }
}
