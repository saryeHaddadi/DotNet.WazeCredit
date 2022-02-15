using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using WazeCredit.Models;
using WazeCredit.Models.ViewModels;
using WazeCredit.Service;
using WazeCredit.Service.Interfaces;
using WazeCredit.Service.LifeTimeExamples;
using WazeCredit.Utility.AppSettingsClasses;

namespace WazeCredit.Controllers;
public class LifeTimeController : Controller
{
	private readonly SingletonService _singletonService;
	private readonly ScopedService _scopedService;
	private readonly TransientService _transientService;

	public LifeTimeController(SingletonService singletonService, ScopedService scopedService, TransientService transientService)
	{
		_singletonService = singletonService;
		_scopedService = scopedService;
		_transientService = transientService;
	}

	public IActionResult Index()
	{
		var messages = new List<string>
		{
			HttpContext.Items["CustomMiddlewareSingleton"].ToString(),
			$"Singleton Controller - {_singletonService.GetGuidAsString()}",
			HttpContext.Items["CustomMiddlewareScoped"].ToString(),
			$"Scoped Controller - {_scopedService.GetGuidAsString()}",
			HttpContext.Items["CustomMiddlewareTransient"].ToString(),
			$"Transient Controller - {_transientService.GetGuidAsString()}"
		};

		// Remember, by default, use Transient!
		// It you think Scoped will do it, go for it,
		// Be carefull when using Singleton (stay the same during the lifetime of the app, and stays in the memory).

		return View(messages);
	}


}
