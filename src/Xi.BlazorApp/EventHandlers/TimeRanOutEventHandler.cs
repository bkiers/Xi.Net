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
    private readonly IEmailService emailService;
    private readonly IGameService gameService;
    private readonly ILogger<TimeRanOutEventHandler> logger;

    public TimeRanOutEventHandler(IEmailService emailService, IGameService gameService, ILogger<TimeRanOutEventHandler> logger)
    {
      this.emailService = emailService;
      this.gameService = gameService;
      this.logger = logger;
    }

    public Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> event: {@event}");

      var model = @event.GameModel;
      var game = model.Game;

      this.gameService.EndGame(game.Id, model.OpponentOf(game.TurnPlayer()).Id, GameResultType.TimeUp);
      this.emailService.Send(EmailTemplateType.TimeRanOut, game.TurnPlayer(), model);

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