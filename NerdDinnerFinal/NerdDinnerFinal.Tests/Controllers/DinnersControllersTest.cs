using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NerdDinnerFinal.Controllers;
using NerdDinnerFinal.Models;
using NerdDinnerFinal.Models.ViewModels;
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

        private DinnersController CreateDinnersController()
        {
            var repository = new FakeDinnerRepository(CreateTestDinners());
            return new DinnersController(repository);
        }

        private DinnersController CreateDinnersControllerAs(string userName)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = CreateDinnersController();
            controller.ControllerContext = mock.Object;

            return controller;
        }

        private DinnersController CreateDinnersControllerAsFakeItEasy(string userName)
        {
            var mock = A.Fake<ControllerContext>();
            A.CallTo(() => mock.HttpContext.User.Identity.Name).Returns(userName);
            A.CallTo(() => mock.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = CreateDinnersController();
            controller.ControllerContext = mock;

            return controller;
        }

        [TestMethod]
        public void DetailsAction_Should_Return_View_For_ExistingDinner()
        {
            //Arrange
            var controller = CreateDinnersController();

            //Act
            var result = controller.Details(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof (ViewResult));
        }

        [TestMethod]
        public void DetailsAction_Should_Return_NotFoundView_For_BogusDinner()
        {
            //Arrange
            var controller = CreateDinnersController();

            //Act
            var result = controller.Details(999) as ViewResult;

            //Assert
            Assert.AreEqual("NotFound", result.ViewName);
        }

        [TestMethod]
        public void EditAction_Should_Return_EditView_When_ValidOwner()
        {
            // Arrange
            var controller = CreateDinnersControllerAs("SomeUser");

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.ViewData.Model, typeof (DinnerFormViewModel));
        }

        [TestMethod]
        public void EditAction_Should_Return_InvalidOwnerView_When_InvalidOwner()
        {
            // Arrange
            var controller = CreateDinnersControllerAs("NotOwnerUser");

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.AreEqual(result.ViewName, "InvalidOwner");
        }

        [TestMethod]
        public void EditAction_Should_Redirect_When_Update_Successful()
        {
            // Arrange     
            var controller = CreateDinnersControllerAs("SomeUser");

            var formValues = new FormCollection
            {
                {"FakeTitle", "Fake value"},
                {"FakeDescription", "Another Fake description"}
            };

            controller.ValueProvider = formValues.ToValueProvider();

            // Act
            var result = controller.Edit(1, formValues) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Details", result.RouteValues["Action"]);
        }
    }
}