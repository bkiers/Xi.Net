namespace Xi.Models.Game;

public class Reminder
{
  public Reminder(int moveNumber)
  {
    this.MoveNumber = moveNumber;
  }

  public int MoveNumber { get; }
}