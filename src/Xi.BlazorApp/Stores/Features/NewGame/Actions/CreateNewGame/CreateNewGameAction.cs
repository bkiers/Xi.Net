namespace Xi.BlazorApp.Stores.Features.NewGame.Actions.CreateNewGame
{
  using Xi.BlazorApp.Models;
  using Xi.Models.Game;

  public class CreateNewGameAction
  {
    public CreateNewGameAction(int loggedInPlayerId, Color playingWithColor, int opponentPlayerId, int daysPerMove)
    {
      this.LoggedInPlayerId = loggedInPlayerId;
      this.PlayingWithColor = playingWithColor;
      this.OpponentPlayerId = opponentPlayerId;
      this.DaysPerMove = daysPerMove;
    }

    public int LoggedInPlayerId { get; }

    public Color PlayingWithColor { get; }

    public int OpponentPlayerId { get; }

    public int DaysPerMove { get; }
  }
}