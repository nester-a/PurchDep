using Microsoft.AspNetCore.Mvc;
using Moq;
using PurchDep.Domain;
using PurchDep.Interfaces.Services;
using PurchDep.WebApi.Controllers;
using PurchDep.WebApi.Tests.Data;
using PurchDep.WebApi.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PurchDep.WebApi.Tests.Controllers
{
    [Collection("ApiController collection")]
    public class StockApiControllerTests
    {
        Mock<StockService> _serviceMock;
        public StockApiControllerTests(ApiControllerFixture fixture)
        {
            _serviceMock = fixture.StockServiceMock;
        }

        [Fact]
        public void GetAll_Returns_Ok_Test()
        {
            _serviceMock.Setup(service => service.GetAll()).Returns(TestData.StocksDom);

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.GetAll();
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as ICollection<Stock>;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.NotEqual(0, returnedObj!.Count);
            _serviceMock.Verify(service => service.GetAll());
        }

        [Fact]
        public void GetAll_Returns_NoContent_Test()
        {
            _serviceMock.Setup(service => service.GetAll()).Returns(new List<Stock>());

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.GetAll();
            var returnedRes = actionRes as NoContentResult;

            Assert.True(returnedRes!.StatusCode == 204);
            _serviceMock.Verify(service => service.GetAll());
        }

        [Fact]
        public void GetById_Returns_Ok_Test()
        {
            _serviceMock.Setup(service => service.Get(It.IsAny<int>())).Returns(TestData.StockDom_1);

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.GetById(1);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Stock;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.StockDom_1.Id, returnedObj!.Id);
            Assert.Equal(TestData.StockDom_1.Name, returnedObj.Name);
            _serviceMock.Verify(service => service.Get(It.IsAny<int>()));
        }

        [Theory]
        [InlineData(404)]
        public void GetById_Returns_NotFound_Test(int id)
        {
            _serviceMock.Setup(service => service.Get(id)).Throws(new ArgumentException());

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.GetById(id);
            var returnedRes = actionRes as NotFoundObjectResult;

            Assert.True(returnedRes!.StatusCode == 404);
            _serviceMock.Verify(service => service.Get(id));
        }

        [Fact]
        public void Add_Returns_Ok_Test()
        {
            _serviceMock.Setup(service => service.Add(TestData.StockDom_ForAdding)).Returns(TestData.StockDom_ForAdding);

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.Add(TestData.StockDom_ForAdding);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Stock;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.StockDom_ForAdding.Id, returnedObj!.Id);
            Assert.Equal(TestData.StockDom_ForAdding.Name, returnedObj.Name);
            _serviceMock.Verify(service => service.Add(TestData.StockDom_ForAdding));
        }

        [Fact]
        public void AddNull_Returns_BadRequest_Test()
        {
            _serviceMock.Setup(service => service.Add(null!)).Throws(new ArgumentNullException());

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.Add(null!);
            var returnedRes = actionRes as BadRequestObjectResult;

            Assert.True(returnedRes!.StatusCode == 400);
            _serviceMock.Verify(service => service.Add(null!));
        }

        [Fact]
        public void AddAlreadyExsistsItem_Returns_BadRequest_Test()
        {
            _serviceMock.Setup(service => service.Add(TestData.StockDom_1)).Throws(new ArgumentException());

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.Add(TestData.StockDom_1);
            var returnedRes = actionRes as BadRequestObjectResult;

            Assert.True(returnedRes!.StatusCode == 400);
            _serviceMock.Verify(service => service.Add(TestData.StockDom_1));
        }

        [Fact]
        public void Edit_Returns_Ok_Test()
        {
            _serviceMock.Setup(service => service.Update(It.IsAny<int>(), TestData.StockDom_ForUpdating)).Returns(TestData.StockDom_ForUpdating);

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.Edit(1, TestData.StockDom_ForUpdating);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Stock;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.StockDom_ForUpdating.Id, returnedObj!.Id);
            Assert.Equal(TestData.StockDom_ForUpdating.Name, returnedObj.Name);
            _serviceMock.Verify(service => service.Update(It.IsAny<int>(), TestData.StockDom_ForUpdating));
        }

        [Fact]
        public void Edit_Returns_BadRequest_Test()
        {
            _serviceMock.Setup(service => service.Update(It.IsAny<int>(), null!)).Throws(new ArgumentNullException());

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.Edit(1, null!);
            var returnedRes = actionRes as BadRequestObjectResult;

            Assert.True(returnedRes!.StatusCode == 400);
            _serviceMock.Verify(service => service.Update(It.IsAny<int>(), null!));
        }

        [Theory]
        [InlineData(404)]
        public void Edit_Returns_NotFound_Test(int id)
        {
            _serviceMock.Setup(service => service.Update(id, It.IsAny<Stock>())).Throws(new ArgumentException());

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.Edit(id, TestData.StockDom_ForUpdating);
            var returnedRes = actionRes as NotFoundObjectResult;

            Assert.True(returnedRes!.StatusCode == 404);
            _serviceMock.Verify(service => service.Update(id, It.IsAny<Stock>()));
        }

        [Fact]
        public void Delete_Returns_Ok_Test()
        {
            _serviceMock.Setup(service => service.Delete(It.IsAny<int>())).Returns(TestData.StockDom_ForDeleting);

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.Delete(1);
            var returnedRes = actionRes as OkObjectResult;
            var returnedObj = returnedRes!.Value as Stock;

            Assert.True(returnedRes.StatusCode == 200);
            Assert.Equal(TestData.StockDom_ForDeleting.Id, returnedObj!.Id);
            Assert.Equal(TestData.StockDom_ForDeleting.Name, returnedObj.Name);
            _serviceMock.Verify(service => service.Delete(It.IsAny<int>()));
        }

        [Theory]
        [InlineData(404)]
        public void Delete_Returns_NotFound_Test(int id)
        {
            _serviceMock.Setup(service => service.Delete(id)).Throws(new ArgumentException());

            var controller = new StockApiController(_serviceMock.Object);
            var actionRes = controller.Delete(id);
            var returnedRes = actionRes as NotFoundObjectResult;

            Assert.True(returnedRes!.StatusCode == 404);
            _serviceMock.Verify(service => service.Delete(id));
        }

    }
}
