// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();


//Exercise 1: The Blind Server (Middleware Ordering)
// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();
// app.Run();

// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();
// app.UseRouting();
// app.MapGet("/api/assessments/results", () => Results.Ok(new
// {
// courseCode = "CS-101",
// studentId = "S-001",
// letterGrade = "A"
// }));
// app.UseAuthentication();
// app.UseAuthorization();
// app.Run();

// Exercise 1B: Custom Request Logging Middleware
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Validate DI lifetimes
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

// Register the Training Authentication Scheme
builder.Services
    .AddAuthentication("Training")
    .AddScheme<AuthenticationSchemeOptions, TrainingAuthHandler>("Training", null);

// Register services
builder.Services.AddSingleton<EnrollmentWorker>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
// Exercise 3: Options Pattern and Configuration Validation
builder.Services
    .AddOptions<PaymentOptions>()
    .BindConfiguration("Payments")
    .ValidateDataAnnotations()
    .ValidateOnStart();

    builder.Services.AddAuthorization();
var app = builder.Build();



// Configure the middleware pipeline
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Existing endpoint
// app.MapGet("/api/assessments/results", () => Results.Ok(new
// {
//     courseCode = "CS-101",
//     studentId = "S-001",
//     letterGrade = "A"
// }))
app.MapGet("/test-log", async (IEnrollmentService service) =>
{
    await service.EnrollAsync("S-001", "CS-101");
    await service.EnrollAsync("S-001", "CS-101"); // duplicate
    await service.GetByIdAsync("does-not-exist");

    return Results.Ok("Logs generated");
});
//.RequireAuthorization();

// Temporary endpoint for Exercise 2
app.MapGet("/worker-test", (EnrollmentWorker worker) =>
{
    worker.ProcessBatch();
    return Results.Ok("Worker executed");
});

app.Run();
