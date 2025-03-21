﻿using DVS.Domain.Commands.SeasonCommands;
using DVS.Domain.Models;
using DVS.WPF.ViewModels.Forms;

namespace DVS.WPF.Stores
{
    public class SeasonStore(ICreateSeasonCommand createSeasonCommand,
                             IDeleteSeasonCommand deleteSeasonCommand)
    {
        private readonly List<Season> _seasons = [];
        public IEnumerable<Season> Seasons => _seasons;

        public event Action<Season, AddEditSeasonFormViewModel> SeasonAdded;
        public event Action<Season, AddEditSeasonFormViewModel> SeasonUpdated;
        public event Action<Guid, AddEditSeasonFormViewModel> SeasonDeleted;
        public event Action<AddEditSeasonFormViewModel> AllSeasonsDeleted;

        public void Load(List<Season> seasons)
        {
            _seasons.Clear();

            if (seasons != null)
            {
                _seasons.AddRange(seasons);
            }
        }

        public async Task Add(Season season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            await createSeasonCommand.Execute(season);

            _seasons.Add(season);

            SeasonAdded.Invoke(season, addEditSeasonFormViewModel);
        }

        public void Update(Season editedSeason, AddEditSeasonFormViewModel? addEditSeasonFormViewModel)
        {
            int index = _seasons.FindIndex(y => y.Id == editedSeason.Id);

            if (index > -1)
            {
                _seasons[index] = editedSeason;
                SeasonUpdated.Invoke(editedSeason, addEditSeasonFormViewModel != null ? addEditSeasonFormViewModel : null);
            }
            else
            {
                throw new InvalidOperationException("Umbenennen der Saison nicht möglich.");
            }

            editedSeason.IsDirty = true;
        }

        public async Task Delete(Season season, AddEditSeasonFormViewModel addEditSeasonFormViewModel)
        {
            await deleteSeasonCommand.Execute(season);

            int index = _seasons.FindIndex(y => y.Id == season.Id);

            if (index > -1)
            {
                _seasons.RemoveAll(y => y.Id == season.Id);
                SeasonDeleted.Invoke(season.Id, addEditSeasonFormViewModel);
            }
            else
            {
                throw new InvalidOperationException("Löschen der Saison nicht möglich.");
            }            
        }
    }
}
