using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDemoForAgileworks.Models;

namespace WebApplicationDemoForAgileworks.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(TicketContext context)
        {
            
            if (context.SupportTickets.Any())
            {
                return;
            }

            var tickets = new SupportTicket[]
            {
                new SupportTicket("Test", DateTime.Now.AddDays(1).AddHours(4)),
                new SupportTicket("Fix computer nr4", DateTime.Now.AddDays(1)),
                new SupportTicket("Test2", DateTime.Now.AddMonths(10).AddHours(8)),
                new SupportTicket(){Description="tere", DueDate=DateTime.Now.AddDays(-1), EntryDate=DateTime.Now}


            };
            foreach (SupportTicket s in tickets)
            {
                context.SupportTickets.Add(s);
            }
            context.SaveChanges();

          
        }
    }
}
