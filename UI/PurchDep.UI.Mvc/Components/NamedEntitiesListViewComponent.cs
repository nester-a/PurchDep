using Microsoft.AspNetCore.Mvc;
using PurchDep.Domain.Base.Core;
using PurchDep.UI.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchDep.UI.Mvc.Components
{
    public class NamedEntitiesListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IndexNamedEntityModel namedEnityModel)
        {
            var res = namedEnityModel;

            return View(res);
        }
    }
}
