using AvalphaTechnologies.CommissionCalculator.Constants;

namespace AvalphaTechnologies.CommissionCalculator.Core
{
    public class CompetitorCommissionStratergy : ICommissionStratergy
    {
        public decimal Calculate(int localSales, int foreignSales,decimal averageSaleAmount)
        {
            return (localSales * averageSaleAmount * CommissionPercentage.CompetitorLocal) +
                (foreignSales * averageSaleAmount * CommissionPercentage.CompetitorForeign);
        }
    }
}
