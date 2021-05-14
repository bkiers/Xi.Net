namespace Xi.BlazorApp.Shared.Board
{
  using Microsoft.AspNetCore.Components;
  using Xi.Models.Game;

  public partial class PlayerComponent
  {
    [Parameter]
    public Player Player { get; set; } = default!;

    protected override void OnInitialized()
    {
      base.OnInitialized();
    }
  }
}