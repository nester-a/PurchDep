using PurchDep.Interfaces.Base.Mapping;
using ProductDom = PurchDep.Domain.Product;
using ProductDal = PurchDep.Dal.Entities.Product;

namespace PurchDep.Interfaces.Mapping
{
    public class ProductMappingService : MappingService<ProductDal, ProductDom>
    {
        public override ProductDom Map(ProductDal item)
        {
            if (item is null) return null!;
            var product = new ProductDom()
            {
                Id = item.Id,
                Name = item.Name,
            };

            return product;
        }

        public override ProductDal Map(ProductDom item)
        {
            if (item is null) return null!;
            var product = new ProductDal()
            {
                Id = item.Id,
                Name = item.Name,
            };

            return product;
        }
    }
}
