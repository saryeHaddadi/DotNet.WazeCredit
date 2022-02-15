using WazeCredit.Service.LifeTimeExamples;

namespace WazeCredit.Middleware;

public class CustomMiddleware
{
	private readonly RequestDelegate _next;

	public CustomMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context,
		SingletonService singletonService, ScopedService scopedService, TransientService transientService)
	{
		context.Items.Add("CustomMiddlewareSingleton", "Singleton Middleware - " + singletonService.GetGuidAsString());
		context.Items.Add("CustomMiddlewareScoped", "Scoped Middleware - " + scopedService.GetGuidAsString());
		context.Items.Add("CustomMiddlewareTransient", "Transient Middleware - " + transientService.GetGuidAsString());

		await _next(context);
	}
}
