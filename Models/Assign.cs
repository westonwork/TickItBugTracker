#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace TickItBugTracker.Models;
public class Assign
{
    [Key]
    public int AssignId {get;set;}

    // 1 User to Many Tickets
    public int UserId {get;set;}
    public User? User {get;set;}

    // Many Users Are Assigned TO Many Tickets 
    // And Many Tickets Are Assigned BY Many Users
    public int TicketId {get;set;}
    public Ticket? Ticket {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
}