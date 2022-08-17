using PurchDep.Domain;
using PurchDep.Interfaces.Services;
using PurchDep.WebApi.Controllers;
using PurchDep.WebApi.Tests.Data;
using PurchDep.WebApi.Tests.Fixtures;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using System;

namespace PurchDep.WebApi.Tests.Controllers
{
    [Collection("ApiController collection")]
    public class SupplierApiControllerTests
    {
        Mock<SupplierService> _serviceMock;
        public SupplierApiControllerTests(ApiControllerFixture fixture)
        {
            _serviceMock = fixture.SupplierServiceMock;
        }

        [Fact]
        public void GetAll_Returns_Ok_Test()
        {
            _serviceMock.Setup(service => service.GetAll()).Returns(TestData.SuppliersDom);

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.GetAll();
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as ICollection<Supplier>;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.NotEqual(0, returnedObj!.Count);
            _serviceMock.Verify(service => service.GetAll());
        }

        [Fact]
        public void GetAll_Returns_NoContent_Test()
        {
            _serviceMock.Setup(service => service.GetAll()).Returns(new List<Supplier>());

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.GetAll();
            var returnedRes = actionRes as NoContentResult;

            Assert.True(returnedRes!.StatusCode == 204);
            _serviceMock.Verify(service => service.GetAll());
        }

        [Fact]
        public void GetById_Returns_Ok_Test()
        {
            _serviceMock.Setup(service => service.Get(It.IsAny<int>())).Returns(TestData.SupplierDom_1);

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.GetById(1);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Supplier;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.SupplierDom_1.Id, returnedObj!.Id);
            Assert.Equal(TestData.SupplierDom_1.Name, returnedObj.Name);
            _serviceMock.Verify(service => service.Get(It.IsAny<int>()));
        }

        [Theory]
        [InlineData(404)]
        public void GetById_Returns_NotFound_Test(int id)
        {
            _serviceMock.Setup(service => service.Get(id)).Throws(new ArgumentException());

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.GetById(id);
            var returnedRes = actionRes as NotFoundObjectResult;

            Assert.True(returnedRes!.StatusCode == 404);
            _serviceMock.Verify(service => service.Get(id));
        }

        [Fact]
        public void Add_Returns_Ok_Test()
        {
            _serviceMock.Setup(service => service.Add(TestData.SupplierDom_ForAdding)).Returns(TestData.SupplierDom_ForAdding);

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.Add(TestData.SupplierDom_ForAdding);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Supplier;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.SupplierDom_ForAdding.Id, returnedObj!.Id);
            Assert.Equal(TestData.SupplierDom_ForAdding.Name, returnedObj.Name);
            _serviceMock.Verify(service => service.Add(TestData.SupplierDom_ForAdding));
        }

        [Fact]
        public void AddNull_Returns_BadRequest_Test()
        {
            _serviceMock.Setup(service => service.Add(null!)).Throws(new ArgumentNullException());

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.Add(null!);
            var returnedRes = actionRes as BadRequestObjectResult;

            Assert.True(returnedRes!.StatusCode == 400);
            _serviceMock.Verify(service => service.Add(null!));
        }

        [Fact]
        public void AddAlreadyExsistsItem_Returns_BadRequest_Test()
        {
            _serviceMock.Setup(service => service.Add(TestData.SupplierDom_1)).Throws(new ArgumentException());

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.Add(TestData.SupplierDom_1);
            var returnedRes = actionRes as BadRequestObjectResult;

            Assert.True(returnedRes!.StatusCode == 400);
            _serviceMock.Verify(service => service.Add(TestData.SupplierDom_1));
        }

        [Fact]
        public void Edit_Returns_Ok_Test()
        {
            _serviceMock.Setup(service => service.Update(It.IsAny<int>(), TestData.SupplierDom_ForUpdating)).Returns(TestData.SupplierDom_ForUpdating);

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.Edit(1, TestData.SupplierDom_ForUpdating);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Supplier;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.SupplierDom_ForUpdating.Id, returnedObj!.Id);
            Assert.Equal(TestData.SupplierDom_ForUpdating.Name, returnedObj.Name);
            _serviceMock.Verify(service => service.Update(It.IsAny<int>(), TestData.SupplierDom_ForUpdating));
        }

        [Fact]
        public void Edit_Returns_BadRequest_Test()
        {
            _serviceMock.Setup(service => service.Update(It.IsAny<int>(), null!)).Throws(new ArgumentNullException());

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.Edit(1, null!);
            var returnedRes = actionRes as BadRequestObjectResult;

            Assert.True(returnedRes!.StatusCode == 400);
            _serviceMock.Verify(service => service.Update(It.IsAny<int>(), null!));
        }

        [Theory]
        [InlineData(404)]
        public void Edit_Returns_NotFound_Test(int id)
        {
            _serviceMock.Setup(service => service.Update(id, It.IsAny<Supplier>())).Throws(new ArgumentException());

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.Edit(id, TestData.SupplierDom_ForUpdating);
            var returnedRes = actionRes as NotFoundObjectResult;

            Assert.True(returnedRes!.StatusCode == 404);
            _serviceMock.Verify(service => service.Update(id, It.IsAny<Supplier>()));
        }

        [Fact]
        public void Delete_Returns_Ok_Test()
        {
            _serviceMock.Setup(service => service.Delete(It.IsAny<int>())).Returns(TestData.SupplierDom_ForDeleting);

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.Delete(1);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Supplier;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.SupplierDom_ForDeleting.Id, returnedObj!.Id);
            Assert.Equal(TestData.SupplierDom_ForDeleting.Name, returnedObj.Name);
            _serviceMock.Verify(service => service.Delete(It.IsAny<int>()));
        }

        [Theory]
        [InlineData(404)]
        public void Delete_Returns_NotFound_Test(int id)
        {
            _serviceMock.Setup(service => service.Delete(id)).Throws(new ArgumentException());

            var controller = new SupplierApiController(_serviceMock.Object);
            var actionRes = controller.Delete(id);
            var returnedRes = actionRes as NotFoundObjectResult;

            Assert.True(returnedRes!.StatusCode == 404);
            _serviceMock.Verify(service => service.Delete(id));
        }
    }
}
