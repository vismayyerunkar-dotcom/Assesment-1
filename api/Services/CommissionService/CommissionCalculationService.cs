using AvalphaTechnologies.CommissionCalculator.Core;
using AvalphaTechnologies.CommissionCalculator.Dtos.CommissionCalculation;

namespace AvalphaTechnologies.CommissionCalculator.Services.CommissionService
{
    public class CommissionCalculationService : ICommissionCalculationService
    {
        private readonly CoreCommissionCalculator _avalphaCalculator;
        private readonly CoreCommissionCalculator _competitorCalculator;

        public CommissionCalculationService(
            AvalphaCommissionStratergy avalphaCommissionStratergy,
            CompetitorCommissionStratergy competitorCommissionStratergy)
        {
            _avalphaCalculator = new CoreCommissionCalculator(avalphaCommissionStratergy);
            _competitorCalculator = new CoreCommissionCalculator(competitorCommissionStratergy);
        }

        public CommissionDto Calculate(int localSalesCount, int foreignSalesCount,decimal averageSaleAmount)
        {
            try
            {
                // Input validation
                ValidateInputs(localSalesCount, foreignSalesCount, averageSaleAmount);

                decimal avalphaCommission = _avalphaCalculator.Calculate(
                    localSalesCount,
                    foreignSalesCount,
                    averageSaleAmount);

                decimal competitorCommission = _competitorCalculator.Calculate(
                    localSalesCount,
                    foreignSalesCount,
                    averageSaleAmount);

                return new CommissionDto
                {
                    AvalphaCommission = decimal.Round(avalphaCommission, 2),
                    CompetitorsCommission = decimal.Round(competitorCommission, 2)
                };
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while calculating commission.", ex);
            }
        }

        private static void ValidateInputs(int localSales,int foreignSales,decimal averageAmount){
            if (localSales < 0)
                throw new ArgumentException("Local sales count cannot be negative.");

            if (foreignSales < 0)
                throw new ArgumentException("Foreign sales count cannot be negative.");

            if (averageAmount < 0)
                throw new ArgumentException("Average sale amount cannot be negative.");
        }
    }
}
