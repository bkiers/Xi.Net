namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Xi.BlazorApp.Models;
  using Xi.Database;
  using Xi.Database.Dtos;
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

    public Player FindByEmailOrCreate(string email, string name)
    {
      var player = this.FindByEmail(email);

      if (player == null)
      {
        var newPlayer = new PlayerDto
        {
          Name = name.Split(' ').First(),
          Email = email,
        };

        this.db.Players.Add(newPlayer);
        this.db.SaveChanges();

        player = newPlayer.ToPlayer();
      }

      return player;
    }

    public Player Update(int playerId, bool showPossibleMoves)
    {
      var player = this.db.Players.Single(p => p.Id == playerId);

      player.ShowPossibleMoves = showPossibleMoves;

      this.db.Players.Update(player);
      this.db.SaveChanges();

      return player.ToPlayer();
    }
  }
}