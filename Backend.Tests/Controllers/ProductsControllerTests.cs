using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Backend.Interfaces;
using Backend.Models;
using Backend.Controllers;
using Backend.Dto;
using System.Collections.Generic;

namespace Backend.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductsRepository> _mockProductsRepository;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductsRepository = new Mock<IProductsRepository>();
            _controller = new ProductsController(_mockProductsRepository.Object);
        }

        [Fact]
        public void GetProducts_ReturnsOk_WithPaginationDto()
        {
            var pageNumber = 1;
            var pageSize = 10;
            var paginationResult = new PaginationDto<Product>
            {
                Result = new List<Product>
                {
                    new Product { Id = 1, Name = "Product 1", Price = 100 },
                    new Product { Id = 2, Name = "Product 2", Price = 200 }
                },
                TotalPages = 1
            };

            _mockProductsRepository
                .Setup(repo => repo.GetProducts(pageNumber, pageSize))
                .Returns(paginationResult);

            var result = _controller.GetProducts(pageNumber, pageSize) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<PaginationDto<Product>>(result.Value);
        }

        [Fact]
        public void GetProduct_ReturnsOkResult_WhenProductExists()
        {
            var productId = 1;
            var product = new Product { Id = productId, Name = "Product 1", Price = 100 };

            _mockProductsRepository.Setup(repo => repo.ProductExists(productId)).Returns(true);
            _mockProductsRepository.Setup(repo => repo.GetProduct(productId)).Returns(product);

            var result = _controller.GetProduct(productId) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<Product>(result.Value);
            Assert.Equal(productId, ((Product)result.Value).Id);
        }

        [Fact]
        public void GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            var productId = 1;
            _mockProductsRepository.Setup(repo => repo.ProductExists(productId)).Returns(false);

            var result = _controller.GetProduct(productId) as NotFoundResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void CreateProduct_ReturnsOkResult_WhenProductIsCreated()
        {
            var newProduct = new Product { Id = 1, Name = "New Product", Price = 150 };
            _mockProductsRepository.Setup(repo => repo.CreateProduct(newProduct)).Returns(true);

            var result = _controller.CreateProduct(newProduct) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public void UpdateProduct_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            var productId = 1;
            var productToUpdate = new Product { Id = productId, Name = "Updated Product", Price = 200 };

            _mockProductsRepository.Setup(repo => repo.ProductExists(productId)).Returns(true);
            _mockProductsRepository.Setup(repo => repo.UpdateProduct(productToUpdate)).Returns(true);

            var result = _controller.UpdateProduct(productId, productToUpdate) as NoContentResult;

            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void UpdateProduct_ReturnsBadRequest_WhenProductIdsDoNotMatch()
        {
            var productId = 1;
            var productToUpdate = new Product { Id = 2, Name = "Updated Product", Price = 200 };

            var result = _controller.UpdateProduct(productId, productToUpdate);

            Assert.NotNull(result);
            var badRequestResult = result as BadRequestResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

    }
}
