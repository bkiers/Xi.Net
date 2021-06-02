namespace Xi.BlazorApp.EventHandlers
{
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;

  public class DeclineNewGameEventHandler : IEventHandler<DeclineNewGameEventHandler.Event>
  {
    private readonly IEmailService emailService;
    private readonly ILogger<DeclineNewGameEventHandler> logger;

    public DeclineNewGameEventHandler(IEmailService emailService, ILogger<DeclineNewGameEventHandler> logger)
    {
      this.emailService = emailService;
      this.logger = logger;
    }

    public Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> event: {@event}");

      return this.emailService.Send(EmailTemplateType.DeclineNewGame, @event.GameModel.Game.InitiatedPlayer, @event.GameModel);
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