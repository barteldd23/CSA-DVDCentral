using DDB.DVDCentral.API.Hubs;
using DDB.DVDCentral.PL2.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddSignalR()
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "DVDCentral API",
                Version = "v1",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name  = "Dean Bartel",
                    Email = "barteldd23@gmail.com",
                    Url = new Uri("https://ww.fvtc.edu")
                }
            });

            var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile) ;
            c.IncludeXmlComments(xmlpath);

           // var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
           //c.IncludeXmlComments(xmlpath);
        });


        

        // Add Connection information
        builder.Services.AddDbContextPool<DVDCentralEntities>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
            options.UseLazyLoadingProxies();
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || true)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        //app.MapControllers();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<BingoHub>("/bingoHub");
        });


        app.Run();
    }
}