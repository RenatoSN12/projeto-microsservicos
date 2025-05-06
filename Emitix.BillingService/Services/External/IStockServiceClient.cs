using Emitix.BillingService.Common;
using Emitix.BillingService.DTOs.Requests;

namespace Emitix.BillingService.Services.External;

public interface IStockServiceClient
{
    Task<Response<bool>> VerifiyStockAvailability(List<StockAvailabilityRequest> request);
}