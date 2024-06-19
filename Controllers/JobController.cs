using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using JobTracker.Models;

public class JobController : Controller
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
    // Accesing Database for Manipulation
    // ===========
    private JobTrackerContext db;
    public JobController(JobTrackerContext context)
    {
        db = context;
    }

    // ================
    // Dashboard
    // ================
    [HttpGet("/job/dashboard")]
    public IActionResult Dashboard()
    {
        if(!loggedIn || uid == null)
        {
            return RedirectToAction("Welcome", "User");
        }

        // if(search != null)
        // {
        //     List<Job> filteredJob = db.Jobs
        //         .Include(a => a.Poster)
        //         .Where(a => a..Contains(search)
        //             || a.Name.Contains(search))
        //         .ToList();
        //     return View("Dashboard", filteredJob);
            
        // }

        // ========
        // Get All Job
        // ========
        List<Job> allJob = db.Jobs
            .Include(a => a.Poster)
            .ToList();



        return View("Dashboard", allJob);
    }

    // ================
    // New Job Render
    // ================
    [HttpGet("/job/new")]
    public IActionResult New()
    {
        if(!loggedIn)
        {
            return RedirectToAction("Welcome", "User");
        }
        return View("NewJob");
    }

    // ================
    // New Job Post
    // ================
    [HttpPost("/job/create")]
    public IActionResult CreateJob(Job newJob)
    {
        if(!loggedIn || uid == null)
        {
            return RedirectToAction("Index", "Users");
        }

        
        if(!ModelState.IsValid)
        {
            Console.WriteLine("======================");
            Console.WriteLine("Invalid!!!!");
            Console.WriteLine("======================");

            return New();
            }

        newJob.UserId = (int)uid;

        db.Jobs.Add(newJob);
        db.SaveChanges();
        return RedirectToAction("Dashboard");
    }

    // ================
    // View Job Render
    // ================
    [HttpGet("/job/{jobId}/view")]
    public IActionResult ViewJob(int jobId)
    {

        if(!loggedIn)
        {
            return RedirectToAction("Welcome", "User");
        }

        // find the id
        Job? job = db.Jobs.FirstOrDefault(p => p.JobId == jobId);

        // if id not found
        if(job == null)
        {
            return RedirectToAction("Dashboard");
        }

        return View("ViewJob", job);
    }

    // ================
    // Edit Job Render
    // ================
    [HttpGet("/job/{jobId}/edit")]
    public IActionResult EditJob(int jobId)
    {

        if(!loggedIn)
        {
            return RedirectToAction("Welcome", "User");
        }

        // find the id
        Job? job = db.Jobs.FirstOrDefault(p => p.JobId == jobId);

        // if id not found
        if(job == null)
        {
            return RedirectToAction("Dashboard");
        }

        return View("EditJob", job);
    }

    // =======================
    // Edit Job Post
    // =======================
    [HttpPost("/job/{jobId}/update")]
    public IActionResult UpdateJob(Job editedJob, int jobId)
    {
        if(!loggedIn)
        {
            return RedirectToAction("Welcome", "User");
        }   
        
        if(!ModelState.IsValid)
        {   
            return EditJob(jobId);
        }

        // does the job exist?
        Job? dbJob = db.Jobs.FirstOrDefault(p => p.JobId == jobId);
        if(dbJob == null)
        {   
            return RedirectToAction("Dashboard");
        }

        dbJob.Name = editedJob.Name;
        dbJob.Company = editedJob.Company;
        dbJob.Location = editedJob.Location;
        dbJob.Description = editedJob.Description;
        dbJob.SalaryMin = editedJob.SalaryMin;
        dbJob.SalaryMax = editedJob.SalaryMax;
        dbJob.ApplicationDate = editedJob.ApplicationDate;
        dbJob.Link = editedJob.Link;
        dbJob.Travel = editedJob.Travel;
        dbJob.Team = editedJob.Team;
        dbJob.Applied = editedJob.Applied;
        dbJob.SalaryOffer = editedJob.SalaryOffer;
        dbJob.Accepted = editedJob.Accepted;
        dbJob.Denied = editedJob.Denied;
        dbJob.Benefits = editedJob.Benefits;
        dbJob.UpdatedAt = DateTime.Now;

        db.Jobs.Update(dbJob);
        Console.WriteLine("====***********************=====");
        db.SaveChanges();


        return RedirectToAction("Dashboard");
    }

    // =======================
    // Delete Job
    // =======================
    [HttpPost("/job/{deletedJobId}/delete")]
    public IActionResult DeleteJob(int deletedJobId)
    {
        if(!loggedIn) {
            return RedirectToAction("Welcome", "User");
        }

        Job? job = db.Jobs.FirstOrDefault(a => a.JobId == deletedJobId);

        if(job != null)
        {
            db.Jobs.Remove(job);
            db.SaveChanges();
        }

        return RedirectToAction("Dashboard");

    }


}