using Emitix.BillingService.Common;
using Emitix.BillingService.DTOs.Requests;

namespace Emitix.BillingService.Services.External;

public class StockServiceClient(HttpClient httpClient) : IStockServiceClient
{
    public async Task<Response<bool>> VerifiyStockAvailability(List<StockAvailabilityRequest> request)
    {
        var response = await httpClient.PostAsJsonAsync($"/api/v1/stock/verify", request);
        return (await response.Content.ReadFromJsonAsync<Response<bool>>())!;
    }
}