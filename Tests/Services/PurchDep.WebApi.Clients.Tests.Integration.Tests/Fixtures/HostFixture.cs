using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;

namespace PurchDep.WebApi.Clients.Tests.Integration.Tests.Fixtures
{
    public class HostFixture : IDisposable
    {
        bool _disposed;
        public WebApplicationFactory<Program> WebAPIHostBuilder { get; private set; }
        public HttpClient HttpClient { get; private set; }
        public HostFixture()
        {
            WebAPIHostBuilder = new WebApplicationFactory<Program>();
            HttpClient = WebAPIHostBuilder.CreateClient();
        }

        public void Dispose()
        {
            if (_disposed) return;
            Dispose(true);
            _disposed = true;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                //освободить все управляемые ресурсы (удалить всё, что было создано в этом объекте)
                HttpClient?.Dispose();
                WebAPIHostBuilder?.Dispose();
            }

            //освобождение всех неуправляемых
        }
    }
}
