using Emitix.BillingService.Common;

namespace Emitix.BillingService.Services.External;

public class ProductServiceClient(HttpClient httpClient) : IProductServiceClient
{
    public async Task<Response<List<string>>> VerifyExistingCodes(string[] productCodes)
    {
        var response = await httpClient.PostAsJsonAsync($"/api/v1/products/verify-codes", productCodes);
        return (await response.Content.ReadFromJsonAsync<Response<List<string>>>())!;
    }
}