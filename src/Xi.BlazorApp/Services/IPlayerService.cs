namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public interface IPlayerService
  {
    public List<Player> AllPlayers();

    public List<Player> AllPlayersExcept(int playerId);

    public Player? FindByEmail(string email);

    public Player FindById(int playerId);

    public Player FindByEmailOrCreate(string email, string name);

    public Player Update(int playerId, bool showPossibleMoves);
  }
}