using PurchDep.Interfaces.Base.Mapping;
using SuppliersProductDal = PurchDep.Dal.Entities.SuppliersProduct;
using SuppliersProductDom = PurchDep.Domain.SuppliersProduct;

namespace PurchDep.Interfaces.Mapping
{
    public class SuppliersProductMappingService : MappingService<SuppliersProductDal, SuppliersProductDom>
    {
        public override SuppliersProductDom Map(SuppliersProductDal item)
        {
            if (item is null) return null!;
            var result = new SuppliersProductDom()
            {
                Id = item.ProductId,
                Name = item.Product.Name,
                SuppliersPrice = item.Price,
                SupplierId = item.SupplierId,
            };

            return result;
        }

        public override SuppliersProductDal Map(SuppliersProductDom item)
        {
            if (item is null) return null!;
            var result = new SuppliersProductDal()
            {
                ProductId = item.Id,
                SupplierId = item.SupplierId,
                Price = item.SuppliersPrice,
            };

            return result;
        }
    }
}
