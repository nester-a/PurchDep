using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchDep.Interfaces.Base.Mapping
{
    public interface IMapperService<TSource, TResult> : IMapper<TSource, TResult>, IMapperAsync<TSource, TResult> where TSource : class where TResult : class
    {

    }
}
