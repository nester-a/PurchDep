using PurchDep.Dal.Entities;
using PurchDep.Interfaces.Base.Mapping;

using SupplierDom = PurchDep.Domain.Supplier;
using SuppliersProductDal = PurchDep.Dal.Entities.SuppliersProduct;
using SuppliersProductDom = PurchDep.Domain.SuppliersProduct;

namespace PurchDep.Interfaces.Mapping
{
    public class SupplierMappingService : MappingService<Supplier, SupplierDom>
    {
        private readonly MappingService<SuppliersProductDal, SuppliersProductDom> _suppliersProductMapper;

        public SupplierMappingService(MappingService<SuppliersProductDal, SuppliersProductDom> suppliersProductMapper)
        {
            _suppliersProductMapper = suppliersProductMapper;
        }
        public override SupplierDom Map(Supplier item)
        {
            if (item is null) return null!;
            var supplier = new SupplierDom()
            {
                Id = item.Id,
                Name = item.Name,
            };
            foreach (var product in item.SuppliersProducts)
            {
                if (product is null) continue;
                var productDom = _suppliersProductMapper.Map(product);
                supplier.SuppliersProducts.Add(productDom);
            }

            return supplier;
        }

        public override Supplier Map(SupplierDom item)
        {
            if (item is null) return null!;
            var supplier = new Supplier()
            {
                Id = item.Id,
                Name = item.Name,
            };
            foreach (SuppliersProductDom product in item.SuppliersProducts)
            {
                if (product is null) continue;
                var productDal = _suppliersProductMapper.Map(product);
                supplier.SuppliersProducts.Add(productDal);
            }

            return supplier;
        }

    }
}
