namespace Xi.BlazorApp.Services
{
  using System;
  using System.Dynamic;
  using System.IO;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using SendGrid;
  using SendGrid.Helpers.Mail;
  using Xi.BlazorApp.Config;
  using Xi.BlazorApp.Models;
  using Xi.Models.Extensions;
  using Xi.Models.Game;

  public class EmailService : IEmailService
  {
    private readonly XiConfig config;
    private readonly ILogger<EmailService> logger;

    public EmailService(IOptions<XiConfig> options, ILogger<EmailService> logger)
    {
      this.config = options.Value;
      this.logger = logger;
    }

    public async Task<bool> Send(EmailTemplateType type, Player toPlayer, GameModel gameModel)
    {
      var templatePath = Path.Combine("EmailTemplates", $"{type}.mt");
      var templateSource = File.ReadAllText(templatePath);
      var template = Mustachio.Parser.Parse(templateSource);

      var memberInfos = type.GetType().GetMember(type.ToString());
      var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == type.GetType());
      var valueAttributes = enumValueMemberInfo!.GetCustomAttributes(typeof(SubjectAttribute), false);
      var model = this.CreateTemplateMode(toPlayer, gameModel);

      var subject = ((SubjectAttribute)valueAttributes[0]).Text;
      var htmlContent = template(model);
      var plainTextContent = Regex.Replace(htmlContent, @"<[\s\S]*?>[ \t]*[\r\n]?", string.Empty);

      this.logger.LogDebug($"subject     --> {subject}");
      this.logger.LogDebug($"htmlContent --> {htmlContent}");
      this.logger.LogDebug($"htmlContent --> {plainTextContent}");

      if (this.config.EmailEnabled)
      {
        return await this.ReallySend(toPlayer, subject, plainTextContent, htmlContent);
      }

      return true;
    }

    private ExpandoObject CreateTemplateMode(Player player, GameModel gameModel)
    {
      dynamic model = new ExpandoObject();

      model.gameId = gameModel.Game.Id;
      model.playerName = player.Name;
      model.opponenName = gameModel.OpponentOf(player).Name;
      model.gameUrl = $"{this.config.BaseUri}/games/{gameModel.Game.Id}";
      model.newGameUrl = $"{this.config.BaseUri}/games/new";
      model.clockRunsOutAt = gameModel.Game.ClockRunsOutAt.ToStringNL("dddd dd MMMM HH:mm");

      return model;
    }

    private async Task<bool> ReallySend(Player toPlayer, string subject, string plainTextContent, string htmlContent)
    {
      var client = new SendGridClient(this.config.SendGridApiKey!);

      var from = new EmailAddress(this.config.ReplyToAddress);
      var to = new EmailAddress(toPlayer.Email, toPlayer.Name);
      var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

      var response = await client.SendEmailAsync(msg);

      return response.IsSuccessStatusCode;
    }
  }
}