using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineProject;

namespace VendingMachineTests
{
    [TestClass]
    public class VendorTests
    {
        VendingMachine vm;

        [TestInitialize]
        public void InitializeTests()
        {
            vm = new VendingMachine();
        }

        #region Coin Related Tests
        [TestMethod]
        public void VendingMachineAcceptsValidCoins()
        {
            var totalInsertedCoins = vm.InsertCoin(Size.TwentyOneMM, Weight.FiveGrams);
            totalInsertedCoins = vm.InsertCoin(Size.SeventeenMM, Weight.TwoGrams);
            totalInsertedCoins = vm.InsertCoin(Size.TwentyFourMM, Weight.FiveAndAHalfGrams);

            Assert.IsTrue(totalInsertedCoins == 40);
        }

        [TestMethod]
        public void VendingMachineRejectsInValidCoins()
        {
            var totalInsertedCoins = vm.InsertCoin(Size.NineteenMM, Weight.TwoAndAHalfGrams);

            Assert.IsTrue(totalInsertedCoins == 0);
        }

        [TestMethod]
        public void VendingMachineReturnsRejectedCoins()
        {
            vm.InsertCoin(Size.NineteenMM, Weight.TwoAndAHalfGrams);
           
            var coinReturn = vm.DisplayCoinReturn();

            Assert.IsTrue(coinReturn == 1);
        }

        [TestMethod]
        public void VendingMachineDisplaysInsertCoinMessageWhenNoCoinsInserted()
        {
            var message = vm.DisplayTotalAmount();

            Assert.IsTrue(message == "INSERT COIN");
        }

        [TestMethod]
        public void VendingMachineDisplaysTotalAmountOfInsertedCoins()
        {
            var totalInsertedCoins = vm.InsertCoin(Size.TwentyFourMM, Weight.FiveAndAHalfGrams);
            var message = vm.DisplayTotalAmount();

            Assert.IsTrue(message == totalInsertedCoins.ToString());
        }
        #endregion

        #region Product Related Tests
        [TestMethod]
        public void VendingMachineDisplaysAvailableProducts()
        {
            var productList = vm.GetProducts();

            Assert.IsTrue(productList.Count == 3);
            Assert.IsTrue(productList.Contains(new Product { Name = "cola", Price = 1.00m }));
            Assert.IsTrue(productList.Contains(new Product { Name = "chips", Price = .50m }));
            Assert.IsTrue(productList.Contains(new Product { Name = "candy", Price = .65m }));
        }

        [TestMethod]
        public void VendingMachineDisplaysItemPriceWhenNotEnoughMoneyInserted()
        {
            var item = vm.GetProducts();

            vm.SelectItem(item.Find(c => c.Name == "cola"));
            var colaPrice = vm.DisplayItemPrice();

            vm.SelectItem(item.Find(c => c.Name == "chips"));
            var chipPrice = vm.DisplayItemPrice();

            vm.SelectItem(item.Find(c => c.Name == "candy"));
            var candyPrice = vm.DisplayItemPrice();

            Assert.IsTrue(colaPrice.ToString() == "PRICE $1.00");
            Assert.IsTrue(chipPrice.ToString() == "PRICE $0.50");
            Assert.IsTrue(candyPrice.ToString() == "PRICE $0.65");
        }

        #endregion
    }
}