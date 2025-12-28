using AvalphaTechnologies.CommissionCalculator.Core;
using AvalphaTechnologies.CommissionCalculator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AvalphaTechnologies.CommissionCalculator.Tests.Base
{
    public abstract class TestBase
    {
        protected IServiceProvider ServiceProvider { get; private set; } = null!;

        protected void BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddCore();
            services.AddCommissionServices();

            ServiceProvider = services.BuildServiceProvider();
        }

        protected T GetRequiredService<T>() where T : notnull
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}
