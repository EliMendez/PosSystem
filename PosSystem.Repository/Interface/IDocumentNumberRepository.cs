using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Repository.Interface
{
    public interface IDocumentNumberRepository
    {
        Task<DocumentNumber?> Get();
        Task<DocumentNumber> Create(DocumentNumber documentNumber);
        Task<DocumentNumber> Update(DocumentNumber documentNumber);
    }
}
