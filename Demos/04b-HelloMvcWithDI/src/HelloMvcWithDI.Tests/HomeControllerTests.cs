using HelloMvcWithDI.Controllers;
using HelloMvcWithDI.Patterns;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace HelloMvcWithDI.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async void Index_action_should_return_products()
        {
            // Arrange
            IProductRepository repo = new FakeProductRepository();
            var controller = new ProductsController(repo);

            // Act
            IActionResult result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
