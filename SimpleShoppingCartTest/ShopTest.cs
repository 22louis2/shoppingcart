using NUnit.Framework;
using SimpleShoppingCart.Controller;
using System.Linq;

namespace SimpleShoppingCartTest
{
    public class Tests
    {
        ProductController Product = new ProductController();
        CartController Cart = new CartController();

        [Test]
        public void NumberOfPageReturnedByOffsetting()
        {
            //Arrange
            int span = 5;
            int offset = 0;

            //Act
            var count = Product.GetAllProductByOffSet(offset, span).Count;

            Assert.That(count, Is.Not.GreaterThan(span));
        }

        [Test]
        public void CheckIfItAddsToCart()
        {
            //Arrange
            var initialcartSize = Cart.GetAllCart().Count;
            var idOfLastProduct = Product.GetAllProduct().LastOrDefault().ProductId; 

            //Act
            Cart.AddCart(idOfLastProduct, 4);
            var actualSize = Cart.GetAllCart().Count;
            var expected = initialcartSize + 1;

            //Assert
            Assert.That(expected, Is.EqualTo(actualSize));
        }

        [Test]
        public void CheckIfAddProductWorks()
        {
            //Arrange
            var initialcartSize = Product.GetAllProduct().Count;

            //Act
            Product.AddProduct("Knives", 9);

            var actualSize = Product.GetAllProduct().Count;
            var expected = initialcartSize + 1;

            //Assert
            Assert.That(expected, Is.EqualTo(actualSize));
        }

        [Test]
        public void CheckIfDeletingProductWorks()
        {
            //Arrange
            var initialcartSize = Product.GetAllProduct().Count;
            var IdOfLastProduct = Product.GetAllProduct().LastOrDefault().ProductId;

            //Act
            Product.RemoveProduct(IdOfLastProduct);

            var actualSize = Product.GetAllProduct().Count;
            var expected = initialcartSize - 1;

            //Assert
            Assert.That(expected, Is.EqualTo(actualSize));
        }
    }
}