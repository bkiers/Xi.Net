namespace Xi.BlazorApp.Pages
{
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.Components;
  using Microsoft.JSInterop;
  using MudBlazor;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Shared;
  using Xi.BlazorApp.Stores.Features.Game.Actions.EndGame;
  using Xi.BlazorApp.Stores.Features.Game.Actions.LoadGame;
  using Xi.BlazorApp.Stores.Features.Game.Actions.StartGame;
  using Xi.BlazorApp.Stores.Features.Players.Actions;
  using Xi.BlazorApp.Stores.States;
  using Xi.Models.Extensions;
  using Xi.Models.Game;
  using Color = Xi.Models.Game.Color;

  public partial class Game
  {
    private bool showedPendingGamePrompt = false;
    private bool showedPendingDrawPrompt = false;

    [Parameter]
    public int? GameId { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private IDispatcher Dispatcher { get; set; } = default!;

    [Inject]
    private IState<GameState> GameState { get; set; } = default!;

    [Inject]
    private Current Current { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
      await base.OnInitializedAsync();

      this.Dispatcher.Dispatch(new DidSomethingAction(this.Current.PossibleLoggedInPlayerId()));
      var game = this.GameState.Value.GameModel?.Game;

      if (game == null || game.Id != this.GameId)
      {
        this.Dispatcher.Dispatch(new LoadGameAction(this.GameId!.Value));
      }

      this.GameState.StateChanged += (_, state) =>
      {
        var currentGame = state.GameModel?.Game;

        if (currentGame == null)
        {
          return;
        }

        if (currentGame.CheckmatePlayer() != null)
        {
          this.Snackbar.Add($"{currentGame.CheckmatePlayer()!.Name} is checkmate.", Severity.Warning);
        }
        else if (currentGame.StalematePlayer() != null)
        {
          this.Snackbar.Add($"{currentGame.StalematePlayer()!.Name} is stalemate.", Severity.Warning);
        }
        else if (currentGame.CheckPlayer() != null)
        {
          this.Snackbar.Add($"{currentGame.CheckPlayer()!.Name} is check.", Severity.Warning);
        }

        if (state.HasErrors)
        {
          this.Snackbar.Add(state.ErrorMessage, Severity.Error);
        }
      };
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);

      this.GameState.StateChanged -= null;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
      await base.OnAfterRenderAsync(firstRender);

      var game = this.GameState.Value?.GameModel?.Game;

      if (game == null)
      {
        return;
      }

      if (!game.Accepted && !this.showedPendingGamePrompt)
      {
        this.showedPendingGamePrompt = true;

        if (this.Current.LoggedInPlayerId() == game.InvitedPlayer.Id)
        {
          await this.PromptAcceptGame(game.SecondsPerMove.ToDays());
        }
        else
        {
          await this.PromptPendingInvite();
        }
      }
      else if (game.ProposedDrawPlayer != null && !this.showedPendingDrawPrompt)
      {
        this.showedPendingDrawPrompt = true;

        if (game.ProposedDrawPlayer.Id == this.Current.LoggedInPlayerId())
        {
          await this.PromptPendingDraw();
        }
        else
        {
          await this.PromptAcceptDraw(game.ProposedDrawPlayer);
        }
      }
    }

    private bool FlipBoard()
    {
      return this.LoggedInPlayerColor() == Color.Black;
    }

    private Color LoggedInPlayerColor()
    {
      return this.Current.LoggedInPlayerId() == this.GameState.Value.GameModel?.Game.BlackPlayer.Id
        ? Color.Black
        : Color.Red;
    }

    private async Task PromptPendingInvite()
    {
      var parameters = new DialogParameters
      {
        [nameof(InfoDialog.Title)] = "Pending invite",
        [nameof(InfoDialog.Content)] = "Your opponent hasn't accepted (or declined) this game yet.",
      };

      var options = new DialogOptions { CloseButton = false, DisableBackdropClick = true };
      var dialog = this.DialogService.Show<InfoDialog>(string.Empty, parameters, options);

      await dialog.Result;
    }

    private async Task PromptAcceptGame(int daysPerMove)
    {
      var parameters = new DialogParameters
      {
        [nameof(YesNoDialog.Title)] = "Accept game",
        [nameof(YesNoDialog.Content)] = $"Do you accept this game with {daysPerMove} days thinking time per move?",
        [nameof(YesNoDialog.YesText)] = "Accept",
        [nameof(YesNoDialog.NoText)] = "Decline",
      };

      var options = new DialogOptions { CloseButton = false, DisableBackdropClick = true };
      var dialog = this.DialogService.Show<YesNoDialog>(string.Empty, parameters, options);

      var result = await dialog.Result;

      if (result.Cancelled)
      {
        this.Dispatcher.Dispatch(new DeclineGameAction(this.GameState.Value.GameModel!, this.Current.LoggedInPlayer()));
        this.Snackbar.Add("The game is declined (and removed).", Severity.Success);
        this.NavigationManager.NavigateTo("/games");
      }
      else
      {
        this.Dispatcher.Dispatch(new AcceptGameAction(this.GameState.Value.GameModel!, this.Current.LoggedInPlayer()));
        this.Snackbar.Add("The game has started, good luck!", Severity.Success);
      }
    }

    private async Task Forfeit()
    {
      var parameters = new DialogParameters
      {
        [nameof(YesNoDialog.Title)] = "Forfeit",
        [nameof(YesNoDialog.Content)] = "Are you sure you want to forfeit this game?",
        [nameof(YesNoDialog.InverseButtonColors)] = true,
      };

      var dialog = this.DialogService.Show<YesNoDialog>(string.Empty, parameters);
      var result = await dialog.Result;

      if (!result.Cancelled)
      {
        this.Snackbar.Add("Successfully forfeited the game.", Severity.Success);

        this.Dispatcher.Dispatch(new ForfeitAction(this.GameState.Value.GameModel!, this.Current.LoggedInPlayer()));
      }
    }

    private async Task ProposeDraw()
    {
      var parameters = new DialogParameters
      {
        [nameof(YesNoDialog.Title)] = "Propose draw",
        [nameof(YesNoDialog.Content)] = "Are you sure you want to propose a draw?",
        [nameof(YesNoDialog.InverseButtonColors)] = true,
      };

      var dialog = this.DialogService.Show<YesNoDialog>(string.Empty, parameters);
      var result = await dialog.Result;

      if (!result.Cancelled)
      {
        this.showedPendingDrawPrompt = true;

        this.Snackbar.Add("Draw proposal sent.", Severity.Success);

        this.Dispatcher.Dispatch(new ProposeDrawAction(this.GameState.Value.GameModel!, this.Current.LoggedInPlayer()));
      }
    }

    private async Task PromptPendingDraw()
    {
      var parameters = new DialogParameters
      {
        [nameof(InfoDialog.Title)] = "Pending draw proposal",
        [nameof(InfoDialog.Content)] = "Your opponent hasn't accepted (or declined) your draw proposal yet.",
      };

      var options = new DialogOptions { CloseButton = false, DisableBackdropClick = true };
      var dialog = this.DialogService.Show<InfoDialog>(string.Empty, parameters, options);

      await dialog.Result;
    }

    private async Task PromptAcceptDraw(Player proposedDrawPlayer)
    {
      var parameters = new DialogParameters
      {
        [nameof(YesNoDialog.Title)] = "Accept draw?",
        [nameof(YesNoDialog.Content)] = $"{proposedDrawPlayer.Name} proposed a draw. Do you want to accept it?",
      };

      var options = new DialogOptions { CloseButton = false, DisableBackdropClick = true };

      var dialog = this.DialogService.Show<YesNoDialog>(string.Empty, parameters, options);
      var result = await dialog.Result;
      var accepted = !result.Cancelled;

      this.Dispatcher.Dispatch(new HandleDrawProposalAction(accepted, this.GameState.Value.GameModel!, this.Current.LoggedInPlayer()));
      this.Snackbar.Add($"The draw proposal was {(accepted ? "accepted" : "rejected")}.", accepted ? Severity.Success : Severity.Normal);
    }
  }
}