using System;
using Pos.DataAccess.Model;

namespace Pos.Web.Services
{
    public interface IScanner
    {
        event EventHandler<BarcodeScannedeventArgs> BarcodeScanned;

        void Scan(string barcode);
    }

    public class BarcodeScannedeventArgs : EventArgs
    {
        public string Barcode { get; set; }
        public string CatalogCode { get; set; }
        public string CatalogName { get; set; }
        public decimal Price { get; set; }
        public Tax[] Taxes { get; set; }
        public decimal Discount { get; set; }
    }
}
