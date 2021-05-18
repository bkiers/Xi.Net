namespace Xi.BlazorApp.Shared.Board
{
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Stores.Features.Game.Actions.CycleMoves;

  public partial class MovesComponent
  {
    [Parameter]
    public GameModel GameModel { get; set; } = default!;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    private void First()
    {
      this.Dispatcher.Dispatch(new FirstMoveAction(this.GameModel));
    }

    private void Previous()
    {
      this.Dispatcher.Dispatch(new PreviousMoveAction(this.GameModel));
    }

    private void Next()
    {
      this.Dispatcher.Dispatch(new NextMoveAction(this.GameModel));
    }

    private void Last()
    {
      this.Dispatcher.Dispatch(new LastMoveAction(this.GameModel));
    }
  }
}