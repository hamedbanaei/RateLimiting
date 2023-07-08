using Microsoft.AspNetCore.Builder;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;

var builder =
	Microsoft.AspNetCore.Builder
	.WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddRateLimiter(configureOptions: rateLimiterOptions =>
{
	rateLimiterOptions.RejectionStatusCode =
		Microsoft.AspNetCore.Http.StatusCodes.Status429TooManyRequests;

	// Fixed Window Limiter
	rateLimiterOptions.AddFixedWindowLimiter(policyName: "Fixed", configureOptions: options =>
	{
		options.PermitLimit = 10;
		options.Window = System.TimeSpan.FromSeconds(10);
		options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
		options.QueueLimit = 5;
	});

	// Fixed Window Limiter for testing Minimal Api
	rateLimiterOptions.AddFixedWindowLimiter(policyName: "FixedForMinimalApi", configureOptions: options =>
	{
		options.PermitLimit = 3;
		options.Window = System.TimeSpan.FromSeconds(10);
		options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
		options.QueueLimit = 0;
	});

	// Sliding Window Limiter
	rateLimiterOptions.AddSlidingWindowLimiter(policyName: "Sliding", configureOptions: options =>
	{
		options.PermitLimit = 10;
		options.Window = System.TimeSpan.FromSeconds(10);
		options.SegmentsPerWindow = 2;
		options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
		options.QueueLimit = 5;
	});

	// Token Bucket Limiter
	rateLimiterOptions.AddTokenBucketLimiter(policyName: "Token", configureOptions: options =>
	{
		options.TokenLimit = 100;
		options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
		options.QueueLimit = 5;
		options.ReplenishmentPeriod = System.TimeSpan.FromSeconds(10);
		options.TokensPerPeriod = 20;
		options.AutoReplenishment = true;
	});

	// Concurrency Limiter
	rateLimiterOptions.AddConcurrencyLimiter(policyName: "Concurrency", configureOptions: options =>
	{
		options.PermitLimit = 10;
		options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
		options.QueueLimit = 5;
	});
});

var app = builder.Build();

app.UseRateLimiter();

app.Map(pattern: "/", handler: () => "Hello Rate Limiting World!");


// Minimal Api Sample
app.Map(pattern: "/aboutAuther", handler: () => "Hi, I'am Hamed Banaei.")
	.RequireRateLimiting("FixedForMinimalApi");


app.MapControllers();

app.Run();