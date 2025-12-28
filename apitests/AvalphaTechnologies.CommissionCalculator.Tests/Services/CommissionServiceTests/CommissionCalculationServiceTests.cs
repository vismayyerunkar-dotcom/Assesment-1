using AvalphaTechnologies.CommissionCalculator.Constants;
using AvalphaTechnologies.CommissionCalculator.Dtos.CommissionCalculation;
using AvalphaTechnologies.CommissionCalculator.Services.CommissionService;
using AvalphaTechnologies.CommissionCalculator.Tests.Base;
using NUnit.Framework;

namespace AvalphaTechnologies.CommissionCalculator.Tests.Services.CommissionServiceTests
{
    [TestFixture]
    public class CommissionCalculationServiceTests : TestBase
    {
        private ICommissionCalculationService _commissionCalculationService = null!;

        [SetUp]
        public void Setup()
        {
            BuildServiceProvider();

            _commissionCalculationService = GetRequiredService<ICommissionCalculationService>();
        }

        [Test]
        public void CalculateCommissionWithValidInput_ReturnsCorrectCommission()
        {
            int localSales = 20;
            int foreignSales = 5;
            decimal avgAmount = 100m;

            CommissionDto result =
                _commissionCalculationService.Calculate(localSales, foreignSales, avgAmount);

            decimal expectedAvalpha = decimal.Round(
                (localSales * avgAmount * CommissionPercentage.AvalphaLocal) +
                (foreignSales * avgAmount * CommissionPercentage.AvalphaForeign), 2);

            decimal expectedCompetitor = decimal.Round(
                (localSales * avgAmount * CommissionPercentage.CompetitorLocal) +
                (foreignSales * avgAmount * CommissionPercentage.CompetitorForeign), 2);

            Assert.That(result.AvalphaCommission, Is.EqualTo(expectedAvalpha));
            Assert.That(result.CompetitorsCommission, Is.EqualTo(expectedCompetitor));
        }

        [Test]
        public void Calculate_ZeroSales_ReturnsZeroCommission()
        {
            var result = _commissionCalculationService.Calculate(0, 0, 1000m);

            Assert.That(result.AvalphaCommission, Is.EqualTo(0m));
            Assert.That(result.CompetitorsCommission, Is.EqualTo(0m));
        }

        [Test]
        public void Calculate_OnlyLocalSales_ReturnsCorrectCommission()
        {
            int localSales = 10;
            decimal avgAmount = 500m;

            var result = _commissionCalculationService.Calculate(localSales, 0, avgAmount);

            Assert.That(result.AvalphaCommission,
                Is.EqualTo(decimal.Round(
                    localSales * avgAmount * CommissionPercentage.AvalphaLocal, 2)));

            Assert.That(result.CompetitorsCommission,
                Is.EqualTo(decimal.Round(
                    localSales * avgAmount * CommissionPercentage.CompetitorLocal, 2)));
        }

        [Test]
        public void Calculate_OnlyForeignSales_ReturnsCorrectCommission()
        {
            int foreignSales = 4;
            decimal avgAmount = 2000m;

            var result = _commissionCalculationService.Calculate(0, foreignSales, avgAmount);

            Assert.That(result.AvalphaCommission,
                Is.EqualTo(decimal.Round(
                    foreignSales * avgAmount * CommissionPercentage.AvalphaForeign, 2)));

            Assert.That(result.CompetitorsCommission,
                Is.EqualTo(decimal.Round(
                    foreignSales * avgAmount * CommissionPercentage.CompetitorForeign, 2)));
        }

        [Test]
        public void Calculate_RoundsCommissionToTwoDecimalPlaces()
        {
            var result = _commissionCalculationService.Calculate(1, 1, 333.33m);

            Assert.That(result.AvalphaCommission,
                Is.EqualTo(decimal.Round(result.AvalphaCommission, 2)));

            Assert.That(result.CompetitorsCommission,
                Is.EqualTo(decimal.Round(result.CompetitorsCommission, 2)));
        }

        [Test]
        public void Calculate_NegativeLocalSales_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                _commissionCalculationService.Calculate(-1, 5, 1000m));
        }

        [Test]
        public void Calculate_NegativeForeignSales_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                _commissionCalculationService.Calculate(5, -1, 1000m));
        }

        [Test]
        public void Calculate_NegativeAverageAmount_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                _commissionCalculationService.Calculate(5, 5, -1000m));
        }
    }
}
