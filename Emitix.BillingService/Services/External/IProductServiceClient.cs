using Emitix.BillingService.Common;

namespace Emitix.BillingService.Services.External;

public interface IProductServiceClient
{
    Task<Response<List<string>>> VerifyExistingCodes(string[] productCode);
}