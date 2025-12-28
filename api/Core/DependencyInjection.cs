namespace AvalphaTechnologies.CommissionCalculator.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ICommissionStratergy, AvalphaCommissionStratergy>();
            services.AddScoped<AvalphaCommissionStratergy>();
            services.AddScoped<CompetitorCommissionStratergy>();

            return services;
        }
    }

}
