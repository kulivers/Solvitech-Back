using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;

namespace Solvintech.Services.Contracts
{
    public interface IQuotationProxyService
    {
        Task<IEnumerable<ValCursValute>> GetJsonQuotatiation(StringDate stringDate);
    }
}