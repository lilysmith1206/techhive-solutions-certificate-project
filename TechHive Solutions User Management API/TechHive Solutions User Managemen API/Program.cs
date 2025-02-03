using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TechHive_Solutions_User_Management_API.Middleware;
using TechHive_Solutions_User_Management_API.Repositories;

namespace TechHive_Solutions_User_Management_API
{
    public class HeaderTokenFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "token",
                In = ParameterLocation.Header,
                Required = false
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(config => {
                config.OperationFilter<HeaderTokenFilter>();
            });

            builder.Services.AddSingleton<IUserRepository, UserRepository>();

            builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler();

            app.Use((context, next) =>
            {
                StringValues stringValues = context.Request.Headers["token"];

                if (stringValues == "J1Y,-&Fk?U^6]0nFpP_>x7}^352W5;!")
                {
                    return next.Invoke();
                }
                else
                {
                    context.Response.StatusCode = 401;

                    return Task.CompletedTask;
                }
            });

            app.Use((context, next) =>
            {
                Console.WriteLine($"{context.Request.Method} - {context.Request.Path} - {context.Response.StatusCode}");

                return next.Invoke();
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
