using Microsoft.Extensions.DependencyInjection.Extensions;
using WazeCredit.Models;
using WazeCredit.Service;
using WazeCredit.Service.Interfaces;
using WazeCredit.Service.LifeTimeExamples;

namespace WazeCredit.Utility.DIConfig;

public static class ConfigureDIServices
{
	public static WebApplicationBuilder AddAllServices(this WebApplicationBuilder builder)
	{
		/// <summary>
		/// First DI!
		/// </summary>
		builder.Services.AddTransient<IMarketForcaster, MarketForcaster>();
		//builder.Services.AddTransient<IMarketForcaster, MarketForcasterV2>();

		/// <summary>
		/// LifeTime Examples
		/// </summary>
		builder.Services.AddSingleton<SingletonService>();
		builder.Services.AddScoped<ScopedService>();
		builder.Services.AddTransient<TransientService>();

		// Other ways of registering services 
		//builder.Services.AddSingleton(new MarketForcaster());
		//builder.Services.AddSingleton<IMarketForcaster, MarketForcaster>();
		//builder.Services.AddSingleton<IMarketForcaster>(new MarketForcaster());
		//builder.Services.AddTransient(typeof(IMarketForcaster), typeof(MarketForcaster));

		// Making sur that an implementation is not registered two time
		// for the same interface
		//builder.Services.TryAddTransient<IMarketForcaster, MarketForcaster>();
		//builder.Services.TryAddTransient<IMarketForcaster, MarketForcasterV2>();
		//builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IMarketForcaster, MarketForcasterV2>());
		// Replace, Remove
		//builder.Services.Replace(ServiceDescriptor.Transient<IMarketForcaster, MarketForcasterV2>());
		//builder.Services.RemoveAll<IMarketForcaster>();

		//builder.Services.AddScoped<IValidationChecker, AddressValidationChecker>();
		//builder.Services.AddScoped<IValidationChecker, LoanValidationChecker>();
		//builder.Services.AddScoped<ILoanValidator, LoanValidator>();

		// Generic way, which prevent duplicates
		builder.Services.TryAddEnumerable(new ServiceDescriptor[]
		{
			ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>(),
			ServiceDescriptor.Scoped<IValidationChecker, LoanValidationChecker>(),
			ServiceDescriptor.Scoped<ILoanValidator, LoanValidator>()
		});

		builder.Services.AddScoped<LoanApprovedHigh>();
		builder.Services.AddScoped<LoanApprovedLow>();
		builder.Services.AddScoped<Func<LoanApprovedEnum, ILoanApproved>>(ServiceProvider => range =>
		{
			switch (range)
			{
				case LoanApprovedEnum.High: return ServiceProvider.GetService<LoanApprovedHigh>();
				case LoanApprovedEnum.Low: return ServiceProvider.GetService<LoanApprovedLow>();
				default: return ServiceProvider.GetService<LoanApprovedLow>();
			}
		});

		return builder;
	}
}
}
