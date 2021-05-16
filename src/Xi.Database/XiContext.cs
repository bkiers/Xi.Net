namespace Xi.Database
{
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore;
  using Xi.Database.Dtos;

  public class XiContext : IdentityDbContext
  {
    public XiContext(DbContextOptions<XiContext> options)
      : base(options)
    {
    }

    public DbSet<GameDto> Games { get; set; } = default!;

    public DbSet<MoveDto> Moves { get; set; } = default!;

    public DbSet<PlayerDto> Players { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // MoveDto
      modelBuilder.Entity<MoveDto>()
        .HasOne(m => m.Game)
        .WithMany(g => g.Moves);

      // GameDto
      modelBuilder.Entity<GameDto>().HasOne(g => g.AcceptedDrawPlayer);
      modelBuilder.Entity<GameDto>().HasOne(g => g.InitiatedPlayer);
      modelBuilder.Entity<GameDto>().HasOne(g => g.InvitedPlayer);
      modelBuilder.Entity<GameDto>().HasOne(g => g.TurnPlayer);
      modelBuilder.Entity<GameDto>().HasOne(g => g.RedPlayer);
      modelBuilder.Entity<GameDto>().HasOne(g => g.BlackPlayer);
      modelBuilder.Entity<GameDto>().HasOne(g => g.WinnerPlayer);
    }
  }
}