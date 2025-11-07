using FluentAssertions;
using Moq;
using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;
using PosSystem.Service.Service;

namespace PosSystem.Tests.Service
{
    public class ProductServiceTest
    {
        private readonly Mock<IRepository<Product>> _mockProductRepo;
        private readonly IService<Product> _productService;
        public ProductServiceTest()
        {
            _mockProductRepo = new Mock<IRepository<Product>>();
            _productService = new ProductService(_mockProductRepo.Object);
        }
        // -------------------------------------------------------------------
        //
        // Test for GetAll
        //
        // -------------------------------------------------------------------
        [Fact]
        public async Task GetAll_WhenProductsExist_ReturnsList()
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
                    SalePrice = 10.99m,
                    Stock = 100,
                    MinimumStock = 3,
                    Status = "Activo"
                },
                new Product
                {
                    ProductId = 2,
                    Barcode = "PRO-0002",
                    Description = "Ropa",
                    CategoryId = 2,
                    SalePrice = 19.99m,
                    Stock = 50,
                    MinimumStock = 3,
                    Status = "Activo"
                },
                new Product
                {
                    ProductId = 3,
                    Barcode = "PRO-0003",
                    Description = "Audifonos",
                    CategoryId = 3,
                    SalePrice = 21.99m,
                    Stock = 35,
                    MinimumStock = 4,
                    Status = "Activo"
                }
            };

            _mockProductRepo.Setup(repo => repo.GetAll()).ReturnsAsync(products);

            var result = await _productService.GetAll();

            // -------- Assert --------
            result.Should().NotBeNull();
            result.Should().HaveCount(3);

            _mockProductRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenNoProducts_ReturnsEmptyList()
        {
            // -------- Arrange --------
            _mockProductRepo.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Product>());

            var result = await _productService.GetAll();
            result.Should().BeEmpty();

            _mockProductRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        // -------------------------------------------------------------------
        //
        // Test for GetById
        //
        // -------------------------------------------------------------------

        [Fact]
        public async Task GetById_WithExistingId_ReturnsProduct()
        {
            // -------- Arrange --------
            var productId = 1;
            var expectedProduct = new Product
            {
                ProductId = productId,
                Barcode = "PRO-0002",
                Description = "Ropa",
                CategoryId = 2,
                SalePrice = 19.99m,
                Stock = 50,
                MinimumStock = 3,
                Status = "Activo"
            };

            // Configure the mock to return an existing product
            _mockProductRepo.Setup(repo => repo.GetById(productId)).ReturnsAsync(expectedProduct);

            // -------- Act --------
            var result = await _productService.GetById(productId);

            // -------- Assert --------
            // Verify that the result is not null
            result.Should().NotBeNull();

            // Verify that the data is correct
            result.Should().BeEquivalentTo(expectedProduct);
            result.ProductId.Should().Be(productId);
            result.Description.Should().Be("Ropa");

            // Verify that the repository method was called exactly once
            _mockProductRepo.Verify(repo => repo.GetById(productId), Times.Once);

            // Verify that no other methods were called
            _mockProductRepo.Verify(repo => repo.Create(It.IsAny<Product>()), Times.Never);
            _mockProductRepo.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Never);
        }

        [Theory]
        [InlineData(111111)]
        [InlineData(999)]
        public async Task GetById_WithNonExistingId_ReturnsNull(int nonExistingId)
        {
            // -------- Arrange --------
            // Configure the mock to return null (product does not exist)
            _mockProductRepo.Setup(repo => repo.GetById(nonExistingId)).ReturnsAsync((Product?)null);

            // -------- Act --------
            var result = await _productService.GetById(nonExistingId);

            // -------- Assert --------
            // Verify that the result is null
            result.Should().BeNull();

            // Verify that the repository method was called exactly once
            _mockProductRepo.Verify(repo => repo.GetById(nonExistingId), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetById_WithZeroOrNegativeId_ThrowsArgumentException(int value)
        {
            // -------- Act & Assert --------
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productService.GetById(value));
            exception.Message.Should().Contain("ID no puede ser menor o igual a cero");

            // Verify that the repository was NOT called
            _mockProductRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Never);
        }

        // -------------------------------------------------------------------
        //
        // Test for Create
        //
        // -------------------------------------------------------------------

        [Theory]
        [InlineData(null, "Teclado", 3, 49.99, 150, 10, "Activo")] // Null barcode
        [InlineData("AUD-0001", "Audifonos inálambricos", 3, 49.99, 150, 10, "Inactivo")]
        public async Task Create_WithValidData_ReturnsProduct(
            string barcode, string description, int categoryId, decimal price, int stock, int minimumStock, string status
        )
        {
            // -------- Arrange --------
            var newProduct = new Product
            {
                Barcode = barcode,
                Description = description,
                CategoryId = categoryId,
                SalePrice = price,
                Stock = stock,
                MinimumStock = minimumStock,
                Status = status
            };

            // Configure the mock: We don't need it to return anything, just that the call is successful.
            // Use It.IsAny<IProduct>() to verify the call, regardless of the instance
            _mockProductRepo.Setup(repo => repo.Create(It.IsAny<Product>())).ReturnsAsync(newProduct);

            // -------- Act --------
            var result = await _productService.Create(newProduct);

            result.Should().NotBeNull();
            result.Should().BeSameAs(newProduct);
            result.Description.Should().Be(description);
            result.Status.Should().Be(status);

            // -------- Assert --------
            // Verify that the repository's AddAsync method was called exactly once.
            _mockProductRepo.Verify(repo => repo.Create(newProduct), Times.Once);
        }

        // -------------------------------------------------------------------
        //
        // Test for Update
        //
        // -------------------------------------------------------------------
        [Theory]
        [InlineData(1, null, "Teclado", 3, 49.99, 150, 10, "Activo")] // Null barcode
        [InlineData(2, "AUD-0001", "Audifonos inálambricos", 3, 49.99, 150, 10, "Inactivo")]
        public async Task Update_WithValidData_ReturnsProduct(
            int productId, string barcode, string description, int categoryId, decimal price, int stock, int minimumStock, string status
        )
        {
            // -------- Arrange --------
            var updatedProduct = new Product
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

            // Configure the mock: We don't need it to return anything, just that the call is successful.
            // Use It.IsAny<IProduct>() to verify the call, regardless of the instance
            _mockProductRepo.Setup(repo => repo.Update(It.IsAny<Product>())).ReturnsAsync((Product product) => product);

            // -------- Act --------
            var result = await _productService.Update(updatedProduct);

            // -------- Assert --------
            result.Should().NotBeNull();
            result.ProductId.Should().Be(productId);
            result.Description.Should().Be(description);
            result.Status.Should().Be(status);

            // Verify that the repository's AddAsync method was called exactly once
            _mockProductRepo.Verify(repo => repo.Update(It.Is<Product>(c => c.ProductId == productId && c.Description == description)), Times.Once);
        }

        // -------------------------------------------------------------------
        //
        // Test for delete
        //
        // -------------------------------------------------------------------
        [Fact]
        public async Task Delete_WithExistingId_ReturnsTrue()
        {
            // -------- Arrange --------
            var existingId = 1;

            _mockProductRepo.Setup(repo => repo.Delete(existingId)).ReturnsAsync(true);

            // -------- Act --------
            var result = await _productService.Delete(existingId);

            // -------- Assert --------
            result.Should().BeTrue();
            _mockProductRepo.Verify(repo => repo.Delete(existingId), Times.Once);
        }

        [Fact]
        public async Task Delete_WithNonExistingId_ReturnsFalse()
        {
            // -------- Arrange --------
            var nonExistingId = 999;

            _mockProductRepo.Setup(repo => repo.Delete(nonExistingId)).ReturnsAsync(false);

            // -------- Act --------
            var result = await _productService.Delete(nonExistingId);

            // -------- Assert --------
            result.Should().BeFalse();
            _mockProductRepo.Verify(repo => repo.Delete(nonExistingId), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Delete_WithZeroOrNegativeId_ThrowsException(int nonExistingId)
        {
            // -------- Arrange, Act & Assert --------
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _productService.Delete(nonExistingId));
            exception.Message.Should().Contain("ID no puede ser menor o igual a cero");

            // Verify that the repository was NOT called
            _mockProductRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        }
    }
}