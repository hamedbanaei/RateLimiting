using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RateLimiting.Controllers
{
	[ApiController]
	[Route("WeatherForecast")]
	[EnableRateLimiting("Fixed")]
	public class WeatherForecastController : ControllerBase
	{
		public static readonly string[] Summaries = new[]
			{ "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

		private readonly Microsoft.Extensions.Logging.ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(Microsoft.Extensions.Logging.ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		// Default method in which defined for the class applied to the method
		[HttpGet(nameof(this.GetWeatherForecastWithControllerDefaultRateLimiting))]
		public 
			System.Collections.Generic.IEnumerable<WeatherForecast> 
			GetWeatherForecastWithControllerDefaultRateLimiting()
		{
			return System.Linq.Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = System.DateOnly.FromDateTime(System.DateTime.Now.AddDays(index)),
				TemperatureC = System.Random.Shared.Next(-20, 55),
				Summary = Summaries[System.Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[EnableRateLimiting("Fixed")]
		[HttpGet(nameof(this.GetWeatherForecastWithFixedRateLimiting))]
		public 
			System.Collections.Generic.IEnumerable<WeatherForecast> 
			GetWeatherForecastWithFixedRateLimiting()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = System.DateOnly.FromDateTime(System.DateTime.Now.AddDays(index)),
				TemperatureC = System.Random.Shared.Next(-20, 55),
				Summary = Summaries[System.Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[EnableRateLimiting("Sliding")]
		[HttpGet(nameof(this.GetWeatherForecastWithSlidingRateLimiting))]
		public 
			System.Collections.Generic.IEnumerable<WeatherForecast> 
			GetWeatherForecastWithSlidingRateLimiting()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = System.DateOnly.FromDateTime(System.DateTime.Now.AddDays(index)),
				TemperatureC = System.Random.Shared.Next(-20, 55),
				Summary = Summaries[System.Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[EnableRateLimiting("Token")]
		[HttpGet(nameof(this.GetWeatherForecastWithTokenRateLimiting))]
		public 
			System.Collections.Generic.IEnumerable<WeatherForecast> 
			GetWeatherForecastWithTokenRateLimiting()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = System.DateOnly.FromDateTime(System.DateTime.Now.AddDays(index)),
				TemperatureC = System.Random.Shared.Next(-20, 55),
				Summary = Summaries[System.Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[EnableRateLimiting("Concurrency")]
		[HttpGet(nameof(this.GetWeatherForecastWithConcurrencyRateLimiting))]
		public 
			System.Collections.Generic.IEnumerable<WeatherForecast> 
			GetWeatherForecastWithConcurrencyRateLimiting()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = System.DateOnly.FromDateTime(System.DateTime.Now.AddDays(index)),
				TemperatureC = System.Random.Shared.Next(-20, 55),
				Summary = Summaries[System.Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[DisableRateLimiting]
		[HttpGet(nameof(this.GetWeatherForecastWithDisabledRateLimiting))]
		public 
			System.Collections.Generic.IEnumerable<WeatherForecast> 
			GetWeatherForecastWithDisabledRateLimiting()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = System.DateOnly.FromDateTime(System.DateTime.Now.AddDays(index)),
				TemperatureC = System.Random.Shared.Next(-20, 55),
				Summary = Summaries[System.Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}
