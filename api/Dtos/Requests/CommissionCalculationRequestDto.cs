namespace AvalphaTechnologies.CommissionCalculator.Dtos.Requests
{
    public class CommissionCalculationRequestDto
    {
        public int LocalSalesCount { get; set; }
        public int ForeignSalesCount { get; set; }
        public decimal AverageSaleAmount { get; set; }
    }
}
