using AvalphaTechnologies.CommissionCalculator.Constants;

namespace AvalphaTechnologies.CommissionCalculator.Core
{
    public class AvalphaCommissionStratergy: ICommissionStratergy
    {
        
        public decimal Calculate(int localSales,int foreignSales,decimal averageSaleAmount)
        {
            return (localSales * averageSaleAmount * CommissionPercentage.AvalphaLocal) +
                (foreignSales * averageSaleAmount * CommissionPercentage.AvalphaForeign);
        }
    }
}
