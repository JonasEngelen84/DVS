using DVS.Domain.Models;

namespace DVS.Domain.Commands.Season
{
    public interface ICreateSeasonCommand
    {
        Task Execute(SeasonModel season);
    }
}
