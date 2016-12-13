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
            Assert.IsTrue(productList.Contains("cola"));
            Assert.IsTrue(productList.Contains("chips"));
            Assert.IsTrue(productList.Contains("candy"));
        }
        #endregion
    }
}