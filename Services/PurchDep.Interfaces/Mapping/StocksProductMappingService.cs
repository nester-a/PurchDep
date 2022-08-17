using PurchDep.Interfaces.Base.Mapping;
using StocksProductDal = PurchDep.Dal.Entities.StocksProduct;
using StocksProductDom = PurchDep.Domain.StocksProduct;

namespace PurchDep.Interfaces.Mapping
{
    public class StocksProductMappingService : MappingService<StocksProductDal, StocksProductDom>
    {
        public override StocksProductDom Map(StocksProductDal item)
        {
            if (item is null) return null!;
            var currentSuppliersProduct = item.Supplier.SuppliersProducts.FirstOrDefault(p => p.SupplierId == item.SupplierId);
            var result = new StocksProductDom()
            {
                Id = item.ProductId,
                Name = item.Product.Name,
                Quantity = item.Quantity,
                SupplierId = item.SupplierId,
                SuppliersPrice = currentSuppliersProduct!.Price,
                StockId = item.StockId,
            };

            return result;
        }

        public override StocksProductDal Map(StocksProductDom item)
        {
            if (item is null) return null!;
            var result = new StocksProductDal()
            {
                ProductId = item.Id,
                StockId = item.StockId,
                Quantity = item.Quantity,
                SupplierId = item.SupplierId,
            };

            return result;
        }
    }
}
