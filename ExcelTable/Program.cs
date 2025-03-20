using ExcelTable;
using ExcelTable.DataAccess;
using ExcelTable.GraphQl;
using Microsoft.EntityFrameworkCore;

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

app.UseHttpsRedirection();

await app.RunAsync();
