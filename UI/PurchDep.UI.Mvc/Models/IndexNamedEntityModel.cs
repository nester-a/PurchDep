using PurchDep.Domain.Base.Core;

namespace PurchDep.UI.Mvc.Models
{
    public class IndexNamedEntityModel
    {
        public string NamedEntityName { get; set; } = string.Empty;

        public IEnumerable<INamedEntity>? Items { get; set; }
    }
}
