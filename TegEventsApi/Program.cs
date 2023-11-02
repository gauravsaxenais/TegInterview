using TegEvents.Framework.Blob;
using TegEventsApi.Entities;
using TegEventsApi.QueryManager;

namespace TegEventsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddRedisCache();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddTransient<IBlobManager<Root>, BlobManager<Root>>();
            builder.Services.AddScoped<IEventQueryManager, EventQueryManager>();
            builder.Services.AddScoped<IVenueQueryManager, VenueQueryManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}