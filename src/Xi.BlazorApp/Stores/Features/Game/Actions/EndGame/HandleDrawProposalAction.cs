namespace Xi.BlazorApp.Stores.Features.Game.Actions.EndGame;

using Xi.BlazorApp.Models;
using Xi.Models.Game;

public class HandleDrawProposalAction
{
  public HandleDrawProposalAction(bool accept, GameModel gameModel, Player player)
  {
    this.Accept = accept;
    this.GameModel = gameModel;
    this.Player = player;
  }

  public bool Accept { get; }

  public GameModel GameModel { get; }

  public Player Player { get; }
}