using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class WhatsAppService
{
    private readonly HttpClient _httpClient;

    public WhatsAppService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("WhatsAppClient");
    }

    public async Task<bool> SendMessageAsync(string phoneNumber, string message)
    {
        var requestData = new
        {
            numberPhone = phoneNumber,
            message = message
        };

        // Reemplaza "/send-message" con el endpoint correcto de tu API de WhatsApp
        var response = await _httpClient.PostAsJsonAsync("/send-message", requestData);

        return response.IsSuccessStatusCode;
    }
}
