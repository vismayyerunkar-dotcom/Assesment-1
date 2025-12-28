using AvalphaTechnologies.CommissionCalculator.Services.CommissionService;

namespace AvalphaTechnologies.CommissionCalculator.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommissionServices(
            this IServiceCollection services)
        {
            services.AddScoped<ICommissionCalculationService, CommissionCalculationService>();
            return services;
        }
    }
}
