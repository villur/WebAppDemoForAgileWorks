using NUnit.Framework;
using System;
using WebApplicationDemoForAgileworks.Controllers;
using WebApplicationDemoForAgileworks.DAL;
using WebApplicationDemoForAgileworks.Models;

namespace Tests
{
    public class TicketTests
    {
        [Test]
        public void CreateTicketWithDescriptionAndDueDate()
        {

            var description = "Test";
            var ticket = new SupportTicket(description, DateTime.Today.AddDays(1));

            Assert.That(ticket.Description, Is.EqualTo(description));
            Assert.That(ticket.DueDate,Is.EqualTo(DateTime.Today.AddDays(1)));
        }
        [Test]
        public void TicketShouldHaveEntryDateByDefault()
        {
            var ticket = new SupportTicket("test", DateTime.Today.AddDays(1));

            Assert.That(ticket.EntryDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(ticket.EntryDate.Date, Is.EqualTo(DateTime.Today));
        }
       
        [Test]
        public void NewTicketShouldHaveNullDoneDate()
        {

            var ticket = new SupportTicket("test", DateTime.Today.AddDays(1));

            Assert.That(ticket.DoneDate, Is.Null);

        }

        [Test]
        public void DoneTicketShouldHaveDoneDate()
        {

            var ticket = new SupportTicket("test", DateTime.Today.AddDays(1));
            ticket.MarkDone();
            
            Assert.That(ticket.DoneDate, Is.Not.Null);
        }
        [Test]
        public void TicketWithNullDescriptionShouldThrowException()
        {

            Assert.Throws<ArgumentException>(() => new SupportTicket(null, DateTime.Today.AddDays(1)));
        }
        [Test]
        public void TicketWithPastDueDateShouldThrowException()
        {

            Assert.Throws<ArgumentException>(() => new SupportTicket("TEST", DateTime.Today.AddDays(-1)));
        }
        [Test]
        public void TicketWithEmptyDescriptionShouldThrowException()
        {

            Assert.Throws<ArgumentException>(() => new SupportTicket("", DateTime.Today.AddDays(1)));
        }

    }
}
