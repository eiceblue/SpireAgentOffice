using SpireAgentOffice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Register Spire AI Agent services
builder.Services.AddSingleton<SpireXlsService>();     // Excel
builder.Services.AddSingleton<SpireWordService>();     // Word
builder.Services.AddSingleton<SpirePdfService>();      // PDF
builder.Services.AddSingleton<SpirePptService>();      // PowerPoint

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Enable static file serving for generated output files (download URL support)
app.UseStaticFiles();

app.MapControllers();

// Ensure output directory exists for downloadable files
var outputDir = Path.Combine(app.Environment.WebRootPath ?? Path.Combine(app.Environment.ContentRootPath, "wwwroot"), "output");
Directory.CreateDirectory(outputDir);

app.Run();
