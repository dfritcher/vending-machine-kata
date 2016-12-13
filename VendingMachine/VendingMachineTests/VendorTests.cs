using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineProject;

namespace VendingMachineTests
{
    [TestClass]
    public class VendorTests
    {
        [TestMethod]
        public void VendingMachineAcceptsValidCoins()
        {
            VendingMachine vm = new VendingMachine();
            var totalInsertedCoins = vm.InsertCoin(Size.TwentyOneMM, Weight.FiveGrams);
            totalInsertedCoins += vm.InsertCoin(Size.SeventeenMM, Weight.TwoGrams);
            totalInsertedCoins += vm.InsertCoin(Size.TwentyFourMM, Weight.FiveAndAHalfGrams);

            Assert.IsTrue(totalInsertedCoins == 40);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void VendingMachineRejectsInValidCoins()
        {
            VendingMachine vm = new VendingMachine();

            vm.InsertCoin(Size.NineteenMM, Weight.TwoAndAHalfGrams);
        }

        [TestMethod]
        public void VendingMachineDisplaysInsertCoinMessage()
        {
            VendingMachine vm = new VendingMachine();
            var message = vm.DisplayTotalAmount();

            Assert.IsTrue(message == "INSERT COIN");
        }
    }
}
