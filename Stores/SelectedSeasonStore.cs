namespace DVS.Stores
{
    public class SelectedSeasonStore
    {
        private string _selectedSeason;
        public string SelectedSeason
        {
            get => _selectedSeason;
            set
            {
                _selectedSeason = value;
                SelectedSeasonModelChanged?.Invoke();
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

        public event Action SelectedSeasonModelChanged;
    }
}
