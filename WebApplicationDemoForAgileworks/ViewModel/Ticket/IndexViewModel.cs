using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDemoForAgileworks.Models;

namespace WebApplicationDemoForAgileworks.ViewModel.Ticket
{
    public class IndexViewModel
    {
        public List<SupportTicketViewModel> Tickets { get; set; }

        public static IndexViewModel Create(IEnumerable<SupportTicket> supportTickets)
        {
            var ticketList = (from t in supportTickets
                              select new SupportTicketViewModel
                              {
                                  Id = t.Id,
                                  Description = t.Description,
                                  EntryDate = t.EntryDate,
                                  DueDate = t.DueDate,
                                  DoneDate = t.DoneDate
                              }).ToList();

            return new IndexViewModel() { Tickets = ticketList };
        }
    }
}
