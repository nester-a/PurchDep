using PurchDep.Domain;
using Xunit;

namespace Common.PurchDep.Domain.Tests
{
    public class ProductTests
    {
        Product product;
        Product<string> stringProduct;

        public ProductTests()
        {
            product = new Product();
            stringProduct = new Product<string>();
        }

        [Fact]
        public void Product_is_ProductOfInt_And_IProduct_Test()
        {
            Assert.True(product is Product<int>);
        }

        [Fact]
        public void StringProduct_is_ProductOfString_And_IProductOfString_Test()
        {
            Assert.True(stringProduct is Product<string>);
        }
    }
}
