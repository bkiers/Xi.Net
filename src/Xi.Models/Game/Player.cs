namespace Xi.Models.Game
{
  public class Player
  {
    public Player(int id, string name, string email, int eloRating, bool isAdmin)
    {
      this.Id = id;
      this.Name = name;
      this.Email = email;
      this.EloRating = eloRating;
      this.IsAdmin = isAdmin;
    }

    public int Id { get; }

    public string Name { get; }

    public string Email { get; }

    public int EloRating{ get; }

    public bool IsAdmin { get; }
  }
}