using Microsoft.EntityFrameworkCore;
using PurchDep.Dal;
using PurchDep.Dal.Entities;
using PurchDep.Interfaces.Base.Services;

namespace PurchDep.Interfaces.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(PurchDepContext context) : base(context) { }

        public override Product Delete(int id)
        {
            var res = Set.FirstOrDefault(x => x.Id == id);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));
            Set.Remove(res);

            SaveChanges();
            return res;
        }

        public async override Task<Product> DeleteAsync(int id, CancellationToken cancel = default)
        {
            var res = await Set.FirstOrDefaultAsync(x => x.Id == id, cancel);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));
            await Task.Factory.StartNew(() => Set.Remove(res), cancel);

            await SaveChangesAsync(cancel);
            return res;
        }

        public override Product Get(int id)
        {
            var res = Set.FirstOrDefault(x => x.Id == id);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            return res;
        }

        public async override Task<Product> GetAsync(int id, CancellationToken cancel = default)
        {
            var res = await Set.FirstOrDefaultAsync(x => x.Id == id, cancel);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            return res;
        }

        public override Product Update(int id, Product updatedItem)
        {
            if (updatedItem is null) throw new ArgumentNullException("The Item being updated is null", nameof(updatedItem));
            var res = Set.FirstOrDefault(x => x.Id == id);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            res.Name = updatedItem.Name;
            res.Price = updatedItem.Price;

            SaveChanges();
            return res;
        }

        public async override Task<Product> UpdateAsync(int id, Product updatedItem, CancellationToken cancel = default)
        {
            if (updatedItem is null) throw new ArgumentNullException("The Item being updated is null", nameof(updatedItem));
            var res = await Set.FirstOrDefaultAsync(x => x.Id == id, cancel);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            res.Name = updatedItem.Name;
            res.Price = updatedItem.Price;

            await SaveChangesAsync(cancel);
            return res;
        }
    }
}
