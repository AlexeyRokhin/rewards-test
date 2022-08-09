using Rewards.Calculator;
using Rewards.Data.Csv;
using Rewards.Model;

namespace Rewards.Web
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
            builder.Services.AddSwaggerGen(options => options.SupportNonNullableReferenceTypes());

            var cf = builder.Configuration.GetSection(CsvConnectionOptions.SectionName).Get<CsvConnectionOptions>();
            builder.Services.AddSingleton(cf);

            builder.Services.AddSingleton<IRewardRuleRepository, RewardRuleFixedRepository>();
            builder.Services.AddScoped<ITransactionRepository, CsvTransactionRepository>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            builder.Services.AddSingleton(serviceProvider => serviceProvider.GetService<IRewardRuleRepository>().GetRewardRules());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            builder.Services.AddScoped<RewardsByCustomerAndMonthCalculator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
