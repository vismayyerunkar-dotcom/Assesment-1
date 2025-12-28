using AvalphaTechnologies.CommissionCalculator.Dtos.Requests;
using AvalphaTechnologies.CommissionCalculator.Dtos.Responses;
using AvalphaTechnologies.CommissionCalculator.Services.CommissionService;
using Microsoft.AspNetCore.Mvc;

namespace AvalphaTechnologies.CommissionCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommisionController : ControllerBase
    {
        private readonly ICommissionCalculationService _commissionCalculationService;

        public CommisionController(
            ICommissionCalculationService commissionCalculationService)
        {
            _commissionCalculationService = commissionCalculationService;
        }

        [HttpPost("calculate")]
        [ProducesResponseType(typeof(CommissionCalculationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Calculate(
            [FromBody] CommissionCalculationRequestDto calculationRequest)
        {
            if (calculationRequest == null)
                return BadRequest("Request body cannot be null");

            var result = _commissionCalculationService.Calculate(
                calculationRequest.LocalSalesCount,
                calculationRequest.ForeignSalesCount,
                calculationRequest.AverageSaleAmount);

            var response = new CommissionCalculationResponseDto
            {
                AvalphaTechnologiesCommissionAmount = result.AvalphaCommission,
                CompetitorCommissionAmount = result.CompetitorsCommission
            };

            return Ok(response);
        }
    }
}

