using System.Collections.Generic;

namespace VirtualDmScreen.Models
{
  public class Player
  {
    public int PlayerId { get; set; }
    public string Name { get; set; }
    public bool IsDm { get; set; }
    public string ImageUrl { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
    public virtual ICollection<DiceRoll> DiceRolls { get; set; }
    public Player()
    {
      this.Messages = new HashSet<Message>();
      this.DiceRolls = new HashSet<DiceRoll>();
    }
  }
}