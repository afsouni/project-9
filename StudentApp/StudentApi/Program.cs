using Pomelo.EntityFrameworkCore.MySql;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;
using StudentApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var connStr = builder.Configuration.GetConnectionString("StudentsConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySql(connStr, ServerVersion.AutoDetect(connStr))
);

/*
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
*/

var app = builder.Build();

var frontendPath = Path.Combine(builder.Environment.ContentRootPath, "..", "frontend");
app.UseDefaultFiles(new DefaultFilesOptions
{
    FileProvider = new PhysicalFileProvider(frontendPath),
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(frontendPath),
});


// app.UseHttpsRedirection();

// app.UseCors("AllowAll");
app.MapControllers();
app.Run();
