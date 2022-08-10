using PurchDep.Interfaces.Base.Mapping;
using StockDal = PurchDep.Dal.Entities.Stock;
using StockDom = PurchDep.Domain.Stock;
using StocksProductDal = PurchDep.Dal.Entities.StocksProduct;
using StocksProductDom = PurchDep.Domain.StocksProduct;

namespace PurchDep.Interfaces.Mapping
{
    public class StockMappingService : IMappingService<StockDal, StockDom>
    {
        private readonly IMappingService<StocksProductDal, StocksProductDom> _stockProductMapper;

        public StockMappingService(IMappingService<StocksProductDal, StocksProductDom> stockProductMapper)
        {
            _stockProductMapper = stockProductMapper;
        }
        public StockDom Map(StockDal item)
        {
            if (item is null) return null!;
            var result = new StockDom()
            {
                Id = item.Id,
                Name = item.Name,
            };

            foreach (var product in item.StocksProducts)
            {
                //var currentSuppliersProduct = product.Supplier.SuppliersProducts.FirstOrDefault(p => p.SupplierId == product.SupplierId);
                //result.StocksProducts.Add(new()
                //{
                //    Id = product.ProductId,
                //    Name = product.Product.Name,
                //    Quantity = product.Quantity,
                //    SuppliersPrice = currentSuppliersProduct!.Price,
                //    SuppliersId = product.SupplierId,
                //});
                result.StocksProducts.Add(_stockProductMapper.Map(product));
            }

            return result;
        }

        public StockDal Map(StockDom item)
        {
            if (item is null) return null!;
            var result = new StockDal()
            {
                Id = item.Id,
                Name = item.Name,
            };

            foreach (var product in item.StocksProducts)
            {
                result.StocksProducts.Add(new()
                {
                    ProductId = product.Id,
                    StockId = product.StockId,
                    Quantity = product.Quantity,
                    SupplierId = product.SupplierId,
                });
                result.StocksProducts.Add(_stockProductMapper.Map(null));
            }
            return result;
        }

        public async Task<StockDom> MapAsync(StockDal item, CancellationToken cancel = default)
        {
            if (item is null) return null!;
            var itemTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await itemTask;
        }

        public async Task<StockDal> MapAsync(StockDom item, CancellationToken cancel = default)
        {
            if (item is null) return null!;
            var itemTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await itemTask;
        }

        public ICollection<StockDom> MapRange(ICollection<StockDal> items)
        {
            if (items is null) return null!;
            var products = new List<StockDom>();
            foreach (var item in items)
            {
                products.Add(Map(item));
            }
            return products;
        }

        public ICollection<StockDal> MapRange(ICollection<StockDom> items)
        {
            if (items is null) return null!;
            var products = new List<StockDal>();
            foreach (var item in items)
            {
                products.Add(Map(item));
            }
            return products;
        }

        public async Task<ICollection<StockDom>> MapRangeAsync(ICollection<StockDal> items, CancellationToken cancel = default)
        {
            if (items is null) return null!;
            var itemsTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await itemsTask;
        }

        public async Task<ICollection<StockDal>> MapRangeAsync(ICollection<StockDom> items, CancellationToken cancel = default)
        {
            if (items is null) return null!;
            var itemsTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await itemsTask;
        }
    }
}
