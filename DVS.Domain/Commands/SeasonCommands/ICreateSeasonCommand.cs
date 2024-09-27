using DVS.Domain.Models;

namespace DVS.Domain.Commands.SeasonCommands
{
    public interface ICreateSeasonCommand
    {
        Task Execute(Season season);
    }
}
