namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public interface IPlayerService
  {
    List<Player> AllPlayers();

    List<Player> AllPlayersExcept(int playerId);

    Player? FindByEmail(string email);

    Player FindById(int playerId);

    Player FindByEmailOrCreate(string email, string name);

    Player Update(int playerId, bool showPossibleMoves);
  }
}