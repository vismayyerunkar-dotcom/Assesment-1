using AvalphaTechnologies.CommissionCalculator.Dtos.CommissionCalculation;

namespace AvalphaTechnologies.CommissionCalculator.Services.CommissionService
{
    public interface ICommissionCalculationService
    {
        CommissionDto Calculate(int localSalesCount,int foreignSalesCount,decimal averageSaleAmount);
    }
}
