using AMicroservice.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AMicroservice.Services;

public class StockService(HttpClient httpClient)
{
    public async Task<int> GetStockCount(int productId)
    {
        var response = await httpClient.GetAsync("/api/Stocks/2");


        if (response.IsSuccessStatusCode)
        {
            var responseAsContext = await response.Content.ReadFromJsonAsync<GetStockResponseModel>();

            return responseAsContext.StockCount;
        }

        var responseAsError = await response.Content.ReadFromJsonAsync<ProblemDetails>();


        throw new Exception(responseAsError.Title);
    }
}