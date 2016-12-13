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

            Assert.IsTrue(totalInsertedCoins == 15);
        }
    }
}
