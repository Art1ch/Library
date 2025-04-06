using LibraryService.API.Extensions;
using Microsoft.AspNetCore.Authentication.Negotiate;
using System.Text.Json.Serialization;
using System.Text.Json;
using LibraryService.API.Middlewares;

namespace LibraryService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.
                AddDbContext().
                AddJwtAuthentication().
                AddConverters().
                AddRepositories().
                AddAdditionalServices().
                AddMainServices().
                AddMapping().
                AddValidation().
                AddCommandsAndQueries();


            var app = builder.Build();
            app.UseMiddleware<GlobalExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }   
}
