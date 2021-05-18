namespace Xi.BlazorApp.Stores.Features.LoggedInPlayer.Effects
{
  using System.Threading.Tasks;
  using Fluxor;
  using Microsoft.AspNetCore.Components.Authorization;
  using Xi.BlazorApp.Extensions;
  using Xi.BlazorApp.Services;
  using Xi.BlazorApp.Stores.Features.LoggedInPlayer.Actions.ReadLoggedInPlayerFromCookie;

  public class ReadLoggedInPlayerFromCookieEffect : Effect<ReadLoggedInPlayerFromCookieAction>
  {
    private readonly AuthenticationStateProvider authenticationStateProvider;
    private readonly IPlayerService playerService;

    public ReadLoggedInPlayerFromCookieEffect(AuthenticationStateProvider authenticationStateProvider, IPlayerService playerService)
    {
      this.authenticationStateProvider = authenticationStateProvider;
      this.playerService = playerService;
    }

    public override async Task HandleAsync(ReadLoggedInPlayerFromCookieAction action, IDispatcher dispatcher)
    {
      var state = await this.authenticationStateProvider.GetAuthenticationStateAsync();

      var email = state.Get("emailaddress", false);
      var name = state.Get("name");

      // Fetch the existing player from the database, or create a new one.
      var player = this.playerService.FindByEmailOrCreate(email, name);

      dispatcher.Dispatch(new ReadLoggedInPlayerFromCookieSuccessAction(player));
    }
  }
}