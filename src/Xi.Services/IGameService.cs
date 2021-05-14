namespace Xi.Services
{
  using System.Collections.Generic;
  using Xi.Models.Game;

  public interface IGameService
  {
    public List<Game> Games();
  }
}