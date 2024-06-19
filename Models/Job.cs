#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobTracker.Models;

public class Job
{
    // ================
    // Job ID
    // ================
    [Key]
    public int JobId { get; set; }

    // ================
    // Name
    // ================
    [Required(ErrorMessage=" is required!")]
    public string Name { get; set; }
    
    // ================
    // Company
    // ================
    [Required(ErrorMessage=" is required!")]
    public string Company { get; set; }
    
    // ================
    // Location
    // ================
    [Required(ErrorMessage=" is required!")]
    public string Location { get; set; }

    // ================
    // Description
    // ================
    public string Description { get; set; }
    
    // ================
    // SalaryMin
    // ================
    [Required(ErrorMessage="Minimum Salary is required!")]
    public double SalaryMin { get; set; }
    
    // ================
    // SalaryMax
    // ================
    public double SalaryMax { get; set; }

    // ================
    // ApplicationDate
    // ================
    [Required(ErrorMessage="Application Date is required!")]
    public DateTime ApplicationDate { get; set; }

    // ================
    // Link
    // ================
    public string Link { get; set; }

    // ================
    // Travel
    // ================
    public string Travel { get; set; }

    // ================
    // Team
    // ================
    public string Team { get; set; }

    // ================
    // Applied
    // ================
    public bool Applied { get; set; } = false;

    // ================
    // JobOffer
    // ================
    public bool JobOffer { get; set; } = false;

    // ================
    // SalaryOffer
    // ================
    public double SalaryOffer { get; set; }

    // ================
    // Accepted
    // ================
    public bool Accepted { get; set; } = false;

    // ================
    // Denied
    // ================
    public bool Denied { get; set; } = false;
    
    // ================
    // Benefits
    // ================
    public string Benefits { get; set; }
    

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
    // User - One to Many
    // ================
    public int UserId { get; set; }
    public User? Poster { get; set; }

    // ================
    // Interview - One to Many
    // ================
    public List<Interview> IntviewCollection { get; set; } = new List<Interview>();
    
    
}