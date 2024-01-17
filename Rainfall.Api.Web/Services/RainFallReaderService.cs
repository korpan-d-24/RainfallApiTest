using System.Net;
using Newtonsoft.Json;
using Rainfall.Api.Models;

namespace Rainfall.Api.Web.Services;

public class RainFallReaderService : IRainFallReaderService
{
    private readonly HttpClient _httpClient;
    private const string Url = "id/stations/{0}/readings?_sorted&parameter=rainfall&_limit={1}";

    public RainFallReaderService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient.CreateClient("RainFallApiBaseUrl");
    }

    public async Task<RainfallReadingResponse> GetRainfallReadings(string stationId, int count = 10)
    {
        if (stationId == null)
            throw new ArgumentNullException(nameof(stationId));

        string url = $"id/stations/{stationId}/readings?_sorted&parameter=rainfall&_limit={count}";

        try
        {
            RainfallReadingResponse rainfallReadingResponse = new();
            var response = await _httpClient.GetAsync(url);

            if(response.StatusCode == HttpStatusCode.OK)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                rainfallReadingResponse = JsonConvert.DeserializeObject<RainfallReadingResponse>(responseData) ?? throw new Exception();
            }
            return rainfallReadingResponse;
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }
}