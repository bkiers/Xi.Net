namespace Xi.Models.Game
{
  using System;

  public class Player
  {
    public Player(int id, string name, string email, int eloRating, bool showPossibleMoves, bool isAdmin)
    {
      this.Id = id;
      this.Name = name;
      this.Email = email;
      this.EloRating = eloRating;
      this.ShowPossibleMoves = showPossibleMoves;
      this.IsAdmin = isAdmin;
    }

    public int Id { get; }

    public string Name { get; }

    public string Email { get; }

    public int EloRating { get; protected set; }

    public bool ShowPossibleMoves { get; }

    public bool IsAdmin { get; }

    public int ProcessEloPoints(Player lost, bool isDraw)
    {
      const int k = 32;

      var eloDifference = lost.EloRating - this.EloRating;
      var percentage = 1.0 / (1.0 + Math.Pow(10, eloDifference / 400.0));

      var change = isDraw
        ? (int)Math.Round(k * (0.5 - percentage))
        : (int)Math.Round(k * (1.0 - percentage));

      this.EloRating += change;
      lost.EloRating -= change;

      return change;
    }
  }
}