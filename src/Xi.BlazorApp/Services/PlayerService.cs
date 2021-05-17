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
    private readonly AuthenticationStateProvider authenticationStateProvider;

    public PlayerService(XiContext db, IOptions<XiConfig> options, AuthenticationStateProvider authenticationStateProvider)
    {
      this.db = db;
      this.config = options.Value;
      this.authenticationStateProvider = authenticationStateProvider;
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
        .FirstOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase))?
        .ToPlayer();
    }

    public async Task<Player> LoggedInUserAsync()
    {
      var state = await this.authenticationStateProvider.GetAuthenticationStateAsync();

      var name = state.Get("name");
      var email = state.Get("emailaddress", false);

      var existingPlayer = this.FindByEmail(email);

      if (existingPlayer == null)
      {
        var newPlayer = new PlayerDto
        {
          Name = name.Split(' ').First(),
          Email = email,
        };

        this.db.Players.Add(newPlayer);
        await this.db.SaveChangesAsync();

        existingPlayer = newPlayer.ToPlayer();
      }

      return existingPlayer;
    }
  }
}