namespace Xi.BlazorApp.Services
{
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Components.Authorization;
  using Xi.BlazorApp.Extensions;
  using Xi.Models.Game;

  public class Current
  {
    private readonly AuthenticationStateProvider authenticationStateProvider;
    private readonly IPlayerService playerService;

    public Current(AuthenticationStateProvider authenticationStateProvider, IPlayerService playerService)
    {
      this.authenticationStateProvider = authenticationStateProvider;
      this.playerService = playerService;
    }

    public int LoggedInPLayerId()
    {
      return this.LoggedInPLayer().Id;
    }

    public Player LoggedInPLayer()
    {
      var task = Task.Run(async () => await this.LoggedInPLayerAsync());

      return task.Result;
    }

    public async Task<Player> LoggedInPLayerAsync()
    {
      var state = await this.authenticationStateProvider.GetAuthenticationStateAsync();

      var email = state.Get("emailaddress", false);
      var name = state.Get("name");

      return this.playerService.FindByEmailOrCreate(email, name);
    }
  }
}