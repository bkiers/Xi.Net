namespace Xi.Models.Game
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public class Game
  {
    private readonly List<Board> boards;

    public Game(
      int id,
      Player redPlayer,
      Player blackPlayer,
      Player initiatedPlayer,
      Player invitedPlayer,
      Player? winnerPlayer,
      GameResultType? gameResultType,
      int secondsPerMove,
      DateTime? clockRunsOutAt,
      bool accepted,
      IEnumerable<Move> moves,
      IEnumerable<Reminder> reminders)
    {
      this.boards = new List<Board>();
      this.Id = id;
      this.RedPlayer = redPlayer;
      this.BlackPlayer = blackPlayer;
      this.InitiatedPlayer = initiatedPlayer;
      this.InvitedPlayer = invitedPlayer;
      this.WinnerPlayer = winnerPlayer;
      this.GameResultType = gameResultType;
      this.SecondsPerMove = secondsPerMove;
      this.ClockRunsOutAt = clockRunsOutAt;
      this.Accepted = accepted;
      this.Moves = moves.ToList();
      this.Reminders = reminders.ToList();

      this.ReplayMoves();
    }

    public int Id { get; }

    public Player RedPlayer { get; }

    public Player BlackPlayer { get; }

    public Player InitiatedPlayer { get; }

    public Player InvitedPlayer { get; }

    public Player? WinnerPlayer { get; }

    public GameResultType? GameResultType { get; }

    public int SecondsPerMove { get; }

    public DateTime? ClockRunsOutAt { get; }

    public bool Accepted { get; }

    public List<Move> Moves { get; }

    public List<Reminder> Reminders { get; }

    public Board Board(int boardIndex)
    {
      return this.boards[boardIndex];
    }

    public bool IsOver()
    {
      return this.GameResultType.HasValue;
    }

    public Board CurrentBoard()
    {
      return this.boards.Last();
    }

    public Color TurnPlayerColor()
    {
      var isBlackTurn = this.Moves.Count % 2 == 1;

      return isBlackTurn ? Color.Black : Color.Red;
    }

    public Player TurnPlayer()
    {
      return this.TurnPlayerColor() == Color.Red ? this.RedPlayer : this.BlackPlayer;
    }

    public Game RemoveMovesFrom(int index)
    {
      return new(
        this.Id,
        this.RedPlayer,
        this.BlackPlayer,
        this.InitiatedPlayer,
        this.InvitedPlayer,
        this.WinnerPlayer,
        this.GameResultType,
        this.SecondsPerMove,
        this.ClockRunsOutAt,
        this.Accepted,
        this.Moves.GetRange(0, index),
        this.Reminders);
    }

    public void Move(Move move, bool appendToMoves = true)
    {
      var moveResult = this.boards.Last().Move(move);

      if (appendToMoves)
      {
        this.Moves.Add(move);
      }

      // For each move, add a new board state.
      this.boards.Add(moveResult.Board);
    }

    public ISet<Cell> ValidToCells(Cell fromCell)
    {
      return this.boards.Last().ValidToCells(fromCell);
    }

    public Player? CheckPlayer()
    {
      if (this.CurrentBoard().IsCheck(Color.Red))
      {
        return this.RedPlayer;
      }

      return this.CurrentBoard().IsCheck(Color.Black) ? this.BlackPlayer : null;
    }

    public Player? CheckmatePlayer()
    {
      if (this.CurrentBoard().IsCheckmate(Color.Red))
      {
        return this.RedPlayer;
      }

      return this.CurrentBoard().IsCheckmate(Color.Black) ? this.BlackPlayer : null;
    }

    public Player? StalematePlayer()
    {
      if (this.CurrentBoard().IsStalemate(Color.Red))
      {
        return this.RedPlayer;
      }

      return this.CurrentBoard().IsStalemate(Color.Black) ? this.BlackPlayer : null;
    }

    public Color PlayingWithColor(int playerId)
    {
      return this.RedPlayer.Id == playerId ? Color.Red : Color.Black;
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