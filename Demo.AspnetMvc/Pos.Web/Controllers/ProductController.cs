using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pos.DataAccess.Model;
using Pos.DataAccess.Repositories;
using Pos.Web.Models;

namespace Pos.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository rep;
        private readonly IProductRepository repository;
        private readonly IPriceCalculator priceCalculator;

        public ProductController(IRepository rep)
        {
            this.rep = rep;
        }

        public ProductController(IProductRepository repository, IPriceCalculator priceCalculator)
        {
            this.repository = repository;
            this.priceCalculator = priceCalculator;
        }

        public IActionResult Details(string barcode)
        {
            var code = barcode.ToLower().Trim();
            Product p = repository.GetProductByBarcode(code);

            ProductViewModel vm;
            if (p != null)
            {
                decimal price = priceCalculator.GetPrice(p);
                vm = new ProductViewModel
                {
                    Code = p.CatalogCode,
                    Name = p.CatalogName,
                    Price = $"{price} $",
                };
            }
            else
                vm = new ProductViewModel {Name = "Not Available"};

            return View(vm);
        }

        public IActionResult List()
        {
            var list = rep.GetEntity<Product>()
                .OrderBy(p=>p.CatalogName)
                .ToList();
            return View(list);
        }
    }
}