using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationDemoForAgileworks.Models;

namespace WebApplicationDemoForAgileworks.DAL
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {

        }

        public DbSet<SupportTicket> SupportTickets { get; set; }

    }
}
