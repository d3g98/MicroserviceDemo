using PlatformService.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public HttpCommandDataClient(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task SendPlatformToCommand(PlatformReadDto flat)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(flat),
                Encoding.UTF8,
                "application/json"
            );

            string url = $"{_configuration.GetSection("CommandService").Value}/api/c/platforms";
            var response = await _httpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync Post to CommandService was OK");
            }
            else
            {
                Console.WriteLine("--> Sync Post to CommandService was ERROR");
            }
        }
    }
}