using Microsoft.EntityFrameworkCore;
using PurchDep.Dal;
using PurchDep.Dal.Entities;
using PurchDep.Interfaces.Base.Services;

namespace PurchDep.Interfaces.Repositories
{
    public class SupplierRepository : Repository<Supplier>
    {
        public SupplierRepository(PurchDepContext context) : base(context) { }

        public override Supplier Delete(int id)
        {
            var res = Set.FirstOrDefault(x => x.Id == id);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));
            Set.Remove(res);

            SaveChanges();
            return res;
        }

        public async override Task<Supplier> DeleteAsync(int id, CancellationToken cancel = default)
        {
            var res = await Set.FirstOrDefaultAsync(x => x.Id == id, cancel);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));
            await Task.Factory.StartNew(() => Set.Remove(res), cancel);

            await SaveChangesAsync(cancel);
            return res;
        }

        public override Supplier Get(int id)
        {
            var res = Set.FirstOrDefault(x => x.Id == id);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            return res;
        }

        public async override Task<Supplier> GetAsync(int id, CancellationToken cancel = default)
        {
            var res = await Set.FirstOrDefaultAsync(x => x.Id == id, cancel);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            return res;
        }

        public override Supplier Update(int id, Supplier updatedItem)
        {
            if (updatedItem is null) throw new ArgumentNullException("The Item being updated is null", nameof(updatedItem));
            var res = Set.FirstOrDefault(x => x.Id == id);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            res.Name = updatedItem.Name;
            res.Products = updatedItem.Products;

            SaveChanges();
            return res;
        }

        public async override Task<Supplier> UpdateAsync(int id, Supplier updatedItem, CancellationToken cancel = default)
        {
            if (updatedItem is null) throw new ArgumentNullException("The Item being updated is null", nameof(updatedItem));
            var res = await Set.FirstOrDefaultAsync(x => x.Id == id, cancel);
            if (res == null) throw new ArgumentException("There is no item with this Id in the database", nameof(id));

            res.Name = updatedItem.Name;
            res.Products = updatedItem.Products;

            await SaveChangesAsync(cancel);
            return res;
        }
    }
}
