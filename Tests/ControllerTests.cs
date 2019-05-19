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

namespace Tests
{
    [TestFixture]
    class ControllerTests
    {

        private readonly TicketContext context;
        private TicketController controller;
        private string uniqueDescription;
        private SupportTicket dbTicket;

        public ControllerTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TicketContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "SupportTickets");
            uniqueDescription = "e9rt8gjhe0gr8" + Guid.NewGuid();
            context = new TicketContext(optionsBuilder.Options);
            controller = new TicketController(context);
            controller.AddTicket(uniqueDescription, DateTime.Today.AddDays(1));
            dbTicket = context.SupportTickets.Where(s => s.Description == uniqueDescription).FirstOrDefault();
        }
        

        [Test]
        public void AddTicketToDatabaseThoroughControllerAddTicketMethod()
        {
            var uniqueDescription = "e9rt8gjhe0gr8123123" + Guid.NewGuid();
            controller.AddTicket(uniqueDescription, DateTime.Today.AddDays(1));

            var dbTicket = context.SupportTickets.Where(s => s.Description == uniqueDescription).FirstOrDefault<SupportTicket>();

            Assert.That(dbTicket.Description, Is.EqualTo(uniqueDescription));
            Assert.That(dbTicket.DueDate, Is.EqualTo(DateTime.Today.AddDays(1)));
        }

        [Test]
        public void AddDoneDateToTicketThroughControllerCompleteTicketMethod()
        {
            var uniqueDescription = "e9rt8gjhe0gr8444" + Guid.NewGuid();
            controller.AddTicket(uniqueDescription, DateTime.Today.AddDays(1));

            var dbTicket = context.SupportTickets.Where(s => s.Description == uniqueDescription).FirstOrDefault<SupportTicket>();

            controller.CompleteTicket(dbTicket.Id);

            Assert.That(dbTicket.DoneDate, Is.Not.Null);
        }

        [Test]
        public void AddTicketMethodShouldRedirectToIndexOnSuccess()
        {
            var redirect = (RedirectToActionResult)controller.AddTicket(uniqueDescription, DateTime.Today.AddDays(1));

            Assert.That(redirect.ActionName, Is.EqualTo("Index"));

        }
        [Test]
        public void AddTicketMethodShouldRedirectToIndexWithFailedStateOnInvalidInput()
        {
            var redirect = (RedirectToActionResult)controller.AddTicket(null, DateTime.Today.AddDays(-1));

            Assert.That(redirect.ActionName, Is.EqualTo("Index"));
            Assert.That(redirect.RouteValues["state"], Is.EqualTo("AddFailed"));
        }

        [Test]
        public void CompleteTicketMethodShouldRedirectToIndexOnSuccess()
        {           
            var redirect = (RedirectToActionResult)controller.CompleteTicket(dbTicket.Id);
            Assert.That(redirect.ActionName, Is.EqualTo("Index"));
        }
        [Test]
        public void CompleteTicketMethodShouldRedirectToIndexWithFailedStateOnInvalidInput()
        {         
            var redirect = (RedirectToActionResult)controller.CompleteTicket(-100000);
            Assert.That(redirect.ActionName, Is.EqualTo("Index"));
            Assert.That(redirect.RouteValues["state"], Is.EqualTo("CompleteFailed"));

            var redirect2 = (RedirectToActionResult)controller.CompleteTicket(Int32.MaxValue);
            Assert.That(redirect2.ActionName, Is.EqualTo("Index"));
            Assert.That(redirect2.RouteValues["state"], Is.EqualTo("CompleteFailed"));
        }
        [Test]
        public void IndexReturnsAViewWithAListOfSupportTickets()
        {           
            var redirect = (ViewResult)controller.Index();
            Assert.That(redirect.Model as List<SupportTicket>, Contains.Item(dbTicket));
        }
        [Test]
        public void CompleteTicketMethodShouldFailWithNullContext()
        {
            TicketController testController = new TicketController(null);

            var redirect = (RedirectToActionResult)testController.CompleteTicket(1);

            Assert.That(redirect.ActionName, Is.EqualTo("Index"));
            Assert.That(redirect.RouteValues["state"], Is.EqualTo("CompleteFailed"));
        }
        [Test]
        public void AddTicketMethodShouldFailWithNullContext()
        {
            TicketController testController = new TicketController(null);
            
            var redirect = (ViewResult)testController.AddTicket("test", DateTime.Today.AddDays(1));
           
            Assert.That(redirect.ViewName, Is.EqualTo("DatabaseError"));
        }


        [Test]
        public void NullContextIndexShouldReturnNullModel()
        {
            TicketController testController = new TicketController(null);

            var redirect = (ViewResult)testController.Index();

            Assert.That(redirect.Model, Is.Null);
            Assert.That(redirect.ViewName, Is.EqualTo("DatabaseError"));
        }

    }
}
