using PurchDep.Interfaces.Base.Mapping;
using PurchDep.Interfaces.Base.Services;
using SupplierDal = PurchDep.Dal.Entities.Supplier;
using SupplierDom = PurchDep.Domain.Supplier;

namespace PurchDep.Interfaces.Services
{
    public class SupplierService : Service<SupplierDal, SupplierDom>
    {
        public SupplierService(Repository<SupplierDal> repository, MappingService<SupplierDal, SupplierDom> mapper) : base(repository, mapper) { }

        public override SupplierDom Add(SupplierDom item)
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

        public async override Task<SupplierDom> AddAsync(SupplierDom item, CancellationToken cancel = default)
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
