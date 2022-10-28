using Showcast.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddGrpc();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Add instructions.
app.MapGet("/", () => "This is API for Showcast project, please return to the main page.");

// Configure gRPC Services.
app.MapGrpcService<GreeterService>();

app.Run();