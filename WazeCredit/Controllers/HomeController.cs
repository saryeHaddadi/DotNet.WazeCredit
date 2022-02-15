using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WazeCredit.Data;
using WazeCredit.Models;
using WazeCredit.Models.ViewModels;
using WazeCredit.Service;
using WazeCredit.Service.Interfaces;
using WazeCredit.Utility.AppSettingsClasses;

namespace WazeCredit.Controllers;
public class HomeController : Controller
{
	public HomeViewModel _homeVM { get; set; }
	private readonly ILogger _logger;
	private readonly ApplicationDbContext _db;
	private readonly IMarketForcaster _marketForecaster;
	private readonly StripeSettings _stripeOptions;
	private readonly SendGridSettings _sendGridOptions;
	private readonly TwilioSettings _twilioOptions;
	private readonly WazeForecastSettings _wazeOptions;
	private readonly ILoanValidator _loanValidator;
	[BindProperty]
	public LoanApplication LoanModel { get; set; }

	public HomeController(ILogger<HomeController> logger,
		ApplicationDbContext db,
		IMarketForcaster marketForecaster,
		IOptions<WazeForecastSettings> wazeOptions,
		ILoanValidator loanValidator
		)
	{
		_homeVM = new HomeViewModel();
		_db = db;
		_logger = logger;
		_marketForecaster = marketForecaster;
		_wazeOptions = wazeOptions.Value;
		_loanValidator = loanValidator;
	}

	public IActionResult Index()
	{
		_logger.LogInformation("Home Controller Index Action Called");
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

		_logger.LogInformation("Home Controller Index Action Ended");
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

	[HttpPost]
	[ValidateAntiForgeryToken]
	[ActionName("LoanApplication")]
	public async Task<IActionResult> LoanApplicationPOST(
		[FromServices] Func<LoanApprovedEnum, ILoanApproved> _loanService)
	{
		if(ModelState.IsValid)
		{
			var (validationPassed, errorMessages) = await _loanValidator.PassAllValidations(LoanModel);
			var loanResult = new LoanResult()
			{
				ErrorList = errorMessages,
				CreditID = 0,
				Success = validationPassed
			};
			if (validationPassed)
			{
				LoanModel.CreditApproved = _loanService(
					LoanModel.Salary > 50000 ? 
					LoanApprovedEnum.High : LoanApprovedEnum.Low)
					.GetLoanApproved(LoanModel);
				_db.LoanApplicationModel.Add(LoanModel);
				_db.SaveChanges();
				loanResult.CreditID = LoanModel.Id;
				loanResult.CreditApproved = LoanModel.CreditApproved;
			}
			return RedirectToAction(nameof(LoanResult), loanResult);
		}
		return View(LoanModel);
	}

	public IActionResult LoanResult(LoanResult loanResult)
	{
		return View(loanResult);
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
