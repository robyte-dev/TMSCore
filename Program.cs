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

// Register the Training Authentication Scheme
builder.Services
.AddAuthentication ("Training")
.AddScheme<AuthenticationSchemeOptions,
TrainingAuthHandler> ("Training", null);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the middleware pipeline
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Protect the endpoint
app.MapGet("/api/assessments/results", () => Results.Ok(new
{
    courseCode = "CS-101",
    studentId = "S-001",
    letterGrade = "A"
}))
.RequireAuthorization();

app.Run();
