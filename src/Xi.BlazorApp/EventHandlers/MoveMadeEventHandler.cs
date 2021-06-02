namespace Xi.BlazorApp.EventHandlers
{
  using System;
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;
  using Xi.Models.Game;

  public class MoveMadeEventHandler : IEventHandler<MoveMadeEventHandler.Event>
  {
    private readonly IEmailService emailService;
    private readonly ILogger<MoveMadeEventHandler> logger;

    public MoveMadeEventHandler(IEmailService emailService, ILogger<MoveMadeEventHandler> logger)
    {
      this.emailService = emailService;
      this.logger = logger;
    }

    public Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> event: {@event}");

      return this.emailService.Send(EmailTemplateType.MoveMade, @event.GameModel.Game.TurnPlayer(), @event.GameModel);
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