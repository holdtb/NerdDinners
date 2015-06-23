using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdDinnerFinal.Controllers;
using NerdDinnerFinal.Models;
using NerdDinnerFinal.Tests.Fakes;

namespace NerdDinnerFinal.Tests.Controllers
{
    [TestClass]
    public class DinnersControllerTest
    {
        private List<Dinner> CreateTestDinners()
        {
            var dinners = new List<Dinner>();
            for (var i = 0; i < 101; i++)
            {
                var sampleDinner = new Dinner
                {
                    DinnerId = i,
                    Title = "Sample Dinner",
                    HostedBy = "SomeUser",
                    Address = "Some Address",
                    Country = "USA",
                    ContactPhone = "425-555-1212",
                    Description = "Some description",
                    EventDate = DateTime.Now.AddDays(i),
                    Latitude = 99,
                    Longitude = -99
                };

                dinners.Add(sampleDinner);
            }
            return dinners;
        }

        private DinnersController createDinnersController()
        {
            var repository = new FakeDinnerRepository(CreateTestDinners());
            return new DinnersController(repository);
        }

        [TestMethod]
        public void DetailsAction_Should_Return_View_For_ExistingDinner()
        {
            //Arrange
            var controller = createDinnersController();

            //Act
            var result = controller.Details(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof (ViewResult));
        }

        [TestMethod]
        public void DetailsAction_Should_Return_NotFoundView_For_BogusDinner()
        {
            //Arrange
            var controller = createDinnersController();

            //Act
            var result = controller.Details(999) as ViewResult;

            //Assert
            Assert.AreEqual("NotFound", result.ViewName);
        }
    }
}