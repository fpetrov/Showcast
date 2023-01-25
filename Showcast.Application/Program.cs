using Showcast.Infrastructure;
using Showcast.Infrastructure.Services.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

// builder.Services.AddDbContext<>()

builder.Services.AddHttpClient<MovieDbService>();
builder.Services.AddHttpClient<RecommendationService>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Add instructions.
app.MapGet("/", () => "This is API for Showcast project, please return to the main page.");

app.Run();


//⚠ User is suspected to be part of an online terrorist organization. Please report any suspicious activity to Discord staff.