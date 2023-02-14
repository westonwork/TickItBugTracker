using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TickItBugTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace NewCSharpBeltExam.Controllers;

public class TicketsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public TicketsController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [SessionCheck]
    [HttpGet("tickets")]
    public IActionResult Tickets()
    {
        List<Ticket>? AllTickets = _context.Tickets
                                            .Include(l => l.ListofAssignmentsForThisTickets)
                                            .Include(u => u.TicketCreator)
                                            .ToList();
        return View(AllTickets);
    }

    [SessionCheck]
    [HttpGet("tickets/my")]
    public IActionResult MyTickets()
    {
        List<Ticket>? AllMyTickets = _context.Tickets
                                            .Include(l => l.ListofAssignmentsForThisTickets)
                                            .Include(u => u.TicketCreator)
                                            .ToList();
        return View(AllMyTickets);
    }

    [SessionCheck]
    [HttpGet("tickets/new")]
    public IActionResult NewTicket()
    {
        return View("NewTicket");
    }

    [HttpPost("tickets/create")]
    public IActionResult CreateTicket(Ticket newTicket)
    {
        if(ModelState.IsValid)
        {
            newTicket.UserId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newTicket);
            _context.SaveChanges();
            return RedirectToAction("Tickets");
        }
        return NewTicket();
    }

    [SessionCheck]
    [HttpGet("tickets/{ticketId}")]
    public IActionResult OneTicket (int ticketId)
    {
        Ticket? oneTicket = _context.Tickets
                                    .Include(l => l.ListofAssignmentsForThisTickets)
                                    .FirstOrDefault(t => t.TicketId == ticketId);
        return View(oneTicket);
    }

    [SessionCheck]
    [HttpGet("tickets/{ticketId}/edit")]
    public IActionResult EditTicket(int ticketId)
    {
        Ticket? TicketToEdit = _context.Tickets.FirstOrDefault(t => t.TicketId == ticketId);
        return View("EditTicket", TicketToEdit);
    }

    [HttpPost("tickets/{ticketId}/update")]
    public IActionResult UpdateTicket(int ticketId, Ticket updatedTicket)
    {
        Ticket? TicketToUpdate = _context.Tickets.FirstOrDefault(t => t.TicketId == ticketId);
        if(TicketToUpdate ==null)
        {
            return RedirectToAction("OneTicket");
        }
        if(ModelState.IsValid)
        {
            TicketToUpdate.Name = updatedTicket.Name;
            TicketToUpdate.Project = updatedTicket.Project;
            TicketToUpdate.DueDate = updatedTicket.DueDate;
            TicketToUpdate.Priority = updatedTicket.Priority;
            TicketToUpdate.Image = TicketToUpdate.Image;
            TicketToUpdate.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Tickets");
        }
        return EditTicket(TicketToUpdate.TicketId);
    }

    [HttpPost("tickets/{ticketId}/destroy")]
    public IActionResult DestroyTicket(int ticketId)
    {
        Ticket? TicketToDelete = _context.Tickets.SingleOrDefault(ticket => ticket.TicketId == ticketId);
        if(TicketToDelete == null)
        {
            return RedirectToAction("Tickets");
        }
        _context.Remove(TicketToDelete);
        _context.SaveChanges();
        return RedirectToAction("Tickets", "Tickets");
    }

    [SessionCheck]
    [HttpPost("assigns/create")]
    public IActionResult CreateAssign(Assign newAssign)
    {
            _context.Add(newAssign);
            _context.SaveChanges();
            return RedirectToAction("Tickets");
    }

    [SessionCheck]
    [HttpPost("assigns/destroy")]
    public IActionResult DestroyAssign(int ticketId, int userId)
    {
        Assign? AssignToDestroy = _context.Assigns.FirstOrDefault(a => a.TicketId == ticketId && a.UserId == userId);
        if(AssignToDestroy == null)
        {
            return RedirectToAction("Tickets");
        } else {
            _context.Remove(AssignToDestroy);
            _context.SaveChanges();
            return RedirectToAction("Tickets");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
