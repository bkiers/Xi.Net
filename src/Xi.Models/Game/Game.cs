namespace Xi.Models.Game
{
  using System.Collections.Generic;

  public class Game
  {
    public Game(int id)
    {
      this.Id = id;
    }

    public int Id { get; set; }

    private readonly List<Board> board = new();

    // TODO: 2 players
  }
}