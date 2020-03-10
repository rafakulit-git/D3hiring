using d3hiring_api_tests.Interface;
using d3hiringNew.Controllers;
using d3hiringNew.Interface;
using d3hiringNew.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace d3hiring_api_tests.Controller
{
    public class HiringControllerTest
    {
        HiringController _controller;
        IHiringRepository _service;

        public HiringControllerTest()
        {
            _service = new IHiringRepositoryFake();
            _controller = new HiringController(_service);
        }

        [Fact]
        public void getstudents()
        {
            // Act
            var okResult = _controller.getstudents();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void getteachers()
        {
            // Act
            var okResult = _controller.getteachers();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void register()
        {
            //Arrange
            Register register = new Register()
            {
                Teacher = "teacherkeno@email.com",
                Students = new string[] { "studentriona@email.com", "studentsara@email.com" }
            };

            // Act
            var okResult = _controller.register(register);

            // Assert
            Assert.IsType<NoContentResult>(okResult);
        }

        [Fact]
        public void registerError()
        {
            //Arrange
            Register register = new Register()
            {
                Teacher = "",
                Students = { }
            };

            // Act
            var badResponse = _controller.register(register);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void commonstudentswith1Teacher()
        {
            //Arrange
            string[] teachers = { "teacherrafa@email.com" };

            // Act
            var okResult = _controller.commonstudents(teachers);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void commonstudentsMultipleTeachers()
        {
            //Arrange
            string[] teachers = { "teacherjay@email.com", "teacherjohn@email.com" };

            // Act
            var okResult = _controller.commonstudents(teachers);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void commonstudentsError()
        {
            //Arrange
            string[] teachers = { "" };

            // Act
            var badResponse = _controller.commonstudents(teachers);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void suspend()
        {
            //Arrange
            Suspend suspend = new Suspend()
            {
                Student = "studentzarah@email.com"
            };

            // Act
            var okResult = _controller.suspend(suspend);

            // Assert
            Assert.IsType<NoContentResult>(okResult);
        }

        [Fact]
        public void suspendError()
        {
            //Arrange
            Suspend suspend = new Suspend()
            {
                Student = ""
            };

            // Act
            var badResponse = _controller.suspend(suspend);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void retrievefornotifications()
        {
            //Arrange
            RetrieveNotification notification = new RetrieveNotification()
            {
                Teacher = "teacherjay@email.com",
                Notification = "Hello students!"
            };

            // Act
            var okResult = _controller.retrievefornotifications(notification);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void retrievefornotificationsError()
        {
            //Arrange
            RetrieveNotification notification = new RetrieveNotification()
            {
                Teacher = "",
                Notification = ""
            };

            // Act
            var badResponse = _controller.retrievefornotifications(notification);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}
