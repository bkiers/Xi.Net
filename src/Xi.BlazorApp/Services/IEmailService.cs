namespace Xi.BlazorApp.Services;

using System.Threading.Tasks;
using Xi.BlazorApp.Models;
using Xi.Models.Game;

public interface IEmailService
{
  Task<bool> Send(EmailTemplateType type, Player toPlayer, GameModel gameModel);
}