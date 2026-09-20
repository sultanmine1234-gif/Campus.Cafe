using System.Diagnostics;

namespace Campus_Cafe.Services
{
    public class OrderTotalCalculator
    {
        public decimal CalculateTotal(
            decimal subtotal,
            decimal discountPercent)
        {
            if (subtotal < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(subtotal));
            }

            if (discountPercent < 0 || discountPercent > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(discountPercent));
            }

            decimal discount = Math.Round(
                subtotal * discountPercent / 100m,
                2,
                MidpointRounding.AwayFromZero);

            decimal total = Math.Round(
                subtotal - discount,
                2,
                MidpointRounding.AwayFromZero);

            return total;
        }
    }
}
