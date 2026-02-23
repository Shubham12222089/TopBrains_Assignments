using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Models;
using FlexibleInventorySystem.Exceptions;

namespace FlexibleInventorySystem.Tests
{
    [TestFixture]
    public class InventoryManagerTests
    {
        private InventoryManager _inventory;

        [SetUp]
        public void Setup()
        {
            _inventory = new InventoryManager();
        }

        [Test]
        public void AddProduct_ValidProduct_ReturnsTrue()
        {
            var product = new ElectronicProduct
            {
                Id = "E100",
                Name = "Phone",
                Price = 30000,
                Quantity = 2,
                Category = "Electronics"
            };

            var result = _inventory.AddProduct(product);

            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void AddProduct_DuplicateId_ThrowsException()
        {
            var product = new ElectronicProduct
            {
                Id = "E101",
                Name = "Tablet",
                Price = 20000,
                Quantity = 1,
                Category = "Electronics"
            };

            _inventory.AddProduct(product);

            Assert.Throws<InventoryException>(() =>
            {
                _inventory.AddProduct(product);
            });
        }

        [Test]
        public void RemoveProduct_NonExisting_ReturnsFalse()
        {
            var result = _inventory.RemoveProduct("INVALID");

            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void GetLowStockProducts_ReturnsCorrectProducts()
        {
            var product = new GroceryProduct
            {
                Id = "G100",
                Name = "Milk",
                Price = 50,
                Quantity = 2,
                Category = "Groceries",
                ExpiryDate = DateTime.Now.AddDays(2)
            };

            _inventory.AddProduct(product);

            var result = _inventory.GetLowStockProducts(5);

            ClassicAssert.AreEqual(1, result.Count);
        }

        [Test]
        public void UpdateQuantity_ValidUpdate_ReturnsTrue()
        {
            var product = new ClothingProduct
            {
                Id = "C100",
                Name = "T-Shirt",
                Price = 799,
                Quantity = 10,
                Category = "Clothing",
                Size = "M"
            };

            _inventory.AddProduct(product);

            var result = _inventory.UpdateQuantity("C100", 5);

            ClassicAssert.IsTrue(result);
            ClassicAssert.AreEqual(5, _inventory.FindProduct("C100").Quantity);
        }
    }
}
