using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain.Base.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchDep.UI.Mvc.Components
{
    public class NamedEntitiesListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<INamedEntity> items)
        {
            var res = items.Take(5);

            return View(res);
        }
    }
}
