namespace Xi.BlazorApp.EventHandlers;

using System.Threading.Tasks;
using JKang.EventBus;
using Microsoft.Extensions.Logging;
using Xi.BlazorApp.Models;
using Xi.BlazorApp.Services;
using Xi.Models.Game;

public class SendEmailEventHandler : IEventHandler<SendEmailEventHandler.Event>
{
  private readonly IEmailService emailService;
  private readonly ILogger<SendEmailEventHandler> logger;

  public SendEmailEventHandler(IEmailService emailService, ILogger<SendEmailEventHandler> logger)
  {
    this.emailService = emailService;
    this.logger = logger;
  }

  public Task HandleEventAsync(Event @event)
  {
    this.logger.LogDebug($">>> SendEmailEventHandler event: {@event.Template}");

    return this.emailService.Send(@event.Template, @event.ToPlayer, @event.GameModel);
  }

  public class Event
  {
    public Event(EmailTemplateType template, Player toPlayer, GameModel gameModel)
    {
      this.Template = template;
      this.ToPlayer = toPlayer;
      this.GameModel = gameModel;
    }

    public EmailTemplateType Template { get; }

    public Player ToPlayer { get; }

    public GameModel GameModel { get; }
  }
}