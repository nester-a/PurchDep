using PurchDep.Interfaces.Base.Mapping;
using ProductDom = PurchDep.Domain.Product;

namespace PurchDep.Interfaces.Mapping
{
    public class ProductMappingService : IMappingService<Dal.Entities.Product, ProductDom>
    {
        public ProductDom Map(Dal.Entities.Product item)
        {
            if (item is null) return null!;
            var product = new Domain.Product()
            {
                Id = item.Id,
                Name = item.Name,
            };

            return product;
        }

        public Dal.Entities.Product Map(ProductDom item)
        {
            if (item is null) return null!;
            var product = new Dal.Entities.Product()
            {
                Id = item.Id,
                Name = item.Name,
            };

            return product;
        }

        public async Task<ProductDom> MapAsync(Dal.Entities.Product item, CancellationToken cancel = default)
        {
            if (item is null) return null!;
            var productTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await productTask;
        }

        public async Task<Dal.Entities.Product> MapAsync(ProductDom item, CancellationToken cancel = default)
        {
            if (item is null) return null!;
            Task<Dal.Entities.Product> productTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await productTask;
        }

        public ICollection<ProductDom> MapRange(ICollection<Dal.Entities.Product> items)
        {
            if (items is null) return null!;
            ICollection<ProductDom> products = new List<ProductDom>();
            foreach (var item in items)
            {
                products.Add(Map(item));
            }
            return products;
        }

        public ICollection<Dal.Entities.Product> MapRange(ICollection<ProductDom> items)
        {
            if (items is null) return null!;
            ICollection<Dal.Entities.Product> products = new List<Dal.Entities.Product>();
            foreach (var item in items)
            {
                products.Add(Map(item));
            }
            return products;
        }

        public async Task<ICollection<ProductDom>> MapRangeAsync(ICollection<Dal.Entities.Product> items, CancellationToken cancel = default)
        {
            if (items is null) return null!;
            var productsTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await productsTask;
        }

        public async Task<ICollection<Dal.Entities.Product>> MapRangeAsync(ICollection<ProductDom> items, CancellationToken cancel = default)
        {
            if (items is null) return null!;
            var productsTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await productsTask;
        }
    }
}
