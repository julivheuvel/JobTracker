#pragma warning disable CS8618
namespace JobTracker.Models;

public class ParentViewModel
{

    // ================
    // User
    // ================
    public User User { get; set; }
    public List<User> AllUsers { get; set; }
    
    // ================
    // Job
    // ================
    public Job Job { get; set; }
    public List<Job> AllJobs { get; set; }
    
    // ================
    // Interview
    // ================
    public Interview Interview { get; set; }
    public List<Interview> AllInterviews { get; set; }
    
}