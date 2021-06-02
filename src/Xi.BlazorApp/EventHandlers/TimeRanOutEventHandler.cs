namespace Xi.BlazorApp.EventHandlers
{
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;
  using Xi.Models.Game;

  public class TimeRanOutEventHandler : IEventHandler<TimeRanOutEventHandler.Event>
  {
    private readonly IGameService gameService;
    private readonly ILogger<TimeRanOutEventHandler> logger;

    public TimeRanOutEventHandler(IGameService gameService, ILogger<TimeRanOutEventHandler> logger)
    {
      this.gameService = gameService;
      this.logger = logger;
    }

    public Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> event: {@event}");

      this.gameService.EndGame(
        @event.GameModel.Game.Id,
        @event.GameModel.OpponentOf(@event.GameModel.Game.TurnPlayer()).Id,
        GameResultType.TimeUp);

      return Task.CompletedTask;
    }

    public class Event : GameEvent
    {
      public Event(GameModel gameModel)
        : base(gameModel)
      {
      }
    }
  }
}