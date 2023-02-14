#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace TickItBugTracker.Models;
public class MyContext : DbContext 
{   
    public MyContext(DbContextOptions options) : base(options) { }    
    public DbSet<User> Users {get;set;}
    public DbSet<Ticket> Tickets {get;set;}
    public DbSet<Assign> Assigns {get;set;}
}