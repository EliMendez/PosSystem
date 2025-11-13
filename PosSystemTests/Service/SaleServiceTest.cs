using FluentAssertions;
using Moq;
using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;
using PosSystem.Service.Service;

namespace PosSystem.Tests.Service
{
    public class SaleServiceTest
    {
        private readonly Mock<ISaleRepository> _mockSaleRepo;
        private readonly ISaleService _saleService;

        public SaleServiceTest()
        {
            _mockSaleRepo = new Mock<ISaleRepository>();
            _saleService = new SaleService(_mockSaleRepo.Object);
        }

        //-------------------------------------------------------
        // Test for GetAll
        //-------------------------------------------------------
        [Fact]
        public async Task GetAll_WhenSalesExist_ReturnsList()
        {
            var sales = new List<Sale>
            {
                new Sale
                {
                    Bill = "0001",
                    SaleDate = DateTime.Now,
                    Dni = "012250067",
                    Customer = "Cliente",
                    Discount = 10,
                    Total = 100,
                    UserId = 1,
                },
                new Sale
                {
                    Bill = "0002",
                    SaleDate = DateTime.Now,
                    Dni = "012250067",
                    Customer = "Cliente",
                    Discount = 20,
                    Total = 350,
                    UserId = 1,
                }
            };

            _mockSaleRepo.Setup(repo => repo.GetAll()).ReturnsAsync(sales);
            var result = await _saleService.GetAll();

            result.Should().NotBeEmpty();

            _mockSaleRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        //-------------------------------------------------------
        // Test for SearchByDate
        //-------------------------------------------------------
        [Theory]
        [InlineData("2025-01-12", "2025-11-12")]
        [InlineData("2025-10-11", "2025-10-11")]
        public async Task SearchByDate_WhenSalesExist_ReturnsList(DateTime startDate, DateTime endDate)
        {
            var sales = new List<Sale>
            {
                new Sale
                {
                    Bill = "0001",
                    SaleDate = DateTime.Parse("2025-10-11"),
                    Dni = "012250067",
                    Customer = "Cliente",
                    Discount = 10,
                    Total = 100,
                    UserId = 1,
                },
                new Sale
                {
                    Bill = "0002",
                    SaleDate = DateTime.Parse("2025-11-11"),
                    Dni = "012250067",
                    Customer = "Cliente",
                    Discount = 20,
                    Total = 350,
                    UserId = 1,
                }
            };

            _mockSaleRepo.Setup(repo => repo.SearchByDate(startDate, endDate)).ReturnsAsync(sales.Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate).ToList());
            var result = await _saleService.SearchByDate(startDate, endDate);

            result.Should().NotBeEmpty();

            _mockSaleRepo.Verify(repo => repo.SearchByDate(startDate, endDate), Times.Once);
        }

        [Theory]
        [InlineData("2025-10-12", "2025-09-12")]
        public async Task SearchByDate_WhenStartDateIsLaterThanEndDate_ReturnsArgumentException(DateTime startDate, DateTime endDate)
        {
            // Act & Assert
            var result = await _saleService.SearchByDate(startDate, endDate);
            result.Should().BeNullOrEmpty();

            _mockSaleRepo.Verify(repo => repo.SearchByDate(startDate, endDate), Times.Once);
        }

        //---------------------------------------------
        // Test for CancelSale
        //---------------------------------------------
        [Theory]
        [InlineData(0, "Razon para anular", 1)]
        [InlineData(-2, "Razon para anular", 1)]
        [InlineData(1, "Razon para anular", 0)]
        [InlineData(1, "Razon para anular", -2)]
        public async Task CancelSale_WhenZeroOrNegativeId_ReturnsArgumentException(int saleId, string reason, int userId)
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _saleService.CancelSale(saleId, reason, userId));
            exception.Message.Should().Contain("ID no puede ser menor o igual a cero.");

            _mockSaleRepo.Verify(repo => repo.CancelSale(saleId, reason, userId), Times.Never);
        }

        [Theory]
        [InlineData(1, "", 1)]
        [InlineData(1, null, 1)]
        public async Task CancelSale_WhenReasonNull_ReturnsArgumentException(int saleId, string reason, int userId)
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _saleService.CancelSale(saleId, reason, userId));
            exception.Message.Should().Contain("La razón para anular la venta es requerida.");

            _mockSaleRepo.Verify(repo => repo.CancelSale(saleId, reason, userId), Times.Never);
        }

        //---------------------------------------------------
        // Test for GetDetailsBySaleId
        //---------------------------------------------------
        [Fact]
        public async Task GetDetailsBySaleId_WhenSaleId_ReturnsList()
        {
            int saleId = 1;

            var sales = new List<SaleDetail>
            {
                new SaleDetail
                {
                    SaleDetailId = 2,
                    SaleId = 1,
                    ProductId = 3,
                    ProductName = "Redmi Note 12s",
                    Price = 450,
                    Quantity = 1,
                    Discount = 0,
                    Total = 450
                }
            };
            _mockSaleRepo.Setup(repo => repo.GetDetailsBySaleId(saleId)).ReturnsAsync(sales);
            // Act & Assert
            var result = await _saleService.GetDetailsBySaleId(saleId);
            result.Should().NotBeNullOrEmpty();

            _mockSaleRepo.Verify(repo => repo.GetDetailsBySaleId(saleId), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetDetailsBySaleId_WhenZeroOrNegativeId_ReturnsArgumentException(int saleId)
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _saleService.GetDetailsBySaleId(saleId));
            exception.Message.Should().Contain("ID no puede ser menor o igual a cero.");

            _mockSaleRepo.Verify(repo => repo.GetDetailsBySaleId(saleId), Times.Never);
        }

        //---------------------------------------------------
        // Test for Create
        //---------------------------------------------------
        [Fact]
        public async Task Create_WhenValidData_ReturnsSale()
        {
            var sale = new Sale
            {
                Bill = "0001",
                SaleDate = DateTime.Now,
                Dni = "012250067",
                Customer = "Cliente",
                Discount = 10,
                Total = 250,
                UserId = 1,
                SaleDetails = new List<SaleDetail>
                {
                    new SaleDetail
                    {
                        SaleDetailId = 2,
                        SaleId = 3,
                        ProductId = 3,
                        ProductName = "Redmi Note 12s",
                        Price = 200,
                        Quantity = 1,
                        Discount = 10,
                        Total = 190
                    },
                    new SaleDetail
                    {
                        SaleDetailId = 2,
                        SaleId = 3,
                        ProductId = 3,
                        ProductName = "Audifonos Redmi",
                        Price = 60,
                        Quantity = 1,
                        Discount = 0,
                        Total = 60
                    },
                }
            };

            _mockSaleRepo.Setup(repo => repo.Create(sale)).ReturnsAsync(sale);
            var result = await _saleService.Create(sale);
            result.Should().NotBeNull();
            result.Should().BeSameAs(sale);

            _mockSaleRepo.Verify(repo => repo.Create(sale), Times.Once);
        }
    }
}