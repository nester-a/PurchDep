using Microsoft.EntityFrameworkCore;
using PurchDep.Dal;
using PurchDep.Dal.Entities;
using PurchDep.Interfaces.Base.Services;

namespace PurchDep.Interfaces.Repositories
{
    public class StockRepository : Repository<Stock>
    {
        public StockRepository(PurchDepContext context) : base(context) { }

        public override Stock Delete(int id)
        {
            var res = Set.FirstOrDefault(x => x.Id == id);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));
            Set.Remove(res);

            SaveChanges();
            return res;
        }

        public async override Task<Stock> DeleteAsync(int id, CancellationToken cancel = default)
        {
            var res = await Set.FirstOrDefaultAsync(x => x.Id == id, cancel);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));
            await Task.Factory.StartNew(() => Set.Remove(res), cancel);

            await SaveChangesAsync(cancel);
            return res;
        }

        public override Stock Get(int id)
        {
            var res = Set.FirstOrDefault(x => x.Id == id);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            return res;
        }

        public async override Task<Stock> GetAsync(int id, CancellationToken cancel = default)
        {
            var res = await Set.FirstOrDefaultAsync(x => x.Id == id, cancel);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            return res;
        }

        public override Stock Update(int id, Stock updatedItem)
        {
            if (updatedItem is null) throw new ArgumentNullException("The Item being updated is null", nameof(updatedItem));
            var res = Set.FirstOrDefault(x => x.Id == id);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            res.Name = updatedItem.Name;
            res.StocksProducts = updatedItem.StocksProducts;

            SaveChanges();
            return res;
        }

        public async override Task<Stock> UpdateAsync(int id, Stock updatedItem, CancellationToken cancel = default)
        {
            if (updatedItem is null) throw new ArgumentNullException("The Item being updated is null", nameof(updatedItem));
            var res = await Set.FirstOrDefaultAsync(x => x.Id == id, cancel);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            res.Name = updatedItem.Name;
            res.StocksProducts = updatedItem.StocksProducts;

            await SaveChangesAsync(cancel);
            return res;
        }
    }
}
