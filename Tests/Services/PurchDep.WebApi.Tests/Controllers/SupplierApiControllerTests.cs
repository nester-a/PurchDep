using PurchDep.Domain;
using PurchDep.Domain.Base;
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
    public class SupplierApiControllerTests
    {
        DbFixture _fixture;
        SupplierApiController _controller;
        public SupplierApiControllerTests(DbFixture fixture)
        {
            _fixture = fixture;
            var repo = new SupplierRepository(_fixture.Db);
            var mapper = new SupplierMappingService();
            var service = new SupplierService(repo, mapper);

            _controller = new SupplierApiController(service);
        }


        [Fact]
        public void GetAll_Returns_Ok_Test()
        {
            var actionRes = _controller.GetAll();
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as ICollection<Supplier>;
            Assert.True(returnedRes.StatusCode == 200);
            Assert.NotEqual(0, returnedObj!.Count);
        }

        [Fact]
        public void GetById_Returns_Ok_Test()
        {
            var actionRes = _controller.GetById(TestData.Supplier1.Id);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as ISupplier;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.Supplier1.Id, returnedObj!.Id);
            Assert.Equal(TestData.Supplier1.Name, returnedObj.Name);
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
            var actionRes = _controller.Add(TestData.Supplier3);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as ISupplier;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.Supplier3.Id, returnedObj!.Id);
            Assert.Equal(TestData.Supplier3.Name, returnedObj.Name);
        }

        [Fact]
        public void Add_Returns_BadRequest_Test()
        {
            var actionRes = _controller.Add(null);
            var returnedRes = actionRes as BadRequestObjectResult;

            Assert.True(returnedRes!.StatusCode == 400);
        }

        [Theory]
        [InlineData("SupplierToUpdate")]
        public void Edit_Returns_Ok_Test(string newName)
        {
            var supplierToUpdate = new Supplier { Name = newName };
            var actionRes = _controller.Edit(TestData.Supplier1.Id, supplierToUpdate);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as ISupplier;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.Supplier1.Id, returnedObj!.Id);
            Assert.Equal(TestData.Supplier1.Name, returnedObj.Name);
            Assert.Equal(TestData.Supplier1.Name, newName);
        }

        [Fact]
        public void Edit_Returns_BadRequest_Test()
        {
            var actionRes = _controller.Edit(TestData.Supplier1.Id, null);
            var returnedRes = actionRes as BadRequestObjectResult;

            Assert.True(returnedRes!.StatusCode == 400);
        }

        [Theory]
        [InlineData(404, "SupplierToUpdate")]
        public void Edit_Returns_NotFound_Test(int id, string newName)
        {
            var supplierToUpdate = new Supplier { Name = newName };
            var actionRes = _controller.Edit(id, supplierToUpdate);
            var returnedRes = actionRes as NotFoundObjectResult;

            Assert.True(returnedRes!.StatusCode == 404);
        }

        [Fact]
        public void Delete_Returns_Ok_Test()
        {
            _controller.Add(TestData.Supplier4);
            Assert.NotEqual(0, TestData.Supplier4.Id);

            var actionRes = _controller.Delete(TestData.Supplier4.Id);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as ISupplier;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.Supplier4.Id, returnedObj!.Id);
            Assert.Equal(TestData.Supplier4.Name, returnedObj.Name);
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
