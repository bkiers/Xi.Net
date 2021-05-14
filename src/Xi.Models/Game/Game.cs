namespace Xi.Models.Game
{
  using System.Collections.Generic;

  public class Game
  {
    public Game(int id, Player redPlayer, Player blackPlayer)
    {
      this.Id = id;
      this.RedPlayer = redPlayer;
      this.BlackPlayer = blackPlayer;
    }

    public int Id { get; }

    public Player RedPlayer { get; }

    public Player BlackPlayer { get; }

    // TODO
    private readonly List<Board> board = new();
  }
}