namespace Xi.BlazorApp.EventHandlers
{
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;
  using Xi.Database;
  using Xi.Database.Dtos;

  public class EmailReminderEventHandler : IEventHandler<EmailReminderEventHandler.Event>
  {
    private readonly XiContext db;
    private readonly IEmailService emailService;
    private readonly ILogger<EmailReminderEventHandler> logger;

    public EmailReminderEventHandler(XiContext db, IEmailService emailService, ILogger<EmailReminderEventHandler> logger)
    {
      this.db = db;
      this.emailService = emailService;
      this.logger = logger;
    }

    public async Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> event: {@event}");

      var game = @event.GameModel.Game;
      var turnPLayer = game.TurnPlayer();
      var success = await this.emailService.Send(EmailTemplateType.MoveReminder, turnPLayer, @event.GameModel);

      if (success)
      {
        this.db.Reminders.Add(new ReminderDto { GameId = game.Id, MoveNumber = game.Moves.Count });
        await this.db.SaveChangesAsync();
      }
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