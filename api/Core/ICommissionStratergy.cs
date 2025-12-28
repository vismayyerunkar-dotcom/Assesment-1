namespace AvalphaTechnologies.CommissionCalculator.Core
{
    public interface ICommissionStratergy
    {
        decimal Calculate(int localSales,int foreignSales,decimal averageSaleAmount);
    }
}
