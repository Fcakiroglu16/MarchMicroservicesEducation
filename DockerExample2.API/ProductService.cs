namespace DockerExample2.API
{
    public class ProductService(HttpClient http)
    {
        public async Task<List<Product>> GetProducts()
        {
            var response = await http.GetAsync("api/products");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<List<Product>>();
                return content;
            }

            throw new Exception("Failed to fetch products");
        }
    }

    public class Product
    {
        public int Id { get; set; }
    }
}
