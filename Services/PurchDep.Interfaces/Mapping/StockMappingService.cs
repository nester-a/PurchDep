using PurchDep.Interfaces.Base.Mapping;
using StockDal = PurchDep.Dal.Entities.Stock;
using StockDom = PurchDep.Domain.Stock;
using StocksProductDal = PurchDep.Dal.Entities.StocksProduct;
using StocksProductDom = PurchDep.Domain.StocksProduct;

namespace PurchDep.Interfaces.Mapping
{
    public class StockMappingService : MappingService<StockDal, StockDom>
    {
        private readonly MappingService<StocksProductDal, StocksProductDom> _stockProductMapper;

        public StockMappingService(MappingService<StocksProductDal, StocksProductDom> stockProductMapper)
        {
            _stockProductMapper = stockProductMapper;
        }
        public override StockDom Map(StockDal item)
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

        public override StockDal Map(StockDom item)
        {
            if (item is null) return null!;
            var result = new StockDal()
            {
                Id = item.Id,
                Name = item.Name,
            };

            foreach (StocksProductDom product in item.StocksProducts)
            {
                //result.StocksProducts.Add(new()
                //{
                //    ProductId = product.Id,
                //    StockId = product.StockId,
                //    Quantity = product.Quantity,
                //    SupplierId = product.SupplierId,
                //});
                result.StocksProducts.Add(_stockProductMapper.Map(product));
            }
            return result;
        }

    }
}
