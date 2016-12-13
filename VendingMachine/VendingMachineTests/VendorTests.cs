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

        [TestMethod]
        public void VendingMachineAcceptsValidCoins()
        {
            var totalInsertedCoins = vm.InsertCoin(Size.TwentyOneMM, Weight.FiveGrams);
            totalInsertedCoins += vm.InsertCoin(Size.SeventeenMM, Weight.TwoGrams);
            totalInsertedCoins += vm.InsertCoin(Size.TwentyFourMM, Weight.FiveAndAHalfGrams);

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
        public void VendingMachineDisplaysInsertCoinMessage()
        {
            var message = vm.DisplayTotalAmount();

            Assert.IsTrue(message == "INSERT COIN");
        }
    }
}
