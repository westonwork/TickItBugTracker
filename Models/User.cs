#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TickItBugTracker.Models;
public class User
{
    [Key]
    public int UserId {get;set;}
    [Required]
    [Display(Name = "First Name")]
    [MinLength (2, ErrorMessage = "First Name must be at least 2 characters")]
    public string FirstName {get;set;} 
    [Required]
    [Display(Name = "Last Name")]
    [MinLength (2, ErrorMessage = "Last Name must be at least 2 characters")]
    public string LastName {get;set;} 
    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string Email {get;set;}
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    // One to many 
    public List<Ticket> ListOfTicketsFromThisUser { get; set; } = new List<Ticket>();

    // Many to many
    public List<Assign> ListofAssignmentsGivenFromThisUser {get;set;} = new List<Assign>();

    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string Confirm {get;set;}

}

// Custom Validation as each registered user must have a unique email address
public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
        {
            return new ValidationResult("Email is required!");
        }
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        if(_context.Users.Any(e => e.Email == value.ToString()))
        {
            return new ValidationResult("Email must be unique!");
        } else {
            return ValidationResult.Success;
        }
    }
}