using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services; 
using Interfaces;
using Models;
using System.Text.Json;


//Summary: 
// This file is a C# class that serves as the code-behind file for your Razor Page (Index.cshtml).
// It contains server-side logic for handling requests and preparing data to be rendered in the corresponding Razor Page.
// In your case, the IndexModel class is a PageModel class, which is a part of Razor Pages framework in ASP.NET Core.
// It typically contains methods like OnGet, OnPost, etc., which correspond to HTTP GET and POST requests, respectively.
// In your IndexModel class, you have an OnGetAsync method, which will be invoked when the page is accessed via an HTTP GET request.
// This method is where you can retrieve data from services,
// databases, etc., and prepare it to be displayed in the Razor Page.

namespace CatsAndDogs.Pages;

public class IndexModel : PageModel
{
     private readonly ILogger<IndexModel> _logger;

    private readonly IWeatherService _weatherService;

    // Define a property to hold the weather data
    public WeatherResponse WeatherData { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    public async Task OnGetAsync()
    {
        // Retrieve weather data from the weather service
        WeatherData = await _weatherService.GetWeatherDataAsync(56.15181, 10.10147);
    }
}
