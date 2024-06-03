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
            }
        }
        
        private string _editedSeason;
        public string EditedSeason
        {
            get
            {
                return _editedSeason;
            }
            set
            {
                _editedSeason = value;
            }
        }
    }
}
