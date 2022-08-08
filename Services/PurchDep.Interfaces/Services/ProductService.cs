using PurchDep.Interfaces.Base.Mapping;
using PurchDep.Interfaces.Base.Services;
using ProductDal = PurchDep.Dal.Entities.Product;
using ProductDom = PurchDep.Domain.Product;

namespace PurchDep.Interfaces.Services
{
    public class ProductService : Service<ProductDal, ProductDom>
    {
        public ProductService(Repository<ProductDal> repository, IMappingService<ProductDal, ProductDom> mapper) : base(repository, mapper) { }

        public override ProductDom Add(ProductDom item)
        {
            var itemToAdd = Mapper.Map(item);
            try
            {
                Repository.Add(itemToAdd);
            }
            catch
            {
                throw;
            }
            item.Id = itemToAdd.Id;
            return item;
        }

        public async override Task<ProductDom> AddAsync(ProductDom item, CancellationToken cancel = default)
        {
            var itemToAdd = await Mapper.MapAsync(item, cancel);
            try
            {
                var result = await Repository.AddAsync(itemToAdd, cancel);
            }
            catch
            {
                throw;
            }
            item.Id = itemToAdd.Id;
            return item;
        }

    }
}
