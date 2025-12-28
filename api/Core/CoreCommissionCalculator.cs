
namespace AvalphaTechnologies.CommissionCalculator.Core
{
    public class CoreCommissionCalculator
    {
        private readonly ICommissionStratergy _commissionStratergy;

        public CoreCommissionCalculator(ICommissionStratergy commissionStratergy)
        {
            _commissionStratergy = commissionStratergy;
        }

        public decimal Calculate(int localSales, int foreignSales,decimal averageSaleAmount)
        {
            return _commissionStratergy.Calculate(localSales,foreignSales,averageSaleAmount);
        }
    }
}
