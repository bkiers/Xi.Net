namespace Xi.BlazorApp.Shared.Board
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Microsoft.JSInterop;
  using Toolbelt.Blazor.HotKeys;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Stores.Features.Game.Actions.Moves;
  using Xi.Models.Game;

  public partial class MovesComponent
  {
    [Parameter]
    public GameModel GameModel { get; set; } = default!;

    [Inject]
    public IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private HotKeys HotKeys { get; set; } = default!;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();

      this.HotKeys.CreateContext()
        .Add(ModKeys.None, Keys.Left, this.Previous)
        .Add(ModKeys.None, Keys.Right, this.Next)
        .Add(ModKeys.None, Keys.Home, this.First)
        .Add(ModKeys.None, Keys.End, this.Last)
        .Add(ModKeys.None, Keys.Up, this.First)
        .Add(ModKeys.None, Keys.Down, this.Last);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
      await base.OnAfterRenderAsync(firstRender);

      if (firstRender || this.GameModel.JustMadeMoveAndReset())
      {
        await this.JSRuntime.InvokeVoidAsync("scrollToLastMove");
      }
    }

    private List<Tuple<int, int, int>> MoveNumbers()
    {
      var moves = new List<Tuple<int, int, int>>();

      for (var i = 0; i < this.GameModel.Game.Moves.Count; i += 2)
      {
        var moveNumber = ((i + 1) / 2) + 1;

        moves.Add(new Tuple<int, int, int>(moveNumber, i, i + 1));
      }

      return moves;
    }

    private async Task First()
    {
      this.Dispatcher.Dispatch(new FirstMoveAction(this.GameModel));

      await this.JSRuntime.InvokeVoidAsync("scrollToFirstMove");
    }

    private void Previous()
    {
      this.Dispatcher.Dispatch(new PreviousMoveAction(this.GameModel));
    }

    private void Next()
    {
      this.Dispatcher.Dispatch(new NextMoveAction(this.GameModel));
    }

    private async Task Last()
    {
      this.Dispatcher.Dispatch(new LastMoveAction(this.GameModel));

      await this.JSRuntime.InvokeVoidAsync("scrollToLastMove");
    }
  }
}