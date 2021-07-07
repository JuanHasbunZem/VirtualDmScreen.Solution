using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualDmScreen.Models;

namespace VirtualDmScreen.Controllers
{
  //[Authorize]
  public class CharactersController : Controller
  {
    private readonly VirtualDmScreenContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CharactersController(UserManager<ApplicationUser> userManager, VirtualDmScreenContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userCharacters = _db.Characters.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userCharacters);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    //Should pass in DiceRoll and Message classes?
    public async Task<ActionResult> Create(Character Characters)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Characters.User = currentUser;
      _db.Characters.Add(Characters);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    //Do we want to see Messages and Dicerolls?
    public ActionResult Details(int id)
    {
      var thisCharacters = _db.Characters
          .Include(Characters => Characters.JoinEntities)
          .ThenInclude(join => join.Message)
          .FirstOrDefault(Characters => Characters.CharacterId == id);
      return View(thisCharacters);
    }
    
    public ActionResult Edit(int id)
    {
      var thisCharacters = _db.Characters.FirstOrDefault(Characters => Characters.CharacterId == id);
      return View(thisCharacters);
    }

    [HttpPost]
    public ActionResult Edit(Character Characters)
    {
      _db.Entry(Characters).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisCharacters = _db.Characters.FirstOrDefault(Characters => Characters.CharacterId == id);
      return View(thisCharacters);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCharacters = _db.Characters.FirstOrDefault(Characters => Characters.CharacterId == id);
      _db.Characters.Remove(thisCharacters);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}
