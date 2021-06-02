namespace Xi.BlazorApp.EventHandlers
{
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;

  public class AcceptNewGameEventHandler : IEventHandler<AcceptNewGameEventHandler.Event>
  {
    private readonly IEmailService emailService;
    private readonly ILogger<AcceptNewGameEventHandler> logger;

    public AcceptNewGameEventHandler(IEmailService emailService, ILogger<AcceptNewGameEventHandler> logger)
    {
      this.emailService = emailService;
      this.logger = logger;
    }

    public Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> event: {@event}");

      return this.emailService.Send(EmailTemplateType.AcceptNewGame, @event.GameModel.Game.InitiatedPlayer, @event.GameModel);
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