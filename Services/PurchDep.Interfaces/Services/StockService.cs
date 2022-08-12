using PurchDep.Interfaces.Base.Mapping;
using PurchDep.Interfaces.Base.Services;
using StockDal = PurchDep.Dal.Entities.Stock;
using StockDom = PurchDep.Domain.Stock;

namespace PurchDep.Interfaces.Services
{
    public class StockService : Service<StockDal, StockDom>
    {
        public StockService(Repository<StockDal, int> repository, IMappingService<StockDal, StockDom> mapper) : base(repository, mapper) { }

        public override StockDom Add(StockDom item)
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

        public async override Task<StockDom> AddAsync(StockDom item, CancellationToken cancel = default)
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
