	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.SignalR;

	namespace WebApplication1.Controllers
	{
		[ApiController]
		[Route("[controller]")]
		public class WeatherForecastController : ControllerBase
		{
			private static readonly string[] Summaries = new[]
			{
				"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
			};

			private readonly ILogger<WeatherForecastController> _logger;
			private readonly IHubContext<MessageHub, IMessageHubClient> messageHub;

			public WeatherForecastController(ILogger<WeatherForecastController> logger, IHubContext<MessageHub, IMessageHubClient> _messageHub)
			{
				_logger = logger;
				messageHub = _messageHub;
			}



			[HttpGet(Name = "GetWeatherForecast")]
			public IEnumerable<WeatherForecast> Get()
			{
				//List<string> offers = new List<string>();
				//offers.Add("20% Off on IPhone 12");
				//offers.Add("15% Off on HP Pavillion");
				//offers.Add("25% Off on Samsung Smart TV");
				messageHub.Clients.All.SendOffersToUser("QQQQQQ"+ Guid.NewGuid().ToString());
				return Enumerable.Range(1, 5).Select(index => new WeatherForecast
				{
					Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
					TemperatureC = Random.Shared.Next(-20, 55),
					Summary = Summaries[Random.Shared.Next(Summaries.Length)]
				})
				.ToArray();
			}

			public IEnumerable<WeatherForecast2> Getdata()
			{
				//List<string> offers = new List<string>();
				//offers.Add("20% Off on IPhone 12");
				//offers.Add("15% Off on HP Pavillion");
				//offers.Add("25% Off on Samsung Smart TV");
				messageHub.Clients.All.SendOffersToUser("QQQQQQ" + Guid.NewGuid().ToString());

				 return Enumerable.Range(1, 5).Select(index => new WeatherForecast2
				{
					Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
					TemperatureC = Random.Shared.Next(-20, 55),
					Summary = Summaries[Random.Shared.Next(Summaries.Length)]
				})
			}

			public IEnumerable<WeatherForecast3> Getdata3()

			{
				//List<string> offers = new List<string>();
				//offers.Add("20% Off on IPhone 12");
				//offers.Add("15% Off on HP Pavillion");
				//offers.Add("25% Off on Samsung Smart TV");
				messageHub.Clients.All.SendOffersToUser("QQQQQQ" + Guid.NewGuid().ToString());

				 return Enumerable.Range(1, 5).Select(index => new WeatherForecast3
				{
					Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
					TemperatureC = Random.Shared.Next(-20, 55),
					Summary = Summaries[Random.Shared.Next(Summaries.Length)]
				})
				.ToArray();
			}

		}
	}
