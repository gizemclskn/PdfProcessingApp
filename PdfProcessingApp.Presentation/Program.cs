using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using PdfProcessingApp.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PdfProcessingApp.DAL;
using PdfProcessingApp.DAL.Repository;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PdfService>();

var app = builder.Build();

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();