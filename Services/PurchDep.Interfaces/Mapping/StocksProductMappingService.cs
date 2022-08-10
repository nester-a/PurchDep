using PurchDep.Interfaces.Base.Mapping;
using StocksProductDal = PurchDep.Dal.Entities.StocksProduct;
using StocksProductDom = PurchDep.Domain.StocksProduct;

namespace PurchDep.Interfaces.Mapping
{
    public class StocksProductMappingService : IMappingService<StocksProductDal, StocksProductDom>
    {
        public StocksProductDom Map(StocksProductDal item)
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

        public StocksProductDal Map(StocksProductDom item)
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

        public Task<StocksProductDom> MapAsync(StocksProductDal item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<StocksProductDal> MapAsync(StocksProductDom item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public ICollection<StocksProductDom> MapRange(ICollection<StocksProductDal> items)
        {
            throw new NotImplementedException();
        }

        public ICollection<StocksProductDal> MapRange(ICollection<StocksProductDom> items)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<StocksProductDom>> MapRangeAsync(ICollection<StocksProductDal> items, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<StocksProductDal>> MapRangeAsync(ICollection<StocksProductDom> items, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
