using DVS.Domain.Commands.Season;
using DVS.Domain.Models;
using DVS.Domain.Queries;
using DVS.WPF.ViewModels.Forms;

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

        private readonly List<SeasonModel> _seasons = [];
        public IEnumerable<SeasonModel> Seasons => _seasons;

        public event Action SeasonsLoaded;
        public event Action<SeasonModel, AddEditSeasonFormViewModel> SeasonAdded;
        public event Action<SeasonModel, AddEditSeasonFormViewModel> SeasonEdited;
        public event Action<Guid, AddEditSeasonFormViewModel> SeasonDeleted;
        public event Action<AddEditSeasonFormViewModel> AllSeasonsDeleted;

        public async Task Load()
        {
            //await _getAllSeasonsQuery.Execute();
            _seasons.Clear();
            SeasonsLoaded?.Invoke();
        }

        public async Task Add(SeasonModel season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            //await _createSeasonCommand.Execute(season);
            _seasons.Add(season);
            SeasonAdded.Invoke(season, addEditSeasonFormViewModel);
        }

        public async Task Update(SeasonModel season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            //await _updateSeasonCommand.Execute(season);

            int index = _seasons.FindIndex(y => y.GuidID == season.GuidID);

            if (index > -1)
            {
                _seasons[index] = season;
                SeasonEdited.Invoke(season, addEditSeasonFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Saison nicht möglich.");
            }
        }

        public async Task Delete(Guid guidID, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            //await _deleteSeasonCommand.Execute(guidID);

            var seasonToDelete = _seasons.FirstOrDefault(y => y.GuidID == guidID);

            if (seasonToDelete != null)
            {
                _seasons.RemoveAll(y => y.GuidID == guidID);
                SeasonDeleted.Invoke(guidID, addEditSeasonFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Saison nicht möglich.");
            }
        }

        public async Task ClearSeasons(AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            if (_seasons != null)
            {
                _seasons.Clear();
                AllSeasonsDeleted.Invoke(addEditSeasonFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen aller Saisons nicht möglich.");
            }
        }
    }
}
