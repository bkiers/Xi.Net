namespace Xi.Models.Game
{
  using System.Collections.Generic;

  public class Game
  {
    public Game(int id, Player redPlayer, Player blackPlayer, Player turnPlayer)
    {
      this.Id = id;
      this.RedPlayer = redPlayer;
      this.BlackPlayer = blackPlayer;
      this.TurnPlayer = turnPlayer;
    }

    public int Id { get; }

    public Player RedPlayer { get; }

    public Player BlackPlayer { get; }

    public Player TurnPlayer { get; }

    // TODO
    private readonly List<Board> board = new();


    public Board CurrentBoard()
    {
      // TODO
      return new Board();
    }


    public Color TurnPlayerColor()
    {
      return this.TurnPlayer.Id == this.RedPlayer.Id ? Color.Red : Color.Black;
    }
  }
}