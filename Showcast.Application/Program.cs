using Showcast.Infrastructure;
using Showcast.Infrastructure.Http;
using Showcast.Infrastructure.Services.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddControllers();

// Configure gRPC services.
builder.Services.AddGrpcClient<Recommender.RecommenderClient>(options =>
{
    options.Address = new Uri("http://localhost:5001");
});

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

// builder.Services.AddDbContext<>()

builder.Services.AddHttpClient<MovieDbClient>();

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