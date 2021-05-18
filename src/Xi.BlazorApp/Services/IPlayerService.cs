namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Xi.Models.Game;

  public interface IPlayerService
  {
    public List<Player> AllPlayers();

    public List<Player> AllPlayersExcept(int playerId);

    public Player? FindByEmail(string email);

    public Player FindByEmailOrCreate(string email, string name);
  }
}