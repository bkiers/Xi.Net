namespace Xi.BlazorApp.Pages
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Components;
  using MudBlazor;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;

  public partial class Games
  {
    [Inject]
    public IGameService GameService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private MudTable<GameModel> Table { get; set; } = default!;

    private async Task<TableData<GameModel>> ReloadGames(TableState state)
    {
      var (totalGames, pagedGames) = this.GameService.PagedGames(state.Page, state.PageSize);

      return await Task.FromResult(new TableData<GameModel> { TotalItems = totalGames, Items = pagedGames });
    }

    private void OpenGame()
    {
      this.NavigationManager.NavigateTo($"/games/{this.Table.SelectedItem.Game.Id}");
    }
  }
}