using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PosSystem.Api.Controllers;
using PosSystem.Dto.Dto;
using PosSystem.Model.Model;
using PosSystem.Service.Interface;

namespace PosSystemTests.Controller
{
    public class ProductControllerTest
    {
        private readonly Mock<IService<Product>> _mockProductService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductController _productController;

        public ProductControllerTest()
        {
            _mockProductService = new Mock<IService<Product>>();
            _mockMapper = new Mock<IMapper>();
            _productController = new ProductController(_mockProductService.Object, _mockMapper.Object);
        }

        // ------------------------------------------
        // Test for create
        // ------------------------------------------
        [Theory]
        [InlineData(1, "AI-0001", "Audifonos inálambricos redmi", 1, 15.0, 150, 1, "Activo")]
        [InlineData(1, "TEL-0001", "Samsung S24 pro", 2, 15.0, 300.99, 1, "Activo")]
        [InlineData(1, "TEL-0002", "Redmi 12s pro", 2, 250.40, 150, 1, "Inactivo")]
        [InlineData(1, "TEC-0001", "Teclado", 3, 49.99, 150, 1, "Activo")]
        public async Task Create_WithValidData_ReturnsOk(
            int productId, string barcode, string description, int categoryId, decimal price, int stock, int minimumStock, string status
        )
        {
            // -------- Arrange --------
            var productDto = new ProductDto 
            { 
                ProductId = productId,
                Barcode = barcode,
                Description = description, 
                CategoryId = categoryId, 
                SalePrice = price, 
                Stock = stock, 
                MinimumStock = minimumStock, 
                Status = status 
            };

            var productEntity = new Product 
            { 
                ProductId = productId, 
                Barcode = barcode, 
                Description = description, 
                CategoryId = categoryId, 
                SalePrice = price, 
                Stock = stock, 
                MinimumStock = minimumStock, 
                Status = status 
            };

            _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(productEntity);
            _mockProductService.Setup(s => s.Create(productEntity)).ReturnsAsync(productEntity);
            _mockMapper.Setup(m => m.Map<ProductDto>(productEntity)).Returns(productDto);

            // -------- Act --------
            var result = await _productController.Create(productDto);

            // -------- Assert --------
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);

            _mockProductService.Verify(s => s.Create(productEntity), Times.Once);
        }

        [Theory]
        [InlineData(null, "Teclado", 3, 49.99, 150, 10, "Activo")] // Null barcode
        [InlineData("TEC-0001", null, 3, 49.99, 150, 10, "Activo")] // Null description
        [InlineData("TEC-0001", "Teclado", null, 49.99, 150, 10, "Activo")] // Null category
        [InlineData("TEC-0001", "Teclado", 3, null, 150, 10, "Activo")] // Null price
        [InlineData("TEC-0001", "Teclado", 3, 0, 150, 10, "Activo")] // Zero price
        [InlineData("TEC-0001", "Teclado", 3, -49.99, 150, 10, "Activo")] // Negative price
        [InlineData("TEC-0001", "Teclado", 3, 49.99, null, 10, "Activo")] // Null stock
        [InlineData("TEC-0001", "Teclado", 3, 49.99, 0, 10, "Activo")] // Zero stock
        [InlineData("TEC-0001", "Teclado", 3, 49.99, -150, 10, "Activo")] // Negative stock
        [InlineData("TEC-0001", "Teclado", 3, 49.99, 150, null, "Activo")] // Null minimum stock
        [InlineData("TEC-0001", "Teclado", 3, 49.99, 150, 0, "Activo")] // Zero minimum stock
        [InlineData("TEC-0001", "Teclado", 3, 49.99, 150, -10, "Activo")] // Negative minimum stock
        [InlineData("TEC-0001", "Teclado", 3, 49.99, 150, 10, null)] // Null status
        [InlineData("TEC-0001", "Teclado", 3, 49.99, 150, 10, "Desactualizado")] // Other status that not 'active' or 'inactive'
        public async Task Create_WithInvalidModel_ReturnsBadRequest(
            string barcode, string description, int categoryId, decimal price, int stock, int minimumStock, string status
        )
        {
            // Arrange
            var productDto = new ProductDto { 
                ProductId = 1, 
                Barcode = barcode, 
                Description = description, 
                CategoryId = categoryId, 
                SalePrice = price, 
                Stock = stock, 
                MinimumStock = minimumStock, 
                Status = status 
            };
            
            // Act
            var result = await _productController.Create(productDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.StatusCode.Should().Be(400);
            _mockProductService.Verify(s => s.Create(It.IsAny<Product>()), Times.Never);
        }

        // ------------------------------------------
        // Test for GetById
        // ------------------------------------------
        [Fact]
        public async Task GetById_WithExistingId_ReturnsOk()
        {
            var productId = 1;
            var productEntity = new Product { ProductId = 1, Barcode = "AI-0001", Description = "Audifonos inhalambricos", CategoryId = 1, SalePrice = 10.0M, Stock = 10, MinimumStock = 5, Status = "Activo" };
            var productDto = new ProductDto { ProductId = 1, Barcode = "AI-0001", Description = "Audifonos inhalambricos", CategoryId = 1, SalePrice = 10.0M, Stock = 10, MinimumStock = 5, Status = "Activo" };

            _mockProductService.Setup(s => s.GetById(productId)).ReturnsAsync(productEntity);
            _mockMapper.Setup(m => m.Map<ProductDto>(productEntity)).Returns(productDto);

            // -------- Act --------
            var result = await _productController.GetById(productId);

            // -------- Assert --------
            
            // Verify that the ActionResult has a Result of type OkObjectResult
            result.Result.Should().NotBeNull().And.BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);

            // Verify the returned value
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(productDto);

            _mockProductService.Verify(s => s.GetById(productId), Times.Once);
            _mockMapper.Verify(m => m.Map<ProductDto>(productEntity), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(999)]
        public async Task GetById_WithNonExistingId_ReturnsNotFound(int nonExistingId)
        {
            // -------- Arrange --------
            _mockProductService.Setup(s => s.GetById(nonExistingId)).ReturnsAsync((Product?)null);

            // -------- Act --------
            var result = await _productController.GetById(nonExistingId);

            // -------- Assert --------
            result.Result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundResult = result.Result as NotFoundObjectResult;
            notFoundResult.StatusCode.Should().Be(404);

            _mockProductService.Verify(s => s.GetById(nonExistingId), Times.Once);
            _mockMapper.Verify(m => m.Map<ProductDto>(It.IsAny<Product>()), Times.Never);
        }

        // ------------------------------------------
        // Test for Get all
        // ------------------------------------------
        [Fact]
        public async Task GetAll_WithData_ReturnsOk()
        {
            // -------- Arrange --------
            var products = new List<Product>
            {
                new Product 
                {
                    ProductId = 1,
                    Barcode = "PRO-0001",
                    Description = "Electrónicos",
                    CategoryId = 1,
                    SalePrice = 10.0m,
                    Stock = 100,
                    MinimumStock = 10,
                    Status = "Activo"
                },
                new Product
                {
                    ProductId = 1,
                    Barcode = "PRO-0002",
                    Description = "Ropa",
                    CategoryId = 2,
                    SalePrice = 19.99m,
                    Stock = 50,
                    MinimumStock = 3,
                    Status = "Activo"
                }
            };

            var productsDto = new List<ProductDto>
            {
                new ProductDto
                {
                    ProductId = 1,
                    Barcode = "PRO-0001",
                    Description = "Electrónicos",
                    CategoryId = 1,
                    SalePrice = 10.0m,
                    Stock = 100,
                    MinimumStock = 10,
                    Status = "Activo"
                },
                new ProductDto
                {
                    ProductId = 1,
                    Barcode = "PRO-0002",
                    Description = "Ropa",
                    CategoryId = 2,
                    SalePrice = 19.99m,
                    Stock = 50,
                    MinimumStock = 3,
                    Status = "Activo"
                }
            };

            _mockProductService.Setup(s => s.GetAll()).ReturnsAsync(products);
            _mockMapper.Setup(m => m.Map<List<ProductDto>>(products)).Returns(productsDto);

            // -------- Act --------
            var result = await _productController.GetAll();

            // -------- Assert --------
            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);

            var returnedProducts = okResult.Value as List<ProductDto>;
            returnedProducts.Should().HaveCount(2);
            returnedProducts.Should().BeEquivalentTo(productsDto);

            _mockProductService.Verify(s => s.GetAll(), Times.Once);
            _mockMapper.Verify(m => m.Map<List<ProductDto>>(products), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenEmpty_ReturnsOkWithEmptyList()
        {
            // -------- Arrange --------
            var emptyProducts = new List<Product>();
            var emptyProductsDto = new List<ProductDto>();

            _mockProductService.Setup(s => s.GetAll()).ReturnsAsync(emptyProducts);
            _mockMapper.Setup(m => m.Map<List<ProductDto>>(emptyProducts)).Returns(emptyProductsDto);

            // -------- Act --------
            var result = await _productController.GetAll();

            // -------- Assert --------
            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);

            var returnedProducts = okResult.Value as List<ProductDto>;
            returnedProducts.Should().BeEmpty();

            _mockProductService.Verify(s => s.GetAll(), Times.Once);
            _mockMapper.Verify(m => m.Map<List<ProductDto>>(emptyProducts), Times.Once);
        }

        // ------------------------------------------
        // Test for Update
        // ------------------------------------------
        [Fact]
        public async Task Update_WithValidData_ReturnsOk()
        {
            // -------- Arrange --------
            var productId = 1;
            var productDto = new ProductDto
            { 
                ProductId = productId,
                Barcode = "PRO-0001",
                Description = "Electrónicos Actualizados",
                CategoryId = 1,
                SalePrice = 10.0m,
                Stock = 100,
                MinimumStock = 10,
                Status = "Activo"
            };
            
            var productEntity = new Product 
            {
                ProductId = productId,
                Barcode = "PRO-0001",
                Description = "Electrónicos Actualizados",
                CategoryId = 1,
                SalePrice = 10.0m,
                Stock = 100,
                MinimumStock = 10,
                Status = "Activo"
            };
            
            var updatedEntity = new Product
            {
                ProductId = productId,
                Barcode = "PRO-0001",
                Description = "Electrónicos Actualizados",
                CategoryId = 1,
                SalePrice = 10.0m,
                Stock = 100,
                MinimumStock = 10,
                Status = "Activo"
            };

            var updatedDto = new ProductDto
            {
                ProductId = productId,
                Barcode = "PRO-0001",
                Description = "Electrónicos Actualizados",
                CategoryId = 1,
                SalePrice = 10.0m,
                Stock = 100,
                MinimumStock = 10,
                Status = "Activo"
            };

            _productController.ModelState.Clear();

            _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(productEntity);
            _mockProductService.Setup(s => s.Update(productEntity)).ReturnsAsync(updatedEntity);
            _mockMapper.Setup(m => m.Map<ProductDto>(updatedEntity)).Returns(updatedDto);

            // -------- Act --------
            var result = await _productController.Update(productId, productDto);

            // -------- Assert --------
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);

            // Verificar la estructura de la respuesta exitosa
            var valueType = okResult.Value.GetType();
            var statusCodeProperty = valueType.GetProperty("StatusCode");
            var messageProperty = valueType.GetProperty("Message");

            var statusCode = statusCodeProperty.GetValue(okResult.Value);
            var message = messageProperty.GetValue(okResult.Value);

            statusCode.Should().Be(200);
            message.Should().Be("Registro actualizado con éxito.");

            _mockProductService.Verify(s => s.Update(productEntity), Times.Once);
            _mockMapper.Verify(m => m.Map<Product>(productDto), Times.Once);
            _mockMapper.Verify(m => m.Map<ProductDto>(updatedEntity), Times.Once);
        }

        [Theory]
        [InlineData(-1)] // Null minimum stock
        [InlineData(0)] // Zero minimum stock
        public async Task Update_WithNonExistingId_ReturnsBadRequest(int productId)
        {
            // -------- Arrange --------
            var productDto = new ProductDto
            {
                ProductId = productId,
                Barcode = "PRO-0001",
                Description = "Producto Inexistente",
                CategoryId = 1,
                SalePrice = 10.0m,
                Stock = 100,
                MinimumStock = 10,
                Status = "Activo" 
            };
            
            var productEntity = new Product
            {
                ProductId = productId,
                Barcode = "PRO-0001",
                Description = "Producto Inexistente",
                CategoryId = 1,
                SalePrice = 10.0m,
                Stock = 100,
                MinimumStock = 10,
                Status = "Activo"
            };

            _mockMapper.Setup(m => m.Map<Product>(productDto)).Returns(productEntity);

            // Simulate that the service returns a false message that it did not find the product.
            _mockProductService.Setup(s => s.Update(productEntity)).ReturnsAsync((Product?)null);

            // -------- Act --------
            var result = await _productController.Update(productId, productDto);

            // -------- Assert --------
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.StatusCode.Should().Be(400);
        }

        // ------------------------------------------
        // Test for Delete
        // ------------------------------------------
        [Fact]
        public async Task Delete_WithExistingId_ReturnsOk()
        {
            // -------- Arrange --------
            var productId = 1;
            _mockProductService.Setup(s => s.Delete(productId)).ReturnsAsync(true);

            // -------- Act --------
            var result = await _productController.Delete(productId);

            // -------- Assert --------
            result.Should().BeOfType<OkObjectResult>();

            // Verify success messsage
            var okResult = result as OkObjectResult;
            var valueType = okResult.Value.GetType();
            var statusCodeProperty = valueType.GetProperty("StatusCode");
            var messageProperty = valueType.GetProperty("Message");

            var statusCode = statusCodeProperty.GetValue(okResult.Value);
            var message = messageProperty.GetValue(okResult.Value);

            statusCode.Should().Be(200);
            message.Should().Be("Registro eliminado satisfactoriamente.");

            _mockProductService.Verify(s => s.Delete(productId), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(999)]
        public async Task Delete_WithNonExistingId_ReturnsNotFound(int nonExistingId)
        {
            // -------- Arrange --------
            _mockProductService.Setup(s => s.Delete(nonExistingId)).ReturnsAsync(false);

            // -------- Act --------
            var result = await _productController.Delete(nonExistingId);

            // -------- Assert --------
            result.Should().BeOfType<NotFoundObjectResult>();

            // Verify error message
            var notFoundResult = result as NotFoundObjectResult;
            var valueType = notFoundResult.Value.GetType();
            var statusCodeProperty = valueType.GetProperty("StatusCode");
            var messageProperty = valueType.GetProperty("Message");

            var statusCode = statusCodeProperty.GetValue(notFoundResult.Value);
            var message = messageProperty.GetValue(notFoundResult.Value);

            statusCode.Should().Be(404);
            message.Should().Be("Registro no encontrado.");

            _mockProductService.Verify(s => s.Delete(nonExistingId), Times.Once);
        }
    }
}
