using MediatR;
using Microsoft.EntityFrameworkCore;
using Showcast.Application.Extensions;
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Add instructions.
app.MapGet("/", () => "This is API for Showcast project, please return to the main page.");

app.Run();

// JWT: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJOYW1lIjoiRmVkb3IiLCJSb2xlIjoiRGVmYXVsdCIsImV4cCI6MTY3NTAzNzY3OH0.QlxLggjntcR_Sk0T-aO5kSZ_8aW-6wPgtLLjYdye2PE
// Refresh Token: 3/zrFaFs4DN94zJKy81ajmydi6CamLl4cnS4lpFFa8FpdFR6PzvGObR6njSvUaT+a5+h+lL6LOgkx5DZ0BA8cg==
// Fingerprint: Mozilla/5.0 (Windows NT 6.1; rv:8.9) Gecko/20100101 Firefox/8.9.0

//⚠ User is suspected to be part of an online terrorist organization. Please report any suspicious activity to Discord staff.