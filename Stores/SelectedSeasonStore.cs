using DVS.Models;

namespace DVS.Stores
{
    public class SelectedSeasonStore
    {
        private SeasonModel _selectedSeason;
        public SeasonModel SelectedSeason
        {
            get => _selectedSeason;
            set
            {
                _selectedSeason = value;
            }
        }
        
        private string _editedSeason;
        public string EditedSeason
        {
            get => _editedSeason;
            set
            {
                _editedSeason = value;
            }
        }
    }
}
