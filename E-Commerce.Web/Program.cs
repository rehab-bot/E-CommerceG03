
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Presistance;
using Presistance.Data.Contexts;
using Presistance.Repositories;
using ServiceAbstraction;
using Services;
using Services.MappingProfiles;

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
             builder.Services.AddSwaggerGen();
           builder.Services.AddDbContext<StoreDbContext>(options =>
              {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                  });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(config => config.AddProfile(new ProductProfile()),typeof(Services.AssemblyReference).Assembly);  
            builder.Services.AddScoped<IServiceManager ,ServiceManager>();
            var app = builder.Build();
            var Scope = app.Services.CreateScope();

           var seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            seed.DataSeedAsync();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}
