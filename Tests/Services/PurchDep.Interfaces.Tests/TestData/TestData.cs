using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProductDal = PurchDep.Dal.Entities.Product;
using SuppliersProductDal = PurchDep.Dal.Entities.SuppliersProduct;
using StocksProductDal = PurchDep.Dal.Entities.StocksProduct;
using SupplierDal = PurchDep.Dal.Entities.Supplier;
using StockDal = PurchDep.Dal.Entities.Stock;
using ProductDom = PurchDep.Domain.Product;
using SuppliersProductDom = PurchDep.Domain.SuppliersProduct;
using StocksProductDom = PurchDep.Domain.StocksProduct;
using SupplierDom = PurchDep.Domain.Supplier;
using StockDom = PurchDep.Domain.Stock;

namespace PurchDep.Interfaces.Tests.TestData
{
    public static class TestData
    {
        public static ProductDal ProductDal_1 { get; } = new()
        {
            Name = "ProductDal_1",
        };
        public static ProductDom ProductDom_1 { get; } = new()
        {
            Name = "ProductDal_1",
        };
    }
}
