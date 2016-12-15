﻿using System;
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

            Assert.IsTrue(totalInsertedCoins == "$0.40");
        }

        [TestMethod]
        public void VendingMachineRejectsInValidCoins()
        {
            var totalInsertedCoins = vm.InsertCoin(penny);

            Assert.IsTrue(totalInsertedCoins == "$0.00");
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
            var message = vm.SelectItem(chips);

            Assert.IsTrue(message == "INSERT COIN");
        }

        [TestMethod]
        public void VendingMachineDisplaysTotalAmountOfInsertedCoins()
        {
            var display = vm.InsertCoin(quarter);

            Assert.IsTrue(display == "$0.25");
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

            var display = vm.SelectItem(cola);
            var dispensedItem = vm.GetDispensedItem();

            Assert.IsNotNull(dispensedItem);
            Assert.IsTrue(dispensedItem.Name == cola.Name);
            Assert.IsTrue(dispensedItem.Price == cola.Price);
        }

        [TestMethod]
        public void VendingMachineDisplaysMessageWhenEnoughMoneyIsInsertedAndItemIsDispensed()
        {
            vm.InsertCoin(quarter);
            vm.InsertCoin(quarter);
            vm.InsertCoin(dime);
            vm.InsertCoin(nickel);
            
            var message = vm.SelectItem(candy);

            Assert.IsTrue(message == "THANK YOU");
        }

        [TestMethod]
        public void VendingMachineDisplaysMessageAfterMultipleChecks()
        {
            vm.InsertCoin(quarter);
            vm.InsertCoin(quarter);
            vm.InsertCoin(dime);
            vm.InsertCoin(nickel);

            var message = vm.SelectItem(candy);

            var message2 = vm.SelectItem(candy);

            Assert.IsTrue(message == "THANK YOU");
            Assert.IsTrue(message2 == "INSERT COIN");
        }

        [TestMethod]
        public void VendingMachineDisplaysItemPriceWhenNotEnoughMoneyInserted()
        {
            var items = vm.GetProducts();
            vm.InsertCoin(quarter);

            var colaPrice = vm.SelectItem(items.Find(c => c.Name == "cola"));
            var chipPrice = vm.SelectItem(items.Find(c => c.Name == "chips"));
            var candyPrice = vm.SelectItem(items.Find(c => c.Name == "candy"));
            
            Assert.IsTrue(colaPrice.ToString() == "PRICE $1.00");
            Assert.IsTrue(chipPrice.ToString() == "PRICE $0.50");
            Assert.IsTrue(candyPrice.ToString() == "PRICE $0.65");
        }

        [TestMethod]
        public void VendingMachineDisplaysCorrectMessageWhenNotEnoughMoneyInsertedAfterMultipleChecks()
        {
            vm.InsertCoin(dime);
            vm.InsertCoin(dime);

            var firstMessage = vm.SelectItem(chips);
            var secondMessage = vm.SelectItem(chips);

            Assert.IsTrue(firstMessage == "PRICE $0.50");
            Assert.IsTrue(secondMessage == "$0.20");
        }
        
        #endregion
    }
}