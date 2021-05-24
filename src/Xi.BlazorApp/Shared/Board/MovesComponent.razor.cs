namespace Xi.BlazorApp.Shared.Board
{
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Toolbelt.Blazor.HotKeys;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Stores.Features.Game.Actions.Moves;

  public partial class MovesComponent
  {
    [Parameter]
    public GameModel GameModel { get; set; } = default!;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private HotKeys HotKeys { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
      this.HotKeys.CreateContext()
        .Add(ModKeys.Ctrl, Keys.Left, this.Previous)
        .Add(ModKeys.Ctrl, Keys.Right, this.Next)
        .Add(ModKeys.Ctrl, Keys.Home, this.First)
        .Add(ModKeys.Ctrl, Keys.End, this.Last);

      await base.OnInitializedAsync();
    }

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