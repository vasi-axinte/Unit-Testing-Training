using Pos.DataAccess.Model;

namespace Pos.Web.Controllers
{
    public class PriceCalculator : IPriceCalculator
    {
        public decimal GetPrice(Product product)
        {
            // var price = product.Taxes.Aggregate(product.Price, (current, productTax) => current + current * productTax.Value);
            var price = product.Price;

            foreach (var productTax in product.Taxes)
            {
                price += price * productTax.Value;
            }

            return price;
        }
    }
}
