﻿namespace DVS.Domain.Commands.Season
{
    public interface ICreateSeasonCommand
    {
        Task Execute(Models.Season season);
    }
}
