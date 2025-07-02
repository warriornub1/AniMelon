using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 30000000000; // Set to 30 GB
});
// Add CORS if needed (e.g., for cross-origin frontend access)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure static files with custom MIME types
var provider = new FileExtensionContentTypeProvider();
// Ensure .m3u8 files are served with the correct MIME type
provider.Mappings[".m3u8"] = "application/vnd.apple.mpegurl";
// Ensure .ts files are served correctly (optional, as this is usually default)
provider.Mappings[".ts"] = "video/mp2t";

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(builder.Environment.WebRootPath),
    ContentTypeProvider = provider
});

// Enable CORS if needed
app.UseCors("AllowAll");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

app.Run();