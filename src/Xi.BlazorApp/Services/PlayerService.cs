namespace Xi.BlazorApp.Services
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Components.Authorization;
  using Microsoft.Extensions.Options;
  using Xi.BlazorApp.Config;
  using Xi.BlazorApp.Extensions;
  using Xi.Database;
  using Xi.Database.Dtos;
  using Xi.Models.Game;

  public class PlayerService : IPlayerService
  {
    private readonly XiContext db;
    private readonly XiConfig config;

    public PlayerService(XiContext db, IOptions<XiConfig> options)
    {
      this.db = db;
      this.config = options.Value;
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
  }
}