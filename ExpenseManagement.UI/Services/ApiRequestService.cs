namespace ExpenseManagement.UI.Services
{
    using System.Text;
    using System.Text.Json;

    namespace ExpenseManagement.UI.Services
    {
        public class ApiRequestService
        {
            private readonly HttpClient _httpClient;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ApiRequestService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
            {
                _httpClient = httpClient;
                _httpContextAccessor = httpContextAccessor;
            }

            private void AddAuthorizationHeader()
            {
                // jwt tokenini daha sonra ekleyecegiz

                //var token = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
                //if (!string.IsNullOrEmpty(token))
                //{
                //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //}
            }

            public async Task<T?> GetAsync<T>(string url)
            {
                AddAuthorizationHeader();
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
            {
                AddAuthorizationHeader();
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            public async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest data)
            {
                AddAuthorizationHeader();
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(url, content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            public async Task<bool> DeleteAsync(string url)
            {
                AddAuthorizationHeader();
                var response = await _httpClient.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
        }
    }

}
