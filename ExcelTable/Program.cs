using ExcelTable;
using ExcelTable.DataAccess;
using ExcelTable.GraphQl;
using ExcelTable.Import;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddDbContextFactory<ExcelTableDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddGraphQLServer()
    .RegisterDbContextFactory<ExcelTableDbContext>()
    .AddQueryType<Query>();

var allowedOrigins = builder.Configuration["AllowedOrigins"]?
    .Split(',', StringSplitOptions.RemoveEmptyEntries)
    .Select(origin => origin.Trim())
    .ToArray() ?? [];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAntiforgery();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapGraphQL();
    
    var dbf = app.Services.GetRequiredService<IDbContextFactory<ExcelTableDbContext>>();
    await using var ctx = dbf.CreateDbContext();
    await ctx.Database.EnsureCreatedAsync();
    await ctx.SeedSampleData();
}

app.UseAntiforgery();
app.UseHttpsRedirection();
app.UseCors();

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

app.MapPost("/file", async (
    IFormFile file,
    ExcelTableDbContext dbContext) =>
{
    await dbContext.ImportExcel(file);
    return Results.Ok();
}).DisableAntiforgery();

await app.RunAsync();
