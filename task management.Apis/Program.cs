using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using taskm.core.Repositories.Contract;
using taskm.Core.Entites;
using taskm.Repository;
using taskm.Repository.Data;

namespace taskmmanagement.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region configur services - Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<TaskDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }); //dependancy injection ...dbcontext 

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            var app = builder.Build();

            #region UpdateDataBase

            using
            var Scope = app.Services.CreateScope();
            var Service = Scope.ServiceProvider;
            var LoggerFactory = Service.GetService<ILoggerFactory>();
            try
            {
                var DbContext = Service.GetRequiredService<TaskDbContext>();
                await DbContext.Database.MigrateAsync(); //update database
                                                         //Scope.Dispose();
            }
            catch (Exception ex)
            {

                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occurde During Appling The Migration");
            }

            #endregion


            #region Configure - Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
