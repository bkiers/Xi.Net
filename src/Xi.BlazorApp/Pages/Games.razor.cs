namespace Xi.BlazorApp.Pages;

using System;
using System.Threading.Tasks;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Xi.BlazorApp.Models;
using Xi.BlazorApp.Services;
using Xi.BlazorApp.Shared;
using Xi.BlazorApp.Stores.Features.Players.Actions;

public partial class Games
{
  [Inject]
  private IDialogService DialogService { get; set; } = default!;

  [Inject]
  private IGameService GameService { get; set; } = default!;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  private IDispatcher Dispatcher { get; set; } = default!;

  [Inject]
  private Current Current { get; set; } = default!;

  private MudTable<GameModel> Table { get; set; } = default!;

  protected override void OnInitialized()
  {
    base.OnInitialized();

    this.Dispatcher.Dispatch(new DidSomethingAction(this.Current.PossibleLoggedInPlayerId()));
  }

  private async Task<TableData<GameModel>> ReloadGames(TableState state)
  {
    var (totalGames, pagedGames) = this.GameService.PagedGames(state.Page, state.PageSize);

    return await Task.FromResult(new TableData<GameModel> { TotalItems = totalGames, Items = pagedGames });
  }

  private void OpenGame()
  {
    this.NavigationManager.NavigateTo($"/games/{this.Table.SelectedItem.Game.Id}");
  }

  private async Task NewGame()
  {
    var options = new DialogOptions { CloseButton = false, DisableBackdropClick = true };
    var dialog = this.DialogService.Show<NewGameDialog>(string.Empty, options);

    var result = await dialog.Result;

    if (!result.Cancelled)
    {
      await this.Table.ReloadServerData();
    }
  }
}