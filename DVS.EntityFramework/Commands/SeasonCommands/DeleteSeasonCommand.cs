﻿using DVS.Domain.Commands.SeasonCommands;
using DVS.Domain.Models;

namespace DVS.EntityFramework.Commands.SeasonCommands
{
    public class DeleteSeasonCommand(DVSDbContextFactory contextFactory) : IDeleteSeasonCommand
    {
        private readonly DVSDbContextFactory _contextFactory = contextFactory;

        public async Task Execute(Season season)
        {
            using DVSDbContext context = _contextFactory.Create();

            context.Seasons.Remove(season);

            await context.SaveChangesAsync();
        }
    }
}
