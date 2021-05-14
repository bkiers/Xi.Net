namespace Xi.BlazorApp.Services
{
  using System.Collections.Generic;
  using Xi.Models.Game;

  public interface IGameService
  {
    public List<Game> Games();

    public Game? Game(int gameId);
  }
}