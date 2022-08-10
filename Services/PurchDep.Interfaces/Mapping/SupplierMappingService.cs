﻿using PurchDep.Dal.Entities;
using PurchDep.Domain.Base;
using PurchDep.Interfaces.Base.Mapping;

using SupplierDom = PurchDep.Domain.Supplier;

namespace PurchDep.Interfaces.Mapping
{
    public class SupplierMappingService : IMappingService<Supplier, SupplierDom>
    {
        public SupplierDom Map(Supplier item)
        {
            if (item is null) return null!;
            var supplier = new SupplierDom()
            {
                Id = item.Id,
                Name = item.Name,
            };
            foreach (var product in item.Products)
            {
                if (product is null) continue;
                supplier.Products.Add(new Domain.Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                });
            }

            return supplier;
        }

        public Supplier Map(SupplierDom item)
        {
            if (item is null) return null!;
            var supplier = new Supplier()
            {
                Id = item.Id,
                Name = item.Name,
            };
            foreach (var product in item.Products)
            {
                if (product is null) continue;
                supplier.Products.Add(new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                });
            }

            return supplier;
        }

        public async Task<SupplierDom> MapAsync(Supplier item, CancellationToken cancel = default)
        {
            if (item is null) return null!;
            var supplierTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await supplierTask;
        }

        public async Task<Supplier> MapAsync(SupplierDom item, CancellationToken cancel = default)
        {
            if (item is null) return null!;
            var supplierTask = Task.Factory.StartNew(() => Map(item), cancel);
            return await supplierTask;
        }

        public ICollection<SupplierDom> MapRange(ICollection<Supplier> items)
        {
            if (items is null) return null!;
            var suppliers = new List<SupplierDom>();
            foreach (var item in items)
            {
                suppliers.Add(Map(item));
            }
            return suppliers;
        }

        public ICollection<Supplier> MapRange(ICollection<SupplierDom> items)
        {
            if (items is null) return null!;
            var suppliers = new List<Supplier>();
            foreach (var item in items)
            {
                suppliers.Add(Map(item));
            }
            return suppliers;
        }

        public async Task<ICollection<SupplierDom>> MapRangeAsync(ICollection<Supplier> items, CancellationToken cancel = default)
        {
            if (items is null) return null!;
            var suppliersTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await suppliersTask;
        }

        public async Task<ICollection<Supplier>> MapRangeAsync(ICollection<SupplierDom> items, CancellationToken cancel = default)
        {
            if (items is null) return null!;
            var suppliersTask = Task.Factory.StartNew(() => MapRange(items), cancel);
            return await suppliersTask;
        }
    }
}
