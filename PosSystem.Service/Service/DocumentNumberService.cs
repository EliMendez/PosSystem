using PosSystem.Model.Model;
using PosSystem.Repository.Interface;
using PosSystem.Service.Interface;

namespace PosSystem.Service.Service
{
    public class DocumentNumberService: IDocumentNumberService
    {
        private readonly IDocumentNumberRepository _documentNumberRepository;

        public DocumentNumberService(IDocumentNumberRepository documentNumberRepository)
        {
            _documentNumberRepository = documentNumberRepository;
        }

        public async Task<DocumentNumber?> Get()
        {
            var document = await _documentNumberRepository.Get();

            if(document == null)
            {
                return null;
            }
            return document;
        }

        public async Task<DocumentNumber> Save(DocumentNumber documentNumber)
        {
            var documentNumberExists = await _documentNumberRepository.Get();
            if(documentNumberExists == null)
            {
                var document = await _documentNumberRepository.Create(documentNumber);
                return document;
            }
            else
            {
                documentNumber.DocumentNumberId = documentNumberExists.DocumentNumberId;
                var updatedDocumentNumber = await _documentNumberRepository.Update(documentNumber);
                return updatedDocumentNumber;
            }
        }
    }
}
