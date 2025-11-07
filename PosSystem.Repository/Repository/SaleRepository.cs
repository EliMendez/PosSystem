using Microsoft.EntityFrameworkCore;
using PosSystem.Model.Context;
using PosSystem.Model.Model;
using PosSystem.Repository.Interface;

namespace PosSystem.Repository.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly PosSystemContext _posSystemContext;
        private readonly IDocumentNumberRepository _documentNumberRepository;

        public SaleRepository(PosSystemContext posSystemContext, IDocumentNumberRepository documentNumberRepository)
        {
            _posSystemContext = posSystemContext;
            _documentNumberRepository = documentNumberRepository;
        }

        public async Task<Sale?> CancelSale(int saleId, string reason, int userId)
        {
            var sale = await _posSystemContext.Sales
                    .Include(v => v.SaleDetails)
                    .FirstOrDefaultAsync(v => v.SaleId == saleId);

            if (sale == null)
            {
                throw new KeyNotFoundException($"La venta con el ID: {saleId} no existe o fue eliminada.");
            }

            // Update Sale
            sale.Status = SaleStatus.Annulled;
            sale.AnnulledDate = DateTime.Today;
            sale.Reason = reason;
            sale.UserCancel = userId;

            // Update product stock
            if(sale.SaleDetails?.Any() == true)
            {
                foreach(var detail in sale.SaleDetails)
                {
                    var product = await _posSystemContext.Products
                        .FindAsync(detail.ProductId);

                    if (product != null)
                    {
                        product.Stock += detail.Quantity;
                    }
                }
            }

            await _posSystemContext.SaveChangesAsync();
            return sale;
        }

        public async Task<Sale> Create(Sale sale)
        {
            using var transaction = await _posSystemContext.Database.BeginTransactionAsync();

            try
            {
                var documentNumber = await _documentNumberRepository.Get();
                string nextDocument;

                if (documentNumber == null)
                {
                    nextDocument = "0001";
                    var newDocumentNumber = new DocumentNumber { Document = nextDocument };
                    await _documentNumberRepository.Create(newDocumentNumber);
                }
                else
                {
                    var currentDocument = int.Parse(documentNumber.Document);
                    nextDocument = (currentDocument + 1).ToString("D4");
                }

                sale.Bill = nextDocument;
                if(string.IsNullOrEmpty(sale.Bill))
                {
                    throw new InvalidOperationException("No se puede generar el número de factura. Intentelo de nuevo");
                }

                if(sale.SaleDetails?.Any() == true)
                {
                    await UpdateStock(sale.SaleDetails.ToList());
                }

                _posSystemContext.Add(sale);
                await _posSystemContext.SaveChangesAsync();
                
                if(documentNumber != null)
                {
                    documentNumber.Document = nextDocument;
                    await _documentNumberRepository.Update(documentNumber);
                }

                await transaction.CommitAsync();
                return sale;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Ocurrio un error al registrar la venta.", ex);
            }
        }

        public async Task<List<Sale>> GetAll()
        {
            return await _posSystemContext.Sales
                .Include(s => s.SaleDetails)
                .ToListAsync();
        }

        public async Task<List<SaleDetail>> GetDetailsBySaleId(int saleId)
        {
            var saleDetails = await _posSystemContext.SaleDetails
                .Where(sd => sd.SaleId == saleId)
                .ToListAsync();

            if(saleDetails.Count == 0)
            {
                throw new InvalidOperationException($"No se encontraron detalles de venta para el ID: {saleId}." +
                    $"Verifica que el ID es correcto.");
            }

            return saleDetails;
        }

        public async Task<List<Sale>> SearchByDate(DateTime startDate, DateTime endDate)
        {
            return await _posSystemContext.Sales
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .ToListAsync();
        }

        public async Task UpdateStock(List<SaleDetail> saleDetails)
        {
            if(saleDetails == null)
            {
                throw new ("La lista de detalles de venta está vacía.");
            }

            foreach (var detail in saleDetails)
            {
                var product = await _posSystemContext.Products
                    .AsTracking()
                    .FirstOrDefaultAsync(p => p.ProductId == detail.ProductId);

                if(product == null)
                {
                    throw new KeyNotFoundException($"El producto con el ID: {detail.ProductId} " +
                        $"no existe en el inventario.");
                }

                if(product.Stock < detail.Quantity)
                {
                    throw new InvalidOperationException($"No hay suficiente stock para el producto: {product.Description} " +
                        $"Stock disponible: {product.Stock}, cantidad solicitada: {detail.Quantity}.");
                }

                product.Stock -= detail.Quantity;
            }

            await _posSystemContext.SaveChangesAsync();
        }
    }
}
