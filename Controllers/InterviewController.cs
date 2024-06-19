using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using JobTracker.Models;

public class InterviewController : Controller
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
    public InterviewController(JobTrackerContext context)
    {
        db = context;
    }

    // ================
    // Interview Dashboard
    // ================
    [HttpGet("/interview/dashboard")]    
    public IActionResult InterviewDashboard()
    {
        if(!loggedIn || uid == null)
        {
            return RedirectToAction("Welcome", "User");
        }


        ParentViewModel allModels = new ParentViewModel
        {
            AllJobs = db.Jobs
                .OrderByDescending(j => j.CreatedAt)
                .ToList(),
            AllInterviews = db.Interviews
                .OrderByDescending(i => i.CreatedAt)
                .ToList()
        };

        return View("InterviewDashboard", allModels);
    }
    
    // ================
    // New Interview Render
    // ================
    [HttpGet("/interview/new/{jobId}")]
    public IActionResult NewInterview(int jobId)
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

        ParentViewModel allModels = new ParentViewModel
        {
            Job = job,
            AllInterviews = db.Interviews
                .OrderByDescending(i => i.CreatedAt)
                .ToList()
        };


        return View("NewInterview", allModels);
    }

    // ================
    // New Interview Post
    // ================
    [HttpPost("/interview/create/{jobId}")]
    public IActionResult CreateInterview(Interview newInterview, int jobId)
    {
        if(!loggedIn || uid == null)
        {
            return RedirectToAction("Index", "Users");
        }

        // find the id
        Job? job = db.Jobs.FirstOrDefault(p => p.JobId == jobId);
        // if id not found
        if(job == null)
        {
            return RedirectToAction("Dashboard");
        }

        
        if(!ModelState.IsValid)
        {
            Console.WriteLine("======================");
            Console.WriteLine("Invalid!!!!");
            Console.WriteLine("======================");

            return NewInterview(job.JobId);
            }

        newInterview.JobId = jobId;

        db.Interviews.Add(newInterview);
        db.SaveChanges();

        ParentViewModel allModels = new ParentViewModel
        {
            AllJobs = db.Jobs
                .OrderByDescending(j => j.CreatedAt)
                .ToList(),
            AllInterviews = db.Interviews
                .OrderByDescending(i => i.CreatedAt)
                .ToList()
        };
        return RedirectToAction("InterviewDashboard", allModels);
    }



}