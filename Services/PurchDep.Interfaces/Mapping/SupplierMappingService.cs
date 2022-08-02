using PurchDep.Dal.Entities;
using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Mapping;

namespace PurchDep.Interfaces.Mapping
{
    public class SupplierMappingService : IMappingService<Dal.Entities.Supplier, ISupplier>
    {
        public ISupplier Map(Supplier item)
        {
            var supplier = new Domain.Supplier()
            {
                Id = item.Id,
                Name = item.Name,
            };
            foreach (var product in item.Products)
            {
                supplier.Products.Add(new Domain.Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                });
            }

            return supplier;
        }

        public Supplier Map(ISupplier item)
        {
            var supplier = new Supplier()
            {
                Id = item.Id,
                Name = item.Name,
            };
            foreach (var product in item.Products)
            {
                supplier.Products.Add(new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                });
            }

            return supplier;
        }

        public async Task<ISupplier> MapAsync(Supplier item, CancellationToken cancel = default)
        {
            var supplierTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await supplierTask;
        }

        public async Task<Supplier> MapAsync(ISupplier item, CancellationToken cancel = default)
        {
            var supplierTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await supplierTask;
        }

        public ICollection<ISupplier> MapRange(ICollection<Supplier> items)
        {
            var suppliers = new List<ISupplier>();
            foreach (var item in items)
            {
                suppliers.Add(Map(item));
            }
            return suppliers;
        }

        public ICollection<Supplier> MapRange(ICollection<ISupplier> items)
        {
            var suppliers = new List<Supplier>();
            foreach (var item in items)
            {
                suppliers.Add(Map(item));
            }
            return suppliers;
        }

        public async Task<ICollection<ISupplier>> MapRangeAsync(ICollection<Supplier> items, CancellationToken cancel = default)
        {
            var suppliersTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await suppliersTask;
        }

        public async Task<ICollection<Supplier>> MapRangeAsync(ICollection<ISupplier> items, CancellationToken cancel = default)
        {
            var suppliersTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await suppliersTask;
        }
    }
}
