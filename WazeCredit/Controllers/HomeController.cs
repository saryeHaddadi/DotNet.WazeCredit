using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WazeCredit.Models;
using WazeCredit.Models.ViewModels;
using WazeCredit.Service;
using WazeCredit.Service.Interfaces;

namespace WazeCredit.Controllers;
public class HomeController : Controller
{
	public HomeViewModel _homeVM { get; set; }
	private readonly IMarketForcaster _marketForecaster;

	public HomeController(IMarketForcaster marketForecaster)
	{
		_homeVM = new HomeViewModel();
		_marketForecaster = marketForecaster;
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
