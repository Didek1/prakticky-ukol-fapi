using Eshop.Api.Controllers;
using Eshop.Api.Interfaces;
using Eshop.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Eshop.Tests.Controllers
{
    public class OrderControllerTests
    {
        [Fact]
        public void GetOrder_ReturnOkWithOrder()    //Test pro status 200 OK
        {
            var orderManagerMock = new Mock<IOrderManager>();

            var expected = new OrderDto { Id = 1u, FirstName = "Jan", LastName = "Novak", Email = "test@email.cz" };
            //Když zavolam GetOrder s id 1, tak vrátí expected
            orderManagerMock.Setup(m => m.GetOrder(1u)).Returns(expected);

            var controller = new OrderController(orderManagerMock.Object);
            var result = controller.GetOrder(1u);

            //Ověření, že výsledek je typu 200 OK s očekávaným objektem
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<OrderDto>(okResult.Value);

            //Ověření, že vracena hodnota odpovídá expected
            Assert.Equal(expected.Id, returned.Id);
            Assert.Equal(expected.FirstName, returned.FirstName);
            Assert.Equal(expected.LastName, returned.LastName);
            Assert.Equal(expected.Email, returned.Email);

            orderManagerMock.Verify(m => m.GetOrder(1u), Times.Once);
        }

        [Fact]
        public void GetOrder_ReturnNotFound()   //Test pro status 404 NotFound
        {
            var orderManagerMock = new Mock<IOrderManager>();

            //Když zavolam GetOrder s jakýmkoliv uint, tak vrátí null
            orderManagerMock.Setup(m => m.GetOrder(It.IsAny<uint>())).Returns((OrderDto?)null);

            var controller = new OrderController(orderManagerMock.Object);
            var result = controller.GetOrder(999u);

            //Ověření, že výsledek je typu 404 NotFound
            Assert.IsType<NotFoundResult>(result);
            orderManagerMock.Verify(m => m.GetOrder(999u), Times.Once);
        }

        [Fact]
        public void AddOrder_ReturnsCreatedAtActionResult() //Test pro status 201 CreatedAtAction
        {
            var OrderManagerMock = new Mock<IOrderManager>();

            //input je testoavci objekt a expected je ocekavany vysledek
            var input = new OrderDto { FirstName = "Jan", LastName = "Novak", Email = "test@email.cz" };
            var expected = new OrderDto { Id = 1u, FirstName = input.FirstName, LastName = input.LastName, Email = input.Email };

            //Když zavolam AddOrder s OrderDto, který má stejné FirstName a LastName jako input, tak vrátí expected
            OrderManagerMock.Setup(m => m
            .AddOrder(It.Is<OrderDto>(o => o.FirstName == input.FirstName && o.LastName == input.LastName)))
            .Returns(expected);

            var controller = new OrderController(OrderManagerMock.Object);
            var result = controller.AddOrder(input);

            //Ověření, že výsledek je typu 201 CreatedAtActionResult
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(OrderController.GetOrder), createdResult.ActionName);

            //Ověření, že route values obsahují "id" a že jeho hodnota odpovídá expected.Id
            Assert.NotNull(createdResult.RouteValues);
            Assert.True(createdResult.RouteValues!.ContainsKey("id"));
            Assert.Equal(expected.Id, Convert.ToUInt32(createdResult.RouteValues["id"]));

            //Ověření, že vrácená hodnota odpovídá expected
            var returned = Assert.IsType<OrderDto>(createdResult.Value);
            Assert.Equal(expected.Id, returned.Id);
            Assert.Equal(expected.FirstName, returned.FirstName);
            Assert.Equal(expected.LastName, returned.LastName);
            Assert.Equal(expected.Email, returned.Email);

            OrderManagerMock.Verify(m => m.AddOrder(It.IsAny<OrderDto>()), Times.Once);
        }

        [Fact]
        public void AddOrder_ReturnsBadRequest_WhenModelStateInvalid()  //Test pro status 400 BadRequest
        {
            var orderManagerMock = new Mock<IOrderManager>();

            var controller = new OrderController(orderManagerMock.Object);
            //Nastavime chybu v ModelState, nevalidni FirstName
            controller.ModelState.AddModelError("FirstName", "Required");

            var input = new OrderDto { FirstName = "", LastName = "Novak", Email = "t@e.c" };
            var result = controller.AddOrder(input);

            //Ověření, že výsledek je typu 400 BadRequest
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequest.Value);

            orderManagerMock.Verify(m => m.AddOrder(It.IsAny<OrderDto>()), Times.Never);
        }
    }
}
