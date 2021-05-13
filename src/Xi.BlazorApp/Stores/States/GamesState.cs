namespace Xi.BlazorApp.Stores.States
{
  using System.Collections.Generic;
  using Xi.Models.Game;

  public class GamesState : RootState
  {
    public GamesState(bool isLoading, string? currentErrorMessage, List<Game>? games)
      : base(isLoading, currentErrorMessage)
    {
      this.Games = games;
    }

    public List<Game>? Games { get; }
  }
}