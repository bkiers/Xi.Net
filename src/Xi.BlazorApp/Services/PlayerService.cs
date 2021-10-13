namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using Xi.Database;
  using Xi.Models;
  using Xi.Models.Game;

  public class PlayerService : IPlayerService
  {
    private readonly XiContext db;

    public PlayerService(XiContext db)
    {
      this.db = db;
    }

    public List<Player> AllPlayers()
    {
      return this.db.Players
        .Select(p => p.ToPlayer())
        .ToList();
    }

    public List<Player> AllPlayersExcept(int playerId)
    {
      return this.AllPlayers()
        .Where(p => p.Id != playerId)
        .ToList();
    }

    public Player? FindByEmail(string email)
    {
      return this.db.Players
        .FirstOrDefault(p => p.Email == email)?
        .ToPlayer();
    }

    public Player FindById(int playerId)
    {
      return this.db.Players
        .Single(p => p.Id == playerId)
        .ToPlayer();
    }

    public Player Update(int playerId, Settings settings)
    {
      var player = this.db.Players.Single(p => p.Id == playerId);

      player.SettingsJson = JsonConvert.SerializeObject(settings);

      this.db.Players.Update(player);
      this.db.SaveChanges();

      return player.ToPlayer();
    }
  }
}