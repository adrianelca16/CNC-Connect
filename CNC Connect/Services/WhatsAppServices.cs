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

    public async Task<bool> SendMessageWithImageAsync(string phoneNumber, string message, byte[]? image)
    {
        try
        {
            var content = new MultipartFormDataContent
        {
            { new StringContent(phoneNumber), "phoneNumber" },
            { new StringContent(message), "message" }
        };

            if (image != null)
            {
                var imageContent = new ByteArrayContent(image);
                imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                content.Add(imageContent, "image", "image.jpg");
            }
            Console.WriteLine(_httpClient.BaseAddress + "/send-image");
            var response = await _httpClient.PostAsync("send-image", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error from API: {response.StatusCode}, Details: {errorContent}");
            }

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            return false;
        }
    }


}
