using Microsoft.EntityFrameworkCore;
using PosSystem.Model.Context;
using PosSystem.Model.Model;
using PosSystem.Repository.Interface;

namespace PosSystem.Repository.Repository
{
    public class DocumentNumberRepository : IDocumentNumberRepository
    {
        private readonly PosSystemContext _posSystemContext;

        public DocumentNumberRepository(PosSystemContext posSystemContext)
        {
            _posSystemContext = posSystemContext;
        }
        
        public async Task<DocumentNumber> Create(DocumentNumber documentNumber)
        {
            if(documentNumber == null)
            {
                throw new ArgumentNullException(nameof(documentNumber));
            }

            _posSystemContext.DocumentNumbers.Add(documentNumber);
            await _posSystemContext.SaveChangesAsync();
            return documentNumber;
        }

        public async Task<DocumentNumber?> Get()
        {
            return await _posSystemContext.DocumentNumbers.FirstOrDefaultAsync();
        }

        public async Task<DocumentNumber> Update(DocumentNumber documentNumber)
        {
            if (documentNumber == null)
            {
                throw new ArgumentNullException(nameof(documentNumber));
            }

            var documentNumberExists = await _posSystemContext.DocumentNumbers.FirstOrDefaultAsync(b => b.DocumentNumberId == documentNumber.DocumentNumberId);
            if(documentNumberExists == null)
            {
                throw new KeyNotFoundException($"No se encontró el documento con el ID: {documentNumber.DocumentNumberId}");
            }

            documentNumberExists.Document = documentNumber.Document;

            _posSystemContext.DocumentNumbers.Update(documentNumberExists);
            await _posSystemContext.SaveChangesAsync();
            return documentNumberExists;
        }
    }
}
