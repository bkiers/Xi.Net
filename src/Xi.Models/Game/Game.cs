namespace Xi.Models.Game
{
  using System.Collections.Generic;
  using System.Linq;

  public class Game
  {
    private readonly List<Board> boards = default!;

    public Game(int id, Player redPlayer, Player blackPlayer, Player turnPlayer, List<Move> moves)
    {
      this.Id = id;
      this.RedPlayer = redPlayer;
      this.BlackPlayer = blackPlayer;
      this.TurnPlayer = turnPlayer;
      this.Moves = moves;

      this.ReplayMoves();
    }

    public int Id { get; }

    public Player RedPlayer { get; }

    public Player BlackPlayer { get; }

    public Player TurnPlayer { get; }

    public List<Move> Moves { get; }

    public Board CurrentBoard()
    {
      return this.boards.Last();
    }

    public Color TurnPlayerColor()
    {
      var isBlackTurn = this.Moves.Count % 2 == 1;

      return isBlackTurn ? Color.Black : Color.Red;
    }

    public void Move(Move move, bool appendToMoves = true)
    {
      var moveResult = this.CurrentBoard().Move(move);

      if (appendToMoves)
      {
        this.Moves.Add(move);
      }

      // For each move, add a new board state.
      this.boards.Add(moveResult.Board);
    }

    public ISet<Cell> ValidToCells(Cell fromCell)
    {
      return this.CurrentBoard().ValidToCells(fromCell);
    }

    private void ReplayMoves()
    {
      // Add the start board first.
      this.boards.Add(new Board());

      foreach (var move in this.Moves)
      {
        this.Move(move, appendToMoves: false);
      }
    }
  }
}