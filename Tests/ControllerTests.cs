using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplicationDemoForAgileworks.Controllers;
using WebApplicationDemoForAgileworks.DAL;
using WebApplicationDemoForAgileworks.Models;
using WebApplicationDemoForAgileworks.ViewModel.Ticket;

namespace Tests
{
    [TestFixture]
    class ControllerTests
    {
        private TicketContext context;
        private TicketController controller;
        private int dbTicket2Id;

        [SetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TicketContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "SupportTickets" + Guid.NewGuid());

            context = new TicketContext(optionsBuilder.Options);
            
            controller = new TicketController(context);
        }


        [Test]
        public void AddTicketToDatabaseThoroughControllerAddTicketMethod()
        {       
            controller.AddTicket("Test", DateTime.Today.AddDays(1));
            Assert.That(context.SupportTickets.Count(), Is.EqualTo(1));
            var dbTicket = context.SupportTickets.Single();
            Assert.That(dbTicket.Description, Is.EqualTo("Test"));
            Assert.That(dbTicket.DueDate, Is.EqualTo(DateTime.Today.AddDays(1)));
        }

        [Test]
        public void AddDoneDateToTicketThroughControllerCompleteTicketMethod()
        {
            SupportTicket dbTicket2 = new SupportTicket("Test2", DateTime.Today.AddDays(1));
            context.Add(dbTicket2);
            context.SaveChanges();
            dbTicket2Id = dbTicket2.Id;

            controller.CompleteTicket(dbTicket2Id);

            Assert.That(dbTicket2.DoneDate, Is.Not.Null);
        }

        [Test]
        public void AddTicketMethodShouldRedirectToIndexOnSuccess()
        {
            var redirect = (RedirectToActionResult)controller.AddTicket("Test", DateTime.Today.AddDays(1));

            Assert.That(redirect.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public void CompleteTicketMethodShouldRedirectToIndexOnSuccess()
        {
            SupportTicket dbTicket2 = new SupportTicket("Test2", DateTime.Today.AddDays(1));
            context.Add(dbTicket2);
            context.SaveChanges();
            dbTicket2Id = dbTicket2.Id;
            var redirect = (RedirectToActionResult)controller.CompleteTicket(dbTicket2Id);
            Assert.That(redirect.ActionName, Is.EqualTo("Index"));
        }

        [TestCase(-10000)]
        [TestCase(Int32.MaxValue)]
        public void CompleteTicketMethodShouldRedirectToIndexWithFailedStateOnInvalidInput(int id)
        {         
            var result = controller.CompleteTicket(id);
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void IndexReturnsAViewWithAListOfSupportTickets()
        {           
            var redirect = (ViewResult)controller.Index();
            Assert.That(redirect.Model, Is.TypeOf<IndexViewModel>());
        }

        [Test]
        public void IndexMethodReturnsOrderedByDueDateList()
        {

            SupportTicket dbTicket1 = new SupportTicket("A", DateTime.Today.AddDays(1));
            SupportTicket dbTicket3 = new SupportTicket("B", DateTime.Today.AddDays(5));
            SupportTicket dbTicket2 = new SupportTicket("C", DateTime.Today.AddDays(3));

            context.Add(dbTicket1);
            context.Add(dbTicket3);
            context.Add(dbTicket2);
            context.SaveChanges();

            var redirect = (ViewResult)controller.Index();

            var ticketModels = ((IndexViewModel)redirect.Model).Tickets;
            var ticketModelsTest = ticketModels.OrderBy(t => t.DueDate);

            //var list2 = list1.OrderBy(t => t.DueDate);
            CollectionAssert.AreEqual(ticketModelsTest, ticketModels);
            Assert.That(ticketModels, Is.Ordered.By("DueDate"));

        }

    }
}
