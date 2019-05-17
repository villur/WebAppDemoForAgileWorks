using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplicationDemoForAgileworks.DAL;
using WebApplicationDemoForAgileworks.Models;

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
            
            
            try
            {
                return View(_context.SupportTickets.OrderBy(ticket => ticket.DueDate).ToList());              
            }
            catch (Exception)
            {

                return View("DatabaseError");
                
            }


        }


        [HttpPost]
        public IActionResult AddTicket(string description, DateTime dueDate)
        {
            SupportTicket ticket;
            try
            {
                ticket = new SupportTicket(description, dueDate);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { state = "AddFailed" });

            }

            try
            {
                _context.SupportTickets.Add(ticket);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return View("DatabaseError");
            }

            
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult CompleteTicket(int id)
        {
            SupportTicket ticket;
            if (id > 0 && _context?.SupportTickets.Find(id) != null)
            {
                ticket = _context.SupportTickets.Find(id);
                ticket.MarkDone();
            }
            else
            {
                return RedirectToAction("Index", new { state = "CompleteFailed" });
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return View("DatabaseError");
            }
           
            
            return RedirectToAction("Index");
        }

     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
