using Soloricle.Core.Contracts;
using Soloricle.Api.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IJournalService, InMemoryJournalService>();


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
