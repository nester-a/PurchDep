using PurchDep.Domain;
using PurchDep.Interfaces.Mapping;
using PurchDep.Interfaces.Repositories;
using PurchDep.Interfaces.Services;
using PurchDep.WebApi.Controllers;
using PurchDep.WebApi.Tests.Data;
using PurchDep.WebApi.Tests.Fixtures;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace PurchDep.WebApi.Tests.Controllers
{
    [Collection("WebApi Database collection")]
    public class ProductApiControllerTests
    {
        DbFixture _fixture;
        ProductApiController _controller;
        public ProductApiControllerTests(DbFixture fixture)
        {
            _fixture = fixture;
            var repo = new ProductRepository(_fixture.Db);
            var mapper = new ProductMappingService();
            var service = new ProductService(repo, mapper);

            _controller = new ProductApiController(service);
        }

        [Fact]
        public void GetAll_Returns_Ok_Test()
        {
            var actionRes = _controller.GetAll();
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as ICollection<Product>;
            Assert.True(returnedRes.StatusCode == 200);
            Assert.NotEqual(0, returnedObj!.Count);
        }

        [Fact]
        public void GetById_Returns_Ok_Test()
        {
            var actionRes = _controller.GetById(TestData.Product1.Id);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Product;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.Product1.Id, returnedObj!.Id);
            Assert.Equal(TestData.Product1.Name, returnedObj.Name);
        }

        [Theory]
        [InlineData(404)]
        public void GetById_Returns_NotFound_Test(int id)
        {
            var actionRes = _controller.GetById(id);
            var returnedRes = actionRes as NotFoundObjectResult;

            Assert.True(returnedRes!.StatusCode == 404);
        }

        [Fact]
        public void Add_Returns_Ok_Test()
        {
            var actionRes = _controller.Add(TestData.Product3);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Product;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.Product3.Id, returnedObj!.Id);
            Assert.Equal(TestData.Product3.Name, returnedObj.Name);
        }

        [Fact]
        public void Add_Returns_BadRequest_Test()
        {
            var actionRes = _controller.Add(null);
            var returnedRes = actionRes as BadRequestObjectResult;

            Assert.True(returnedRes!.StatusCode == 400);
        }

        [Theory]
        [InlineData("ProductToUpdate")]
        public void Edit_Returns_Ok_Test(string newName)
        {
            var productToUpdate = new Product { Name = newName };
            var actionRes = _controller.Edit(TestData.Product1.Id, productToUpdate);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Product;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.Product1.Id, returnedObj!.Id);
            Assert.Equal(TestData.Product1.Name, returnedObj.Name);
            Assert.Equal(TestData.Product1.Name, newName);
        }

        [Fact]
        public void Edit_Returns_BadRequest_Test()
        {
            var actionRes = _controller.Edit(TestData.Product1.Id, null);
            var returnedRes = actionRes as BadRequestObjectResult;

            Assert.True(returnedRes!.StatusCode == 400);
        }

        [Theory]
        [InlineData(404, "ProductToUpdate")]
        public void Edit_Returns_NotFound_Test(int id, string newName)
        {
            var productToUpdate = new Product { Name = newName };
            var actionRes = _controller.Edit(id, productToUpdate);
            var returnedRes = actionRes as NotFoundObjectResult;

            Assert.True(returnedRes!.StatusCode == 404);
        }

        [Fact]
        public void Delete_Returns_Ok_Test()
        {
            _controller.Add(TestData.Product4);
            Assert.NotEqual(0, TestData.Product4.Id);

            var actionRes = _controller.Delete(TestData.Product4.Id);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Product;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.Product4.Id, returnedObj!.Id);
            Assert.Equal(TestData.Product4.Name, returnedObj.Name);
        }

        [Theory]
        [InlineData(404)]
        public void Delete_Returns_NotFound_Test(int id)
        {
            var actionRes = _controller.Delete(id);
            var returnedRes = actionRes as NotFoundObjectResult;

            Assert.True(returnedRes.StatusCode == 404);
        }
    }
}
