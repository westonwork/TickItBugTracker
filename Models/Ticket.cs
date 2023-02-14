#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TickItBugTracker.Models;
public class Ticket
{
    [Key]
    public int TicketId {get;set;}
    [Required]
    public string Name {get;set;}
    [Required]
    public string Project {get;set;}
    [Required]
    [DataType(DataType.Date)]
    [Display (Name = "Due Date")]
    [FutureDate]
    public DateTime DueDate {get;set;}
    [Required]
    public string Priority {get;set;}
    [Required]
    public string Image {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    // 1 to many
    public int UserId {get;set;}
    public User? TicketCreator {get;set;}

// Many to many

    public List<Assign> ListofAssignmentsForThisTickets {get;set;} = new List<Assign>();

}

public class FutureDateAttribute : ValidationAttribute
{    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {        
        if(value == null) 
        {
            return new ValidationResult("Must have Date");
        }
        if((DateTime)value < DateTime.Now)
        {
            return new ValidationResult("Due Date must be in the future");
        } else {
            Console.WriteLine(value);
            return ValidationResult.Success;
        }
    }
}