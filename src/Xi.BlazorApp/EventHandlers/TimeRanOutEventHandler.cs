namespace Xi.BlazorApp.EventHandlers
{
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;

  public class TimeRanOutEventHandler : IEventHandler<TimeRanOutEventHandler.Event>
  {
    private readonly ILogger<TimeRanOutEventHandler> logger;

    public TimeRanOutEventHandler(ILogger<TimeRanOutEventHandler> logger)
    {
      this.logger = logger;
    }

    public Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> event: {@event}");

      return Task.CompletedTask;
    }

    public class Event
    {
      public Event(GameModel gameModel)
      {
        this.GameModel = gameModel;
      }

      public GameModel GameModel { get; }
    }
  }
}