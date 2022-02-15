using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WazeCredit.Models;
using WazeCredit.Models.ViewModels;
using WazeCredit.Service;
using WazeCredit.Service.Interfaces;
using WazeCredit.Utility.AppSettingsClasses;

namespace WazeCredit.Controllers;
public class HomeController : Controller
{
	public HomeViewModel _homeVM { get; set; }
	private readonly IMarketForcaster _marketForecaster;
	private readonly StripeSettings _stripeOptions;
	private readonly SendGridSettings _sendGridOptions;
	private readonly TwilioSettings _twilioOptions;
	private readonly WazeForecastSettings _wazeOptions;
	[BindProperty]
	private LoanApplication LoanModel { get; set; }

	public HomeController(IMarketForcaster marketForecaster, IOptions<WazeForecastSettings> wazeOptions)
	{
		_homeVM = new HomeViewModel();
		_marketForecaster = marketForecaster;
		_wazeOptions = wazeOptions.Value;
	}

	public IActionResult Index()
	{
		var currentMarket = _marketForecaster.GetMarketPrediction();

		switch (currentMarket.MarketCondition)
		{
			case MarketCondition.StableDown:
				_homeVM.MarketForcast = "Market shows signs to go Down";
				break;
			case MarketCondition.StableUp:
				_homeVM.MarketForcast = "Market shows signs to go Up";
				break;
			case MarketCondition.Volatile:
				_homeVM.MarketForcast = "Market shows signs of Volatility";
				break;
			default:
				_homeVM.MarketForcast = "Apply for a discount!";
				break;
		}
		return View(_homeVM);
	}

	public IActionResult AllConfigSettings(
		[FromServices] IOptions<StripeSettings> stripeOptions,
		[FromServices] IOptions<SendGridSettings> sendGridOptions,
		[FromServices] IOptions<TwilioSettings> twilioOptions
		)
	{
		var messages = new List<string>();
		messages.Add($"Waze config: " + _wazeOptions.ForecastTrackerEnabled);
		messages.Add($"Stripe PubKey: " + stripeOptions.Value.PublishableKey);
		messages.Add($"Stripe SecretKey: " + stripeOptions.Value.SecretKey);
		messages.Add($"SendGrid Key: " + sendGridOptions.Value.SendGridKey);
		messages.Add($"Twilio Phone: " + twilioOptions.Value.PhoneNumber);
		messages.Add($"Twilio SID: " + twilioOptions.Value.AccountSid);
		messages.Add($"Twilio Token: " + twilioOptions.Value.AuthToken);

		return View(messages);
	}

	public IActionResult LoanApplication()
	{
		LoanModel = new LoanApplication();
		return View(LoanModel);
	}



	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
