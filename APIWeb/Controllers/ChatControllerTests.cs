using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using APIWeb.Controllers;
using APIWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Tests
{
    public class ChatControllerTests
    {
        [Fact]
        public async Task GetAllChats_ReturnsOkObjectResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Chats.Add(new Chat { Id = 1, ReceiverPhone = "0569590676", SenderID = "1", Email = "maram@gmail.com" });
                context.Chats.Add(new Chat { Id = 2, ReceiverPhone = "0123456789", SenderID = "2", Email = "john@example.com" });
                await context.SaveChangesAsync();

                var controller = new ChatController(context);

                // Act
                var result = await controller.GetAllChats();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var chats = Assert.IsAssignableFrom<List<Chat>>(okResult.Value);
                Assert.Equal(2, chats.Count);
            }
        }
    }
}