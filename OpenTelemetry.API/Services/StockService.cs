namespace OpenTelemetryAPI.Services
{
    public class StockService(HttpClient httpClient)
    {
        public async Task GetStockPrice()
        {
            var response = await httpClient.GetAsync("/api/stock");
            response.EnsureSuccessStatusCode();
            await response.Content.ReadAsStringAsync();
        }
    }
}
