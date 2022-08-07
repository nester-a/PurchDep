using PurchDep.Domain.Base;
using PurchDep.WebApi.Clients.Base;
using System.Net.Http.Json;

namespace PurchDep.WebApi.Clients.Products
{
    public class ProductsClient : BaseClient
    {
        protected ProductsClient(HttpClient client) : base(client, "api/products") { }

        public IProduct Add(IProduct item)
        {
            var response = Post(Address, item);
            var result = response.IsSuccessStatusCode;
            if (!result)
            {

            }
            var addedItem = response.Content.ReadFromJsonAsync<IProduct>().Result;

            if (addedItem is null)
            {
                var exceptionMessage = response.Content.ReadFromJsonAsync<string>().Result;
                throw new InvalidOperationException(exceptionMessage);
            }
            return addedItem;
        }

        public IProduct Delete(int id)
        {
            var response = Delete($"{Address}/{id}");
            var result = response.IsSuccessStatusCode;
            if (result)
            {
                var deletedItem = response.Content.ReadFromJsonAsync<IProduct>().Result;
                if(deletedItem is not null) return deletedItem;
            }
            return result;
        }

        public bool Edit(EmployeeDTO dto)
        {
            var response = Put(Address, dto);
            var result = response.EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<bool>()
                .Result;

            return result;
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            var employeesDTO = Get<IEnumerable<EmployeeDTO>>(Address);
            return employeesDTO ?? Enumerable.Empty<EmployeeDTO>();
        }

        public EmployeeDTO? GetById(int id)
        {
            var employeeDTO = Get<EmployeeDTO>($"{Address}/{id}");
            return employeeDTO;
        }
    }
}
