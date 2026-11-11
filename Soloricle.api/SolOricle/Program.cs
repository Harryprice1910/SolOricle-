using SolOricle.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using SolOricle.Api.Data;
using SolOricle.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJournalService, DbJournalService>();

builder.Services.AddScoped<IAnalysisService, AnalysisService>(); // THIS IS MY RUDIMENTARY AI !!!!

//sql lite context -- redundant following azure implementation
//builder.Services.AddDbContext<SoloricleContext>(options =>
//  options.UseSqlite("Data Source=soloricle.db"));

// Azure SQL connection string. It is NOT stored in appsettings.json (it contains a password).
// Set it once per machine with:
//   dotnet user-secrets set "ConnectionStrings:SolOricleDatabase" "<connection string from Azure portal>"
var connectionString = builder.Configuration.GetConnectionString("SolOricleDatabase");
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("WARNING: ConnectionStrings:SolOricleDatabase is not set - database calls will fail. See Program.cs.");
}

builder.Services.AddDbContext<SolOricleContext>(options =>
    options.UseSqlServer(
        connectionString,
        // Azure SQL (especially serverless) can pause or drop connections briefly; retry instead of crashing.
        sql => sql.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)));

var app = builder.Build();





app.MapPost("/journal", async (CreateJournalEntryRequest request, IJournalService journal, CancellationToken ct) => // adding in endpoints directly in Program.cs for simplicity; in a real app, these would be in controllers
{
    var created = await journal.CreateAsync(request, ct);
    return Results.Created($"/journal/{created.Id}", created);
});

app.MapGet("/journal/latest", async (int count, IJournalService journal, CancellationToken ct) =>
{
    var latest = await journal.GetLatestAsync(count <= 0 ? 20 : count, ct);
    return Results.Ok(latest);
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
