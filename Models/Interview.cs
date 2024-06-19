#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobTracker.Models;

public class Interview
{
    // ================
    // Interview ID
    // ================
    [Key]
    public int InterviewId { get; set; }

    // ================
    // Name (s)
    // ================
    [Required(ErrorMessage="(s) is required!")]
    public string Name { get; set; }

    // ================
    // Location
    // ================
    public string Location { get; set; }

    // ================
    // Description
    // ================
    public string Description { get; set; }

    // ================
    // FollowUp
    // ================
    public string FollowUp { get; set; }
    
    // ================
    // LengthOfTime
    // ================
    [Required(ErrorMessage="is required!")]
    public double LengthOfTime { get; set; }

    // ================
    // InterviewDate
    // ================
    [Required(ErrorMessage="Interview Date is required!")]
    public DateTime InterviewDate { get; set; }

    // ================
    // CreatedAt
    // ================
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // ================
    // UpdatedAt
    // ================
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //! ===================================
    //! Relationships
    //! ===================================
    // ================
    // Job - One to Many
    // ================
    public int JobId { get; set; }
    public Job? JobContainer { get; set; }

    
}