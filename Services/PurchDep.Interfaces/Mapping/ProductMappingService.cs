using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Mapping;

namespace PurchDep.Interfaces.Mapping
{
    public class ProductMappingService : IMappingService<Dal.Entities.Product, IProduct>
    {
        public IProduct Map(Dal.Entities.Product item)
        {
            var product = new Domain.Product()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
            };

            return product;
        }

        public Dal.Entities.Product Map(IProduct item)
        {
            var product = new Dal.Entities.Product()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
            };

            return product;
        }

        public async Task<IProduct> MapAsync(Dal.Entities.Product item, CancellationToken cancel = default)
        {
            var productTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await productTask;
        }

        public async Task<Dal.Entities.Product> MapAsync(IProduct item, CancellationToken cancel = default)
        {
            Task<Dal.Entities.Product> productTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await productTask;
        }

        public ICollection<IProduct> MapRange(ICollection<Dal.Entities.Product> items)
        {
            ICollection<IProduct> products = new List<IProduct>();
            foreach (var item in items)
            {
                products.Add(Map(item));
            }
            return products;
        }

        public ICollection<Dal.Entities.Product> MapRange(ICollection<IProduct> items)
        {
            ICollection<Dal.Entities.Product> products = new List<Dal.Entities.Product>();
            foreach (var item in items)
            {
                products.Add(Map(item));
            }
            return products;
        }

        public async Task<ICollection<IProduct>> MapRangeAsync(ICollection<Dal.Entities.Product> items, CancellationToken cancel = default)
        {
            var productsTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await productsTask;
        }

        public async Task<ICollection<Dal.Entities.Product>> MapRangeAsync(ICollection<IProduct> items, CancellationToken cancel = default)
        {
            var productsTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await productsTask;
        }
    }
}
