using FluentAssertions;
using Moq;
using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;
using PosSystem.Service.Service;

namespace PosSystem.Tests.Service
{
    public class CategoryServiceTest
    {
        private readonly Mock<IRepository<Category>> _mockCategoryRepo;
        private readonly IService<Category> _categoryService;
        public CategoryServiceTest()
        {
            _mockCategoryRepo = new Mock<IRepository<Category>>();
            _categoryService = new CategoryService(_mockCategoryRepo.Object);
        }
        // -------------------------------------------------------------------
        //
        // Test for GetAll
        //
        // -------------------------------------------------------------------
        [Fact]
        public async Task GetAll_WhenCategoriesExist_ReturnsList()
        {
            // -------- Arrange --------
            var categories = new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    Description = "Electrónicos",
                    Status = "Activo" 
                },
                new Category 
                { 
                    CategoryId = 2, 
                    Description = "Ropa", 
                    Status = "Activo" 
                },
                new Category 
                { 
                    CategoryId = 3, 
                    Description = "Hogar", 
                    Status = "Inactivo"
                }
            };

            _mockCategoryRepo.Setup(repo => repo.GetAll()).ReturnsAsync(categories);

            var result = await _categoryService.GetAll();

            // -------- Assert --------
            result.Should().NotBeNull();
            result.Should().HaveCount(3);

            _mockCategoryRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenNoCategories_ReturnsEmptyList()
        {
            // -------- Arrange --------
            _mockCategoryRepo.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Category>());

            var result = await _categoryService.GetAll();
            result.Should().BeEmpty();

            _mockCategoryRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        // -------------------------------------------------------------------
        //
        // Test for GetById
        //
        // -------------------------------------------------------------------

        [Fact]
        public async Task GetById_WithExistingId_ReturnsCategory()
        {
            // -------- Arrange --------
            var categoryId = 1;
            var expectedCategory = new Category
            {
                CategoryId = categoryId,
                Description = "Electrónicos",
                Status = "Activo"
            };

            // Configure the mock to return an existing category
            _mockCategoryRepo.Setup(repo => repo.GetById(categoryId)).ReturnsAsync(expectedCategory);

            // -------- Act --------
            var result = await _categoryService.GetById(categoryId);

            // -------- Assert --------
            // Verify that the result is not null
            result.Should().NotBeNull();

            // Verify that the data is correct
            result.Should().BeEquivalentTo(expectedCategory);
            result.CategoryId.Should().Be(categoryId);
            result.Description.Should().Be("Electrónicos");

            // Verify that the repository method was called exactly once
            _mockCategoryRepo.Verify(repo => repo.GetById(categoryId), Times.Once);

            // Verify that no other methods were called
            _mockCategoryRepo.Verify(repo => repo.Create(It.IsAny<Category>()), Times.Never);
            _mockCategoryRepo.Verify(repo => repo.Update(It.IsAny<Category>()), Times.Never);
        }

        [Fact]
        public async Task GetById_WithNonExistingId_ReturnsNull()
        {
            // -------- Arrange --------
            var nonExistingId = 999;

            // Configure the mock to return null (category does not exist)
            _mockCategoryRepo.Setup(repo => repo.GetById(nonExistingId)).ReturnsAsync((Category?)null);

            // -------- Act --------
            var result = await _categoryService.GetById(nonExistingId);

            // -------- Assert --------
            // Verify that the result is null
            result.Should().BeNull();

            // Verify that the repository method was called exactly once
            _mockCategoryRepo.Verify(repo => repo.GetById(nonExistingId), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetById_WithZeroOrNegativeId_ThrowsArgumentException(int value)
        {
            // -------- Act & Assert --------
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.GetById(value));
            exception.Message.Should().Contain("ID no puede ser menor o igual a cero");

            // Verify that the repository was NOT called
            _mockCategoryRepo.Verify(x => x.GetById(It.IsAny<int>()), Times.Never);
        }

        // -------------------------------------------------------------------
        //
        // Test for Create
        //
        // -------------------------------------------------------------------
        [Theory]
        [InlineData("Teclado", "Activo")]
        [InlineData("Audifonos inálambricos", "Inactivo")]
        public async Task Create_WithValidData_ReturnsProduct(
            string description, string status
        )
        {
            // -------- Arrange --------
            var newCategory = new Category
            {
                Description = description,
                Status = status
            };

            // Configure the mock: We don't need it to return anything, just that the call is successful.
            // Use It.IsAny<ICategory>() to verify the call, regardless of the instance
            _mockCategoryRepo.Setup(repo => repo.Create(It.IsAny<Category>())).ReturnsAsync(newCategory);

            // -------- Act --------
            var result = await _categoryService.Create(newCategory);

            result.Should().NotBeNull();
            result.Should().BeSameAs(newCategory);
            result.Description.Should().Be(description);
            result.Status.Should().Be(status);

            // -------- Assert --------
            // Verify that the repository's AddAsync method was called exactly once.
            _mockCategoryRepo.Verify(repo => repo.Create(newCategory), Times.Once);
        }

        // -------------------------------------------------------------------
        //
        // Test for Update
        //
        // -------------------------------------------------------------------
        [Theory]
        [InlineData(1, "Teclado actualizado", "Activo")]
        [InlineData(2, "Audifonos inálambricos actualizado", "Inactivo")]
        public async Task Update_WithValidData_ReturnsCategory(
            int categoryId, string description, string status
        )
        {
            // -------- Arrange --------
            var updatedCategory = new Category
            {
                CategoryId = categoryId,
                Description = description,
                Status = status
            };

            // Configure the mock: We don't need it to return anything, just that the call is successful.
            // Use It.IsAny<ICategory>() to verify the call, regardless of the instance
            _mockCategoryRepo.Setup(repo => repo.Update(It.IsAny<Category>())).ReturnsAsync((Category category) => category);

            // -------- Act --------
            var result = await _categoryService.Update(updatedCategory);

            // -------- Assert --------
            result.Should().NotBeNull();
            result.CategoryId.Should().Be(categoryId);
            result.Description.Should().Be(description);
            result.Status.Should().Be(status);

            // Verify that the repository's AddAsync method was called exactly once
            _mockCategoryRepo.Verify(repo => repo.Update(It.Is<Category>(c => c.CategoryId == categoryId && c.Description == description)), Times.Once);
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

            _mockCategoryRepo.Setup(repo => repo.Delete(existingId)).ReturnsAsync(true);

            // -------- Act --------
            var result = await _categoryService.Delete(existingId);

            // -------- Assert --------
            result.Should().BeTrue();
            _mockCategoryRepo.Verify(repo => repo.Delete(existingId), Times.Once);
        }

        [Fact]
        public async Task Delete_WithNonExistingId_ReturnsFalse()
        {
            // -------- Arrange --------
            var nonExistingId = 999;

            _mockCategoryRepo.Setup(repo => repo.Delete(nonExistingId))
                          .ReturnsAsync(false);

            // -------- Act --------
            var result = await _categoryService.Delete(nonExistingId);

            // -------- Assert --------
            result.Should().BeFalse();
            _mockCategoryRepo.Verify(repo => repo.Delete(nonExistingId), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Delete_WithZeroOrNegativeId_ThrowsException(int nonExistingId)
        {
            // -------- Arrange, Act & Assert --------
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _categoryService.Delete(nonExistingId));
            exception.Message.Should().Contain("ID no puede ser menor o igual a cero");

            // Verify that the repository was NOT called
            _mockCategoryRepo.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
        }
    }
}