using CodeRunner.Properties;
using CodeRunner.Services;
using System.IO.Compression;

var pythonPath = InitPython();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddSingleton<ICodeRunnerService>((x) =>
    new CodeRunnerService(x.GetRequiredService<ILogger<CodeRunnerService>>(), pythonPath)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

static string InitPython()
{
    string pythonZipPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "python.zip");
    string pythonFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "python");
    string pythonBinPath = Path.Combine(pythonFolderPath, "python.exe");
    if (File.Exists(pythonBinPath)) return pythonBinPath;

    using (FileStream fsDst = new(pythonZipPath, FileMode.Create, FileAccess.Write))
    {
        byte[] bytes = Resources.python_3_10_5_embed_amd64;
        fsDst.Write(bytes, 0, bytes.Length);
    }
    ZipFile.ExtractToDirectory(pythonZipPath, pythonFolderPath, false);

    return pythonBinPath;
}