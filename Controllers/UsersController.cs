using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TickItBugTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace NewCSharpBeltExam.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public UsersController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("signin")]
    public IActionResult Signin()
    {
        return View();
    }

    [HttpPost("users/create")]
    public IActionResult CreateUser(User newUser)
    {
        if(ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            return RedirectToAction("Dashboard", "Home");
        } else {
            return View("Signin");
        }
    }

    [HttpPost("users/login")]
    public IActionResult Login(LoginUser userSubmission)
    {
    if(ModelState.IsValid)
        { 
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.LEmail);
            if(userInDb == null)
            {
                ModelState.AddModelError("LEmail", "Invalid Email/Password");
                return View("Index");
            }
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LPassword);
            if(result == 0)
            {
                ModelState.AddModelError("LEmail", "Invalid Email/Password");
                return View("Signin");
            }
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                HttpContext.Session.SetString("FirstName", userInDb.FirstName);
                HttpContext.Session.SetString("LastName", userInDb.LastName);
                return RedirectToAction("Dashboard", "Home");
        } else {
            return View("Signin");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
