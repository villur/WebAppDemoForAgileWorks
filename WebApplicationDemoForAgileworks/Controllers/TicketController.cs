using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationDemoForAgileworks.DAL;
using WebApplicationDemoForAgileworks.Models;
using WebApplicationDemoForAgileworks.ViewModel.Ticket;

namespace WebApplicationDemoForAgileworks.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketContext _context;
        public TicketController(TicketContext context)
        {
            
            _context = context;           
        }

        public IActionResult Index()
        {           
            return View(IndexViewModel.Create(_context.SupportTickets.OrderBy(ticket => ticket.DueDate).ToList()));
        }

        [HttpPost]
        public IActionResult AddTicket(string description, DateTime dueDate)
        {

            if (ModelState.IsValid)
            {
                SupportTicket ticket = new SupportTicket(description, dueDate);
                _context.SupportTickets.Add(ticket);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CompleteTicket(int id)
        {
            SupportTicket ticket = _context.SupportTickets.Find(id);

            if (ticket == null)
            {
                return NotFound();
            }

            ticket.MarkDone();
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
