namespace Xi.BlazorApp.Shared.Board
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.Game.Actions.Moves;

  public partial class MoveComponent
  {
    [Parameter]
    public GameModel GameModel { get; set; } = default!;

    [Parameter]
    public int Index { get; set; } = default!;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    public Current Current { get; set; } = default!;

    private void RemoveMove()
    {
      this.Dispatcher.Dispatch(new RemoveMovesAction(this.GameModel, this.Index));
    }

    private void ConfirmMove()
    {
      this.Dispatcher.Dispatch(new ConfirmMoveAction(this.Current.LoggedInPLayerId(), this.GameModel, this.Index));
    }
  }
}