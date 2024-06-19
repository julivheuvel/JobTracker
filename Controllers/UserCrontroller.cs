using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using JobTracker.Models;

public class UserController : Controller
{
    // ===========
    // Getting User ID
    // ===========
    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("UUID");
        }
    }

    // ===========
    // Getting Logged In User
    // ===========
    private bool loggedIn
    {
        get
        {
            return uid != null;
        }
    }


    // ===========
    // Importing Database
    // ===========
    private JobTrackerContext db;
    public UserController(JobTrackerContext context)
    {
        db = context;
    }

    // =================
    // Welcome
    // =================
    [HttpGet("/")]
    public IActionResult Welcome()
    {   
        return View("Welcome");
    }

    // =================
    // GET REGISTER
    // =================
    [HttpGet("/registration")]
    public IActionResult Registration()
    {   
        return View("Register");
    }

    // =================
    // GET LOGIN
    // =================
    [HttpGet("/login")]
    public IActionResult Login()
    {   
        return View("Login");
    }

    // ===================
    // REGISTER
    // ===================
    [HttpPost("/register")]
    public IActionResult Register(User newUser)
    {
        if(ModelState.IsValid)
        {
            if(db.Users.Any(u => u.Email == newUser.Email))
            {
                ModelState.AddModelError("Email", "is taken");
            }
        }

        if(!ModelState.IsValid)
        {
            return Registration();
        }

        PasswordHasher<User> hashBrowns = new PasswordHasher<User>();
        newUser.Password = hashBrowns.HashPassword(newUser, newUser.Password);

        db.Users.Add(newUser);
        db.SaveChanges();

        HttpContext.Session.SetInt32("UUID", newUser.UserId);
        return RedirectToAction("Dashboard", "Job");
    }

    // ===================
    // LOGIN
    // ===================
    [HttpPost("/logging")]
    public IActionResult Logging(LoginUser loginUser)
    {
        
        if(ModelState.IsValid == false)
        {
            return Registration();
        }

        User? dbUser = db.Users.FirstOrDefault(u => u.Email == loginUser.LoginEmail);

        if(dbUser == null)
        {
            ModelState.AddModelError("LoginUser", "Not found");
            return Registration();
        }

        PasswordHasher<LoginUser> hashBorwns = new PasswordHasher<LoginUser>();
        PasswordVerificationResult pwCompare = hashBorwns.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

        // if passwords don't match
        if(pwCompare == 0)
        {
            ModelState.AddModelError("LoginPassword", "invalid password");
            return Registration();
        }

        // no issues
        HttpContext.Session.SetInt32("UUID", dbUser.UserId);
        return RedirectToAction("Dashboard", "Job");
    }
    
    // ===================
    // LOGOUT
    // ===================
    [HttpPost("/logout")]
    public IActionResult Logout()
    {

        HttpContext.Session.Clear();

        return RedirectToAction("Welcome", "User");
    }

}