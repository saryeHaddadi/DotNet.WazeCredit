using WazeCredit.Utility.AppSettingsClasses;

namespace WazeCredit.Utility.DIConfig;

public static class AppSettingsDIConfig
{
	public static WebApplicationBuilder AddAppSettingsConfig(this WebApplicationBuilder builder)
	{
		builder.Services.Configure<WazeForecastSettings>(builder.Configuration.GetSection("WazeForecast"));
		builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGrid"));
		builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
		builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));

		return builder;
	}
}
