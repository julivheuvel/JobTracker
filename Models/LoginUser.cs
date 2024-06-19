#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JobTracker.Models;

[NotMapped]
public class LoginUser
{

    // ================
    // Email
    // ================
    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email")]
    public string LoginEmail { get; set; }


    // ================
    // Password
    // ================
    [Required(ErrorMessage = "Password is required")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string LoginPassword { get; set; }
    
}