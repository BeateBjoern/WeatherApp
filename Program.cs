using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Interfaces;
using Microsoft.Extensions.Hosting;
using Models;
using System.Xml.Serialization;
using System.Xml.Schema;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddSingleton<IWeatherService, WeatherService>();
        builder.Services.AddHttpClient();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        // Create an instance of HttpClient
        using (HttpClient httpClient = new HttpClient())
        {
            // Create an instance of WeatherService with the HttpClient instance
            WeatherService weatherService = new WeatherService(httpClient);

            // Specify the latitude and longitude
            double latitude = 40.7128; // Example latitude (New York City)
            double longitude = -74.0060; // Example longitude (New York City)

            try
            {
                // Call the GetWeatherDataAsync method to fetch weather data
                WeatherResponse weatherData = weatherService.GetWeatherDataAsync(latitude, longitude).GetAwaiter().GetResult();

                // Display the weather data
                Console.WriteLine($"Weather data for Latitude: {latitude}, Longitude: {longitude}");
                Console.WriteLine($"Temperature: {weatherData.properties.timeseries[0].data.instant.details.air_temperature}°C");
                Console.WriteLine($"Wind Speed: {weatherData.properties.timeseries[0].data.instant.details.wind_speed} m/s");
                Console.WriteLine($"Wind Direction: {weatherData.properties.timeseries[0].data.instant.details.precipitation_amount}°");
                // You can display the weather data as per your requirement here


                foreach (var timeSeries in weatherData.properties.timeseries)
                {
                    // Access time and data properties of each TimeSeries object
                    DateTime time = timeSeries.time;
                    Data data = timeSeries.data;

                    // Access instant data
                    Details instantDetails = data.instant.details;
                    // Access next 1 hour data if available
                    Details next1HourDetails = data.next_1_hours?.details;
                    // Access next 6 hours data if available
                    Details next6HourDetails = data.next_6_hours?.details;

                    // Now you can access specific weather information from each Details object
                    if (instantDetails != null)
                    {
                        Console.WriteLine($"Time: {time}");
                        Console.WriteLine($"Temperature: {instantDetails.air_temperature}°C");
                    }

                    if (next1HourDetails != null)
                    {
                        Console.WriteLine($"Precipitation Amount: {next1HourDetails.precipitation_amount}");
                    }

                    if (next6HourDetails != null)
                    {
                        Console.WriteLine($"Precipitation Amount (Next 6 Hours): {next6HourDetails.precipitation_amount}"); 
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        app.Run();
    }
}