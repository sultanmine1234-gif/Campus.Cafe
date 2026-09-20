using Campus_Cafe.Services;

namespace CampusCafe.Tests
{
    public class OrderTotalCalculatorTests
    {
        [Fact]
        public void CalculateTotal_NoDiscount_ReturnsSubtotal()
        {
            // Arrange
            OrderTotalCalculator calculator = new OrderTotalCalculator();

            decimal subtotal = 24.00m;
            decimal discountPercent = 0m;
            decimal expectedResult = 24.00m;

            // Act
            decimal actualResult = calculator.CalculateTotal(
                subtotal,
                discountPercent);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]

        public void CalculateTotal_FivePercentDiscount_ReturnsDiscountedTotal()
        {
            // Arrange 
            OrderTotalCalculator calculator = new OrderTotalCalculator();

            decimal subtotal = 24.00m;
            decimal discountPercent = 5m;
            decimal expectedResult = 22.8m;

            // Act

            decimal actualtResult = calculator.CalculateTotal(
                subtotal,
                discountPercent);

            // Assert 
            Assert.Equal(expectedResult, actualtResult);
        }

        [Fact]

        public void CalculateTotal_MidpointDiscount_RoundsAwayFromZero()
        {
            // Arrange 

            OrderTotalCalculator calculator = new OrderTotalCalculator();

            decimal subtotal = 12.50m;
            decimal discountPercent = 5m;
            decimal expectedResult = 11.87m;

            // Act 

            decimal actualtResult = calculator.CalculateTotal(
                subtotal,
                discountPercent);

            // Assert 
            Assert.Equal(expectedResult, actualtResult);
        }

        [Fact]

        public void CalculateTotal_ZeroSubtotal_ReturnsZero()
        {
            // Arrange 

            OrderTotalCalculator calculator = new OrderTotalCalculator();

            decimal subtotal = 0.00m;
            decimal discountPercent = 5m;
            decimal expectedResult = 0.00m;

            // Act 

            decimal actualtResult = calculator.CalculateTotal(
                subtotal,
                discountPercent);

            // Assert   
            Assert.Equal(expectedResult, actualtResult);
        }

        [Theory]
        [InlineData(10.00, 0, 10.00)]
        [InlineData(10.00, 5, 9.50)]
        [InlineData(33.00, 10, 29.70)]
        public void CalculateTotal_VariousValues_ReturnsExpectedTotal(
            decimal subtotal,
            decimal discountPercent,
            decimal expectedResult)
        {
            // Arrange
            OrderTotalCalculator calculator = new OrderTotalCalculator();

            // Act
            decimal actualResult = calculator.CalculateTotal(
                subtotal,
                discountPercent);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CalculateTotal_DiscountAboveHundred_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            OrderTotalCalculator calculator = new OrderTotalCalculator();

            decimal subtotal = 10m;
            decimal discountPercent = 101m;

            // Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(
                () => calculator.CalculateTotal(subtotal, discountPercent));
        }

    }
}