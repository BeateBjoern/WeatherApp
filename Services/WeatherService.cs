
using Interfaces;
using Models;
using System.Xml.Serialization;
using Newtonsoft.Json;



namespace Services;

public class WeatherService : IWeatherService
{

    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponse> GetWeatherDataAsync(double latitude, double longitude)
    {
        try
        {
            string baseUrl = "https://api.met.no/weatherapi/locationforecast/2.0/compact";
            string url = $"{baseUrl}?lat={latitude}&lon={longitude}";
            string siteName = "CatsAndDogs"; // Change this to your site name
            string contactInfo = "Contact: ida.eaa.815@gmail.com"; // Change this to your contact info
            string userAgentValue = $"{siteName} ({contactInfo})";


            // Create HttpClient with custom User-Agent header
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgentValue);

                // Make request
                using (HttpResponseMessage response = await httpClient.GetAsync(url))
                {
                    // Check response status
                    response.EnsureSuccessStatusCode();

                    // Read JSON content
                    string json = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON into WeatherData object
                    WeatherResponse weatherData = JsonConvert.DeserializeObject<WeatherResponse>(json);

                    return weatherData;
                }
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle request failure
            throw new HttpRequestException($"Failed to retrieve weather data. {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            throw new Exception($"An error occurred while processing the request. {ex.Message}");
        }
    }












}