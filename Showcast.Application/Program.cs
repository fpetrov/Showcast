using MediatR;
using Microsoft.EntityFrameworkCore;
using Showcast.Application.Extensions;
using Showcast.Infrastructure;
using Showcast.Infrastructure.Contexts;
using Showcast.Infrastructure.Services.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

var connectionString = builder.Configuration.GetConnectionString("HerokuPostgreSQL");

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


//⚠ User is suspected to be part of an online terrorist organization. Please report any suspicious activity to Discord staff.