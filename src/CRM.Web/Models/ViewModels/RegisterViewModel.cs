using System.ComponentModel.DataAnnotations;

namespace CRM.Web.Models.ViewModels;

public class RegisterViewModel
{
   [Required(ErrorMessage = "First name is required")]
   [Display(Name = "First Name")]
   public required string FirstName { get; set; }

   [Required(ErrorMessage = "Last name is required")]
   [Display(Name = "Last Name")]
   public required string LastName { get; set; }

   [Required(ErrorMessage = "Email is required")]
   [EmailAddress(ErrorMessage = "Please enter a valid email address")]
   [Display(Name = "Email")]
   public required string Email { get; set; }

   [Required(ErrorMessage = "Password is required")]
   [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 8)]
   [DataType(DataType.Password)]
   [Display(Name = "Password")]
   public required string Password { get; set; }

   [DataType(DataType.Password)]
   [Display(Name = "Confirm Password")]
   [Compare("Password", ErrorMessage = "Passwords do not match")]
   public required string ConfirmPassword { get; set; }

   [Required(ErrorMessage = "Position is required")]
   [Display(Name = "Position")]
   public int PositionId { get; set; }
}