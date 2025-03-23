using AMicroservice.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AMicroservice.Services;

public class XService(IHttpClientFactory factory)
{
    public async Task<int> GetStockCount(int productId)
    {
        var client = factory.CreateClient("AMicroservice");

        // token

        if (true) client.DefaultRequestHeaders.Add("Authorization,", "Bearer token");

        var response = await client.GetAsync("/api/Stocks/2");
        if (response.IsSuccessStatusCode)
        {
            var responseAsContext = await response.Content.ReadFromJsonAsync<GetStockResponseModel>();
            return responseAsContext.StockCount;
        }

        var responseAsError = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        throw new Exception(responseAsError.Title);
    }
}