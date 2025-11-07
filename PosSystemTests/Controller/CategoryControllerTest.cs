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
    public class CategoryControllerTest
    {
        private readonly Mock<IService<Category>> _mockCategoryService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryController _categoryController;

        public CategoryControllerTest()
        {
            _mockCategoryService = new Mock<IService<Category>>();
            _mockMapper = new Mock<IMapper>();
            _categoryController = new CategoryController(_mockCategoryService.Object, _mockMapper.Object);
        }

        // ------------------------------------------
        // Test for create
        // ------------------------------------------
        [InlineData("Télefonos", "Inactivo")]
        [InlineData("Accesorios", "Activo")]
        public async Task Create_WithValidData_ReturnsOk(
            string description, string status
        )
        {
            // -------- Arrange --------
            var categoryDto = new CategoryDto
            {
                CategoryId = 1,
                Description = description,
                Status = status
            };
            
            var categoryEntity = new Category 
            { 
                CategoryId = 1, 
                Description = description, 
                Status = status 
            };

            _mockMapper.Setup(m => m.Map<Category>(categoryDto)).Returns(categoryEntity);
            _mockCategoryService.Setup(s => s.Create(categoryEntity)).ReturnsAsync(categoryEntity);
            _mockMapper.Setup(m => m.Map<CategoryDto>(categoryEntity)).Returns(categoryDto);

            // -------- Act --------
            var result = await _categoryController.Create(categoryDto);

            // -------- Assert --------
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);

            _mockCategoryService.Verify(s => s.Create(categoryEntity), Times.Once);
        }

        [InlineData("Télefonos", "Inactivo")]
        [InlineData("Accesorios", "Activo")]
        public async Task Create_WithInvalidModel_BadRequest(
            string description, string status
        )
        {
            // Arrange
            var categoryDto = new CategoryDto
            {
                CategoryId = 1,
                Description = description,
                Status = "Activo"
            };

            // Act
            var result = await _categoryController.Create(categoryDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var okResult = result as BadRequestObjectResult;
            okResult.StatusCode.Should().Be(400);
            _mockCategoryService.Verify(s => s.Create(It.IsAny<Category>()), Times.Never);
        }

        // ------------------------------------------
        // Test for GetById
        // ------------------------------------------
        [Fact]
        public async Task GetById_WithExistingId_ReturnsOk()
        {
            var categoryId = 1;
            var categoryEntity = new Category { CategoryId = categoryId, Description = "Accesorios", Status = "Activo" };
            var categoryDto = new CategoryDto { CategoryId = categoryId, Description = "Accesorios", Status = "Activo" };

            _mockCategoryService.Setup(s => s.GetById(categoryId)).ReturnsAsync(categoryEntity);
            _mockMapper.Setup(m => m.Map<CategoryDto>(categoryEntity)).Returns(categoryDto);

            // -------- Act --------
            var result = await _categoryController.GetById(categoryId);

            // -------- Assert --------
            // Verify that the ActionResult has a Result of type OkObjectResult
            result.Result.Should().NotBeNull().And.BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);

            // Verify the returned value
            var okResult = result.Result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(categoryDto);

            _mockCategoryService.Verify(s => s.GetById(categoryId), Times.Once);
            _mockMapper.Verify(m => m.Map<CategoryDto>(categoryEntity), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(999)]
        public async Task GetById_WithNonExistingId_ReturnsNotFound(int nonExistingId)
        {
            // -------- Arrange --------
            _mockCategoryService.Setup(s => s.GetById(nonExistingId)).ReturnsAsync((Category?)null);

            // -------- Act --------
            var result = await _categoryController.GetById(nonExistingId);

            // -------- Assert --------
            result.Result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundResult = result.Result as NotFoundObjectResult;
            notFoundResult.StatusCode.Should().Be(404);

            _mockCategoryService.Verify(s => s.GetById(nonExistingId), Times.Once);
            _mockMapper.Verify(m => m.Map<CategoryDto>(It.IsAny<Category>()), Times.Never);
        }

        // ------------------------------------------
        // Test for Get all
        // ------------------------------------------
        [Fact]
        public async Task GetAll_WithData_ReturnsOk()
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
                }
            };

            var categoriesDto = new List<CategoryDto>
            {
                new CategoryDto 
                { 
                    CategoryId = 1, 
                    Description = "Electrónicos", 
                    Status = "Activo" 
                },
                new CategoryDto 
                { 
                    CategoryId = 2, 
                    Description = "Ropa", 
                    Status = "Activo" 
                }
            };

            _mockCategoryService.Setup(s => s.GetAll()).ReturnsAsync(categories);
            _mockMapper.Setup(m => m.Map<List<CategoryDto>>(categories)).Returns(categoriesDto);

            // -------- Act --------
            var result = await _categoryController.GetAll();

            // -------- Assert --------
            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);

            var returnedCategories = okResult.Value as List<CategoryDto>;
            returnedCategories.Should().HaveCount(2);
            returnedCategories.Should().BeEquivalentTo(categoriesDto);

            _mockCategoryService.Verify(s => s.GetAll(), Times.Once);
            _mockMapper.Verify(m => m.Map<List<CategoryDto>>(categories), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenEmpty_ReturnsOkWithEmptyList()
        {
            // -------- Arrange --------
            var emptyCategories = new List<Category>();
            var emptyCategoriesDto = new List<CategoryDto>();

            _mockCategoryService.Setup(s => s.GetAll()).ReturnsAsync(emptyCategories);
            _mockMapper.Setup(m => m.Map<List<CategoryDto>>(emptyCategories)).Returns(emptyCategoriesDto);

            // -------- Act --------
            var result = await _categoryController.GetAll();

            // -------- Assert --------
            result.Result.Should().BeOfType<OkObjectResult>();

            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);

            var returnedCategories = okResult.Value as List<CategoryDto>;
            returnedCategories.Should().BeEmpty();

            _mockCategoryService.Verify(s => s.GetAll(), Times.Once);
            _mockMapper.Verify(m => m.Map<List<CategoryDto>>(emptyCategories), Times.Once);
        }

        // ------------------------------------------
        // Test for Update
        // ------------------------------------------
        [Fact]
        public async Task Update_WithValidData_ReturnsOk()
        {
            // -------- Arrange --------
            var categoryId = 1;
            var categoryDto = new CategoryDto
            {
                CategoryId = categoryId,
                Description = "Electrónicos Actualizados",
                Status = "Activo"
            };
            
            var categoryEntity = new Category
            {
                CategoryId = categoryId,
                Description = "Electrónicos Actualizados",
                Status = "Activo"
            };
            
            var updatedEntity = new Category
            {
                CategoryId = categoryId,
                Description = "Electrónicos Actualizados",
                Status = "Activo"
            };
            
            var updatedDto = new CategoryDto
            { 
                CategoryId = categoryId, 
                Description = "Electrónicos Actualizados", 
                Status = "Activo" 
            };

            _categoryController.ModelState.Clear();

            _mockMapper.Setup(m => m.Map<Category>(categoryDto)).Returns(categoryEntity);
            _mockCategoryService.Setup(s => s.Update(categoryEntity)).ReturnsAsync(updatedEntity);
            _mockMapper.Setup(m => m.Map<CategoryDto>(updatedEntity)).Returns(updatedDto);

            // -------- Act --------
            var result = await _categoryController.Update(categoryId, categoryDto);

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

            _mockCategoryService.Verify(s => s.Update(categoryEntity), Times.Once);
            _mockMapper.Verify(m => m.Map<Category>(categoryDto), Times.Once);
            _mockMapper.Verify(m => m.Map<CategoryDto>(updatedEntity), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task Update_WithNonExistingId_ReturnsBadRequest(int categoryId)
        {
            // -------- Arrange --------
            var categoryDto = new CategoryDto{ CategoryId = categoryId, Description = "Categoría Inexistente", Status = "Activo" };
            var categoryEntity = new Category { CategoryId = categoryId, Description = "Categoría Inexistente", Status = "Activo" };

            _mockMapper.Setup(m => m.Map<Category>(categoryDto)).Returns(categoryEntity);

            // Simulate that the service returns a false message that it did not find the category.
            _mockCategoryService.Setup(s => s.Update(categoryEntity)).ReturnsAsync((Category?)null);

            // -------- Act --------
            var result = await _categoryController.Update(categoryId, categoryDto);

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
            var categoryId = 1;
            _mockCategoryService.Setup(s => s.Delete(categoryId)).ReturnsAsync(true);

            // -------- Act --------
            var result = await _categoryController.Delete(categoryId);

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

            _mockCategoryService.Verify(s => s.Delete(categoryId), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(999)]
        public async Task Delete_WithNonExistingId_ReturnsNotFound(int nonExistingId)
        {
            // -------- Arrange --------
            _mockCategoryService.Setup(s => s.Delete(nonExistingId)).ReturnsAsync(false);

            // -------- Act --------
            var result = await _categoryController.Delete(nonExistingId);

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

            _mockCategoryService.Verify(s => s.Delete(nonExistingId), Times.Once);
        }
    }
}
