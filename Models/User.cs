#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobTracker.Models;

public class User
{
    // ================
    // User ID
    // ================
    [Key]
    public int UserId { get; set; }

    // ================
    // FirstName
    // ================
    [Required(ErrorMessage = "is required.")]
    [Display(Name="First Name")]
    [MaxLength(10, ErrorMessage ="must be less than 10 characters")]
    public string FirstName { get; set; }
    
    // ================
    // LastName
    // ================
    [Required(ErrorMessage = "is required.")]
    [Display(Name="Last Name")]
    [MinLength(2, ErrorMessage ="must be at least 2 characters long")]
    public string LastName { get; set; }

    // ================
    // Email
    // ================
    [Required(ErrorMessage = "is required.")]
    [UniqueEmail]
    [Display(Name="Email Address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    // ================
    // Password
    // ================
    [Required(ErrorMessage = "is required.")]
    [Display(Name="Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    // ================
    // ConfirmPassword
    // ================
    [NotMapped]
    [Display(Name="Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "passwords must match")]
    public string ConfirmPassword { get; set; }

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
    // Job - OneToMany
    // ================    
    public List<Job> JobPosts { get; set; } = new List<Job>();    


}







