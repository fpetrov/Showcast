using MediatR;
using Microsoft.EntityFrameworkCore;
using Showcast.Application.Extensions;
using Showcast.Application.Middlewares;
using Showcast.Infrastructure;
using Showcast.Infrastructure.Contexts;
using Showcast.Infrastructure.Services.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

var connectionString = builder.Configuration.GetConnectionString("SupabasePostgreSQL");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDefaultAuthentication();

builder.Services.AddAutoMapper(typeof(IAssemblyMarker));
builder.Services.AddMediatR(typeof(IAssemblyMarker));

builder.Services.AddHttpClient<MovieDbService>();
builder.Services.AddHttpClient<RecommendationService>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseMiddleware<TelegramMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<AuthenticationMiddleware>();

app.MapControllers();

// Add instructions.
app.MapGet("/", () => "This is API for Showcast project, please return to the main page.");

app.Run();

// JWT: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJOYW1lIjoiRmVkb3IxIiwiUm9sZSI6IkRlZmF1bHQiLCJleHAiOjE2NzUzNTU0NDF9.LK99b7129Xeuzwi32fUhbxP3cGvIiUzfQEH7UQrpTYk
// Refresh Token: TAX/hlFgsl8sG8kSTgLmqHxUcHBd+slUx72sWDsU8McebieEGwcxOy3oXTZ+3stCmXqBVcyki16P7iRIdOjv4A==
// Fingerprint: Mozilla/5.0 (Windows; U; Windows NT 6.3) AppleWebKit/537.0.2 (KHTML, like Gecko) Chrome/25.0.898.0 Safari/537.0.2

//⚠ User is suspected to be part of an online terrorist organization. Please report any suspicious activity to Discord staff.