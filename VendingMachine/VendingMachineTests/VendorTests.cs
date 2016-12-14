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
        Product cola;
        Product chips;
        Product candy; 

        [TestInitialize]
        public void InitializeTests()
        {
            vm = new VendingMachine();
            penny = new Coin { Size = Size.NineteenMM, Weight = Weight.TwoAndAHalfGrams };
            nickel = new Coin { Size = Size.TwentyOneMM, Weight = Weight.FiveGrams };
            dime = new Coin { Size = Size.SeventeenMM, Weight = Weight.TwoGrams };
            quarter = new Coin { Size = Size.TwentyFourMM, Weight = Weight.FiveAndAHalfGrams };

            cola = new Product { Name = "cola", Price = 1.00m };
            chips = new Product { Name = "chips", Price = 0.50m };
            candy = new Product { Name = "candy", Price = 0.65m };
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
            var message = vm.DisplayVendMessage();

            Assert.IsTrue(message == "INSERT COIN");
        }

        [TestMethod]
        public void VendingMachineDisplaysTotalAmountOfInsertedCoins()
        {
            var totalInsertedCoins = vm.InsertCoin(quarter);
            var message = vm.DisplayVendMessage();

            Assert.IsTrue(message == totalInsertedCoins.ToString());
        }
        #endregion

        #region Product Related Tests
        [TestMethod]
        public void VendingMachineDisplaysAvailableProducts()
        {
            var productList = vm.GetProducts();

            Assert.IsTrue(productList.Count == 3);
            CollectionAssert.Contains(productList, cola);
            CollectionAssert.Contains(productList, chips);
            CollectionAssert.Contains(productList, candy);
        }

        [TestMethod]
        public void VendingMachineDispensesProductWhenEnoughMoneyIsInserted()
        {
            var items = vm.GetProducts();

            vm.InsertCoin(quarter);
            vm.InsertCoin(quarter);
            vm.InsertCoin(quarter);
            vm.InsertCoin(quarter);

            vm.SelectItem(cola);

            var dispensedItem = vm.DispenseItem();

            Assert.IsNotNull(dispensedItem);
            Assert.IsTrue(dispensedItem.Name == cola.Name);
        }

        [TestMethod]
        public void VendingMachineDisplaysMessage()
        {
            var dispenseItem = vm.DispenseItem(); 

            var message = vm.DisplayVendMessage();

            Assert.IsTrue(message == "THANK YOU");
        }

        [TestMethod]
        public void VendingMachineDisplaysMessageAfterMultipleChecks()
        {
            var dispenseItem = vm.DispenseItem();

            var message = vm.DisplayVendMessage();

            var message2 = vm.DisplayVendMessage();

            Assert.IsTrue(message == "THANK YOU");
            Assert.IsTrue(message2 == "INSERT COIN");
        }

        [TestMethod]
        public void VendingMachineDisplaysItemPriceWhenNotEnoughMoneyInserted()
        {
            var items = vm.GetProducts();
            vm.InsertCoin(quarter);

            vm.SelectItem(items.Find(c => c.Name == "cola"));
            var colaPrice = vm.DisplayVendMessage();

            vm.SelectItem(items.Find(c => c.Name == "chips"));
            var chipPrice = vm.DisplayVendMessage();

            vm.SelectItem(items.Find(c => c.Name == "candy"));
            var candyPrice = vm.DisplayVendMessage();

            Assert.IsTrue(colaPrice.ToString() == "PRICE $1.00");
            Assert.IsTrue(chipPrice.ToString() == "PRICE $0.50");
            Assert.IsTrue(candyPrice.ToString() == "PRICE $0.65");
        }

        
        #endregion
    }
}