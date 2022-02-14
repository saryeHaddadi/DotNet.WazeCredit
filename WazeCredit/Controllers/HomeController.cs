using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WazeCredit.Models;
using WazeCredit.Models.ViewModels;
using WazeCredit.Service;

namespace WazeCredit.Controllers;
public class HomeController : Controller
{

	public IActionResult Index()
	{
		var homeVM = new HomeViewModel();
		var marketForecaster = new MarketForcaster();
		var currentMarket = marketForecaster.GetMarketPrediction();

		switch (currentMarket.MarketCondition)
		{
			case MarketCondition.StableDown:
				homeVM.MarketForcast = "Market shows signs to go Down";
				break;
			case MarketCondition.StableUp:
				homeVM.MarketForcast = "Market shows signs to go Up";
				break;
			case MarketCondition.Volatile:
				homeVM.MarketForcast = "Market shows signs of Volatility";
				break;
			default:
				homeVM.MarketForcast = "Apply for a discount!";
				break;
		}
		return View(homeVM);
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
