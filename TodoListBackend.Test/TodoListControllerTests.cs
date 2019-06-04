using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using TodoListBackend.DAL.Repositories;
using TodoListBackend.BLL.Interfaces;
using TodoListBackend.BLL.DTOs;
using TodoListBackend.WEB.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace TodoListBackend.Test
{
    [ExcludeFromCodeCoverage]
    public class TodoListControllerTests
    {
        [Fact]
        public async Task TestGetAllAsync_200OK()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            mock.Setup(serv => serv.GetAllAsync()).Returns(async () => { return DBContextMocker.GetAllDTOsList(); });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.GetAllAsync()).Result as ObjectResult;
            var value = result.Value as List<TodoItemDTO>;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(5, value.Count);
        }
        [Fact]
        public async Task TestGetAllAsync_404NotFound()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            mock.Setup(serv => serv.GetAllAsync()).Returns(async () => { return null; });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.GetAllAsync()).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
        [Fact]
        public async Task TestGetAllAsync_400BadRequest()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            mock.Setup(serv => serv.GetAllAsync()).Returns(async () => { throw new Exception(); });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.GetAllAsync()).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
        [Fact]
        public async Task TestGetByIdAsync_200Ok()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            int id = 1;
            mock.Setup(serv => serv.GetByIdAsync(id)).Returns(async () => {
                return new TodoItemDTO { Id = 1, Text = "", IsCompleted = false };
            });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.GetByIdAsync(id)).Result as ObjectResult;
            var value = result.Value as TodoItemDTO;

            // Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(id, value.Id);
        }
        [Fact]
        public async Task TestGetByIdAsync_404NotFound()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            int id = 1;
            mock.Setup(serv => serv.GetByIdAsync(id)).Returns(async () => {
                throw new NullReferenceException();
            });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.GetByIdAsync(id)).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
        [Fact]
        public async Task TestGetByIdAsync_400BadRequest()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            int id = 1;
            mock.Setup(serv => serv.GetByIdAsync(id)).Returns(async () => {
                throw new Exception();
            });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.GetByIdAsync(id)).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
        [Fact]
        public async Task TestPostAsync_201Created()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            TodoItemDTO item = new TodoItemDTO { Id = 1, Text = "", IsCompleted = false };
            mock.Setup(serv => serv.CreateAsync(item)).Returns(async () => { return item; });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.PostAsync(item)).Result as ObjectResult;
            var value = result.Value as TodoItemDTO;

            // Assert
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal(1, value.Id);
        }
        [Fact]
        public async Task TestPostAsync_400BadRequest()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            TodoItemDTO item = new TodoItemDTO { Id = 1, Text = "", IsCompleted = false };
            mock.Setup(serv => serv.CreateAsync(item)).Returns(async () => { throw new Exception(); });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.PostAsync(item)).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
        [Fact]
        public async Task TestPutAsync_204NoContent()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            TodoItemDTO item = new TodoItemDTO { Id = 1, Text = "", IsCompleted = false };
            mock.Setup(serv => serv.UpdateAsync(item)).Returns(async () => { return item; });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.PutAsync(item.Id, item)).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }
        [Fact]
        public async Task TestPutAsync_400BadRequest_OnId()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            TodoItemDTO item = new TodoItemDTO { Id = 1, Text = "", IsCompleted = false };
            mock.Setup(serv => serv.UpdateAsync(item)).Returns(async () => { return item; });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.PutAsync(item.Id + 1, item)).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
        [Fact]
        public async Task TestPutAsync_400BadRequest()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            TodoItemDTO item = new TodoItemDTO { Id = 1, Text = "", IsCompleted = false };
            mock.Setup(serv => serv.UpdateAsync(item)).Returns(async () => { throw new Exception(); });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.PutAsync(item.Id, item)).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
        [Fact]
        public async Task TestDeleteAsync_204NoContent()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            TodoItemDTO item = new TodoItemDTO { Id = 1, Text = "", IsCompleted = false };
            mock.Setup(serv => serv.DeleteAsync(item.Id)).Returns(async () => { return item; });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.DeleteAsync(item.Id)).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }
        [Fact]
        public async Task TestDeleteAsync_404NotFound()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            TodoItemDTO item = new TodoItemDTO { Id = 1, Text = "", IsCompleted = false };
            mock.Setup(serv => serv.DeleteAsync(item.Id)).Returns(async () => { throw new NullReferenceException(); });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.DeleteAsync(item.Id)).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
        [Fact]
        public async Task TestDeleteAsync_404NotFound_AE()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            TodoItemDTO item = new TodoItemDTO { Id = 1, Text = "", IsCompleted = false };
            mock.Setup(serv => serv.DeleteAsync(item.Id)).Returns(async () => { throw new ArgumentNullException(); });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.DeleteAsync(item.Id)).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
        [Fact]
        public async Task TestDeleteAsync_400BadRequest()
        {
            // Arrange
            var mock = new Mock<ITodoItemService>();
            TodoItemDTO item = new TodoItemDTO { Id = 1, Text = "", IsCompleted = false };
            mock.Setup(serv => serv.DeleteAsync(item.Id)).Returns(async () => { throw new Exception(); });
            var controller = new TodoItemsController(mock.Object);

            // Act
            var result = (await controller.DeleteAsync(item.Id)).Result as IStatusCodeActionResult;

            // Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }
    }
}
