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

        public async Task<StocksProductDom> MapAsync(StocksProductDal item, CancellationToken cancel = default)
        {
            if (item is null) return null!;
            var itemTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await itemTask;
        }

        public async Task<StocksProductDal> MapAsync(StocksProductDom item, CancellationToken cancel = default)
        {
            if (item is null) return null!;
            var itemTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await itemTask;
        }

        public ICollection<StocksProductDom> MapRange(ICollection<StocksProductDal> items)
        {
            if (items is null) return null!;
            var products = new List<StocksProductDom>();
            foreach (var item in items)
            {
                products.Add(Map(item));
            }
            return products;
        }

        public ICollection<StocksProductDal> MapRange(ICollection<StocksProductDom> items)
        {
            if (items is null) return null!;
            var products = new List<StocksProductDal>();
            foreach (var item in items)
            {
                products.Add(Map(item));
            }
            return products;
        }

        public async Task<ICollection<StocksProductDom>> MapRangeAsync(ICollection<StocksProductDal> items, CancellationToken cancel = default)
        {
            if (items is null) return null!;
            var itemsTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await itemsTask;
        }

        public async Task<ICollection<StocksProductDal>> MapRangeAsync(ICollection<StocksProductDom> items, CancellationToken cancel = default)
        {
            if (items is null) return null!;
            var itemsTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await itemsTask;
        }
    }
}
