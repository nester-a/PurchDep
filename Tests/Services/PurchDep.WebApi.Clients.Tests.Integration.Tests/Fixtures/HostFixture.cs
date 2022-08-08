using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace PurchDep.WebApi.Clients.Tests.Integration.Tests.Fixtures
{
    public class HostFixture
    {
        public WebApplicationFactory<Program> WebAPIHostBuilder { get; private set; }
        public HttpClient HttpClient { get; private set; }
        public HostFixture()
        {
            WebAPIHostBuilder = new WebApplicationFactory<Program>();
            HttpClient = WebAPIHostBuilder.CreateClient();
        }
    }
}
