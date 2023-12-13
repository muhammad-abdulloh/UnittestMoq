using Moq;
using UnittestMoq.Controllers;
using UnittestMoq.Models;
using UnittestMoq.Services;

namespace UnitTestMoq.Tests
{
    public class UnitTestController
    {
        private readonly Mock<IProductService> productService;
        public UnitTestController()
        {
            productService = new Mock<IProductService>();
        }
        [Fact]
        public void GetProductList_ProductList()
        {
            //arrange
            var productList = GetProductsData();
            productService.Setup(x => x.GetProductList())
                .Returns(productList);
            var productController = new ProductController(productService.Object);
            //act
            var productResult = productController.ProductList();
            //assert
            Assert.NotNull(productResult);
            Assert.Equal(GetProductsData().Count(), productResult.Count());
            Assert.Equal(GetProductsData().ToString(), productResult.ToString());
            Assert.True(productList.Equals(productResult));
        }
        [Fact]
        public void GetProductByID_Product()
        {
            //arrange
            var productList = GetProductsData();
            productService.Setup(x => x.GetProductById(2))
                .Returns(productList[1]);
            var productController = new ProductController(productService.Object);
            //act
            var productResult = productController.GetProductById(2);
            //assert
            Assert.NotNull(productResult);
            Assert.Equal(productList[1].ProductId, productResult.ProductId);
            Assert.True(productList[1].ProductId == productResult.ProductId);
        }
        [Theory]
        [InlineData("IPhone")]
        public void CheckProductExistOrNotByProductName_Product(string productName)
        {
            //arrange
            var productList = GetProductsData();
            productService.Setup(x => x.GetProductList())
                .Returns(productList);
            var productController = new ProductController(productService.Object);
            //act
            var productResult = productController.ProductList();
            var expectedProductName = productResult.ToList()[0].ProductName;
            //assert
            Assert.Equal(productName, expectedProductName);
        }

        [Fact]
        public void AddProduct_Product()
        {
            //arrange
            var productList = GetProductsData();
            productService.Setup(x => x.AddProduct(productList[2]))
                .Returns(productList[2]);
            var productController = new ProductController(productService.Object);
            //act
            var productResult = productController.AddProduct(productList[2]);
            //assert
            Assert.NotNull(productResult);
            Assert.Equal(productList[2].ProductId, productResult.ProductId);
            Assert.True(productList[2].ProductId == productResult.ProductId);
        }

        private List<Product> GetProductsData()
        {
            List<Product> productsData = new List<Product>
        {
            new Product
            {
                ProductId = 1,
                ProductName = "IPhone",
                ProductDescription = "IPhone 12",
                ProductPrice = 55000,
                ProductStock = 10
            },
             new Product
            {
                ProductId = 2,
                ProductName = "Laptop",
                ProductDescription = "HP Pavilion",
                ProductPrice = 100000,
                ProductStock = 20
            },
             new Product
            {
                ProductId = 3,
                ProductName = "TV",
                ProductDescription = "Samsung Smart TV",
                ProductPrice = 35000,
                ProductStock = 30
            },
        };
            return productsData;
        }
    }
}