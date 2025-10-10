using PosSystem.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosSystem.Service.Interface
{
    public interface IDocumentNumberService
    {
        Task<DocumentNumber?> Get();
        Task<DocumentNumber> Save(DocumentNumber documentNumber);
    }
}
