using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineProject;

namespace VendingMachineTests
{
    [TestClass]
    public class VendorTests
    {
        VendingMachine vm;
        Coin penny;
        Coin nickel;
        Coin dime;
        Coin quarter;

        [TestInitialize]
        public void InitializeTests()
        {
            vm = new VendingMachine();
            penny = new Coin { Size = Size.NineteenMM, Weight = Weight.TwoAndAHalfGrams };
            nickel = new Coin { Size = Size.TwentyOneMM, Weight = Weight.FiveGrams };
            dime = new Coin { Size = Size.SeventeenMM, Weight = Weight.TwoGrams };
            quarter = new Coin { Size = Size.TwentyFourMM, Weight = Weight.FiveAndAHalfGrams };
        }

        #region Coin Related Tests
        [TestMethod]
        public void VendingMachineAcceptsValidCoins()
        {
            var totalInsertedCoins = vm.InsertCoin(nickel);
            totalInsertedCoins = vm.InsertCoin(dime);
            totalInsertedCoins = vm.InsertCoin(quarter);

            Assert.IsTrue(totalInsertedCoins == .40m);
        }

        [TestMethod]
        public void VendingMachineRejectsInValidCoins()
        {
            var totalInsertedCoins = vm.InsertCoin(penny);

            Assert.IsTrue(totalInsertedCoins == 0);
        }

        [TestMethod]
        public void VendingMachineReturnsRejectedCoins()
        {
            vm.InsertCoin(penny);
           
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
            var totalInsertedCoins = vm.InsertCoin(quarter);
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

        [TestMethod]
        public void VendingMachineDispensesProductWhenEnoughMoneyIsInserted()
        {
            var item = vm.GetProducts();

            vm.InsertCoin(quarter);
        }
        #endregion
    }
}