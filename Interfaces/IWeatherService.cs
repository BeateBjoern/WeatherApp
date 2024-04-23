using Models;
namespace Interfaces; 

public interface IWeatherService
{
    // Task<string> GetForecastAsync(double latitude, double longitude);
    Task<WeatherResponse> GetWeatherDataAsync(double latitude, double longitude);



    
}