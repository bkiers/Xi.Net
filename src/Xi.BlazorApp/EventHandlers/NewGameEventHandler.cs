namespace Xi.BlazorApp.EventHandlers
{
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;

  public class NewGameEventHandler : IEventHandler<NewGameEventHandler.Event>
  {
    private readonly IEmailService emailService;
    private readonly ILogger<NewGameEventHandler> logger;

    public NewGameEventHandler(IEmailService emailService, ILogger<NewGameEventHandler> logger)
    {
      this.emailService = emailService;
      this.logger = logger;
    }

    public Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> event: {@event}");

      return this.emailService.Send(EmailTemplateType.NewGame, @event.GameModel.Game.InvitedPlayer, @event.GameModel);
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