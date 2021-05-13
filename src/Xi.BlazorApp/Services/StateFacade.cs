namespace Xi.BlazorApp.Services
{
  using Fluxor;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Stores.Features.Games.Actions.LoadGames;

  public class StateFacade
  {
    private readonly ILogger<StateFacade> logger;
    private readonly IDispatcher dispatcher;

    public StateFacade(ILogger<StateFacade> logger, IDispatcher dispatcher)
    {
      this.logger = logger;
      this.dispatcher = dispatcher;
    }

    public void LoadGames()
    {
      this.logger.LogInformation("Issuing LoadGamesAction...");
      this.dispatcher.Dispatch(new LoadGamesAction());
    }
  }
}