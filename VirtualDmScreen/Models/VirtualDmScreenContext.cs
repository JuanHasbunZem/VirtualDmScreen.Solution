using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace VirtualDmScreen.Models
{
  public class VirtualDmScreenContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<DiceRoll> DiceRolls { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<Player> Players { get; set; }

    public VirtualDmScreenContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}