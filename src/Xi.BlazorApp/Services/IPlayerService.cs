namespace Xi.BlazorApp.Services;

using System.Collections.Generic;
using Xi.Models;
using Xi.Models.Game;

public interface IPlayerService
{
  List<Player> AllPlayers();

  List<Player> AllPlayersExcept(int playerId);

  Player? FindByEmail(string email);

  Player FindById(int playerId);

  Player Update(int playerId, Settings settings);

  bool DidSomething(int? playerId);
}