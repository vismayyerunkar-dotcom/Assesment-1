
using AvalphaTechnologies.CommissionCalculator.Core;
using AvalphaTechnologies.CommissionCalculator.Services;

namespace AvalphaTechnologies.CommissionCalculator
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


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowUI",
                    policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:3000") // Added this for development purpose
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });



            // registering dependencies
            builder.Services.AddCore()
                .AddCommissionServices();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowUI");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
