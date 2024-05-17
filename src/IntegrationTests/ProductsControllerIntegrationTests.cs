using API;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IntegrationTests
{
    public class ProductsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ProductsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

        }

        [Fact]
        public async Task GetAllProducts_ReturnsSuccessStatusCode()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/v1.0/products");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsListOfProducts()
        {
            // Arrange & Act
            var response = await _client.GetAsync("/api/v1.0/products");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(content);

            // Assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task GetProductById_ExistingId_ReturnsSuccessStatusCode()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/v1.0/products/1");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProductById_NonExistingId_ReturnsNotFoundStatusCode()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/v1.0/products/999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateProduct_ValidInput_ReturnsCreatedStatusCode()
        {
            // Arrange
            var product = new Product { Name = "Product A", Price = 1000.0M, Description = "Product A" };

            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/v1.0/products", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_WithValidData_ReturnsNoContent()
        {
            // Arrange

            var productId = 4;
            var updatedProduct = new Product { Id = productId, Name = "Updated Product", Price = 16000.0M, Description = "Updated Product Description" };
            var content = new StringContent(JsonConvert.SerializeObject(updatedProduct), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/v1/products/{productId}", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var productId = 1;

            // Act
            var response = await _client.DeleteAsync($"/api/v1/products/{productId}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


    }

}