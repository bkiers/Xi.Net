namespace Xi.BlazorApp.EventHandlers
{
  using System.Threading.Tasks;
  using JKang.EventBus;
  using Microsoft.Extensions.Logging;
  using Xi.BlazorApp.Models;
  using Xi.BlazorApp.Services;

  public class DeclineDrawProposalEventHandler : IEventHandler<DeclineDrawProposalEventHandler.Event>
  {
    private readonly IEmailService emailService;
    private readonly ILogger<DeclineDrawProposalEventHandler> logger;

    public DeclineDrawProposalEventHandler(IEmailService emailService, ILogger<DeclineDrawProposalEventHandler> logger)
    {
      this.emailService = emailService;
      this.logger = logger;
    }

    public Task HandleEventAsync(Event @event)
    {
      this.logger.LogDebug($">>> event: {@event}");

      return this.emailService.Send(EmailTemplateType.DeclineDrawProposal, @event.GameModel.Game.ProposedDrawPlayer!, @event.GameModel);
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