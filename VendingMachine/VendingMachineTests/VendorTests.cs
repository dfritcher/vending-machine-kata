using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;

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
            Assert.IsTrue(totalInsertedCoins = 5);
        }
    }
}
