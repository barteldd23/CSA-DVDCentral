using DDB.DVDCentral.PL2.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextPool<DVDCentralEntities>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"), builder => {
    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
}));

var app = builder.Build();
app.Run();