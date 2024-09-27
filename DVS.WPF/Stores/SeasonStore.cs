using DVS.Domain.Commands.SeasonCommands;
using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.WPF.ViewModels.Forms;
using System.Windows;

namespace DVS.WPF.Stores
{
    public class SeasonStore(IGetAllSeasonsQuery getAllSeasonsQuery,
                             ICreateSeasonCommand CreateSeasonCommand,
                             IUpdateSeasonCommand UpdateSeasonCommand,
                             IDeleteSeasonCommand DeleteSeasonCommand)
    {
        private readonly IGetAllSeasonsQuery _getAllSeasonsQuery = getAllSeasonsQuery;
        private readonly ICreateSeasonCommand _createSeasonCommand = CreateSeasonCommand;
        private readonly IUpdateSeasonCommand _updateSeasonCommand = UpdateSeasonCommand;
        private readonly IDeleteSeasonCommand _deleteSeasonCommand = DeleteSeasonCommand;

        private readonly List<Season> _seasons = [new(Guid.NewGuid(), "Saisonlos")];
        public IEnumerable<Season> Seasons => _seasons;

        public event Action SeasonsLoaded;
        public event Action<Season, AddEditSeasonFormViewModel> SeasonAdded;
        public event Action<Season, AddEditSeasonFormViewModel> SeasonUpdated;
        public event Action<Guid, AddEditSeasonFormViewModel> SeasonDeleted;
        public event Action<AddEditSeasonFormViewModel> AllSeasonsDeleted;

        public async Task Load()
        {
            IEnumerable<Season> season = [];

            try
            {
                season = await _getAllSeasonsQuery.Execute();
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Laden der Seasons von Datenbank ist fehlgeschlagen!", "SeasonStore, Load", button, icon);
            }

            _seasons.Clear();

            if (season != null)
            {
                _seasons.AddRange(season);
            }

            SeasonsLoaded?.Invoke();
        }

        public async Task Add(Season season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            try
            {
                await _createSeasonCommand.Execute(season);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Hinzufügen der Season in Datenbank ist fehlgeschlagen!", "SeasonStore, Add", button, icon);
            }

            _seasons.Add(season);

            SeasonAdded.Invoke(season, addEditSeasonFormViewModel);
        }

        public async Task Update(Season updatedSeason, AddEditSeasonFormViewModel? addEditSeasonFormViewModel)
        {
            try
            {
                //await _updateSeasonCommand.Execute(season);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Updaten der Season in Datenbank ist fehlgeschlagen!", "SeasonStore, Update", button, icon);
            }

            int index = _seasons.FindIndex(y => y.GuidID == updatedSeason.GuidID);

            if (index > -1)
            {
                _seasons[index] = updatedSeason;
                SeasonUpdated.Invoke(updatedSeason, addEditSeasonFormViewModel != null ? addEditSeasonFormViewModel : null);
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Saison nicht möglich.");
            }
        }

        public async Task Delete(Season season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            try
            {
                await _deleteSeasonCommand.Execute(season);
            }
            catch
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show("Löschen der Season aus Datenbank ist fehlgeschlagen!", "SeasonStore, Delete", button, icon);
            }

            var seasonToDelete = _seasons.FirstOrDefault(y => y.GuidID == season.GuidID);

            if (seasonToDelete != null)
            {
                _seasons.RemoveAll(y => y.GuidID == season.GuidID);
                SeasonDeleted.Invoke(season.GuidID, addEditSeasonFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Saison nicht möglich.");
            }
        }
    }
}
