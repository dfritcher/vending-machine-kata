using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineProject
{
    class Program
    {
        
        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();
            var penny = new Coin { Size = Size.NineteenMM, Weight = Weight.TwoAndAHalfGrams };
            var nickel = new Coin { Size = Size.TwentyOneMM, Weight = Weight.FiveGrams };
            var dime = new Coin { Size = Size.SeventeenMM, Weight = Weight.TwoGrams };
            var quarter = new Coin { Size = Size.TwentyFourMM, Weight = Weight.FiveAndAHalfGrams };
            
            var products = vm.GetProducts();
            Console.WriteLine("**** Vending Machine Items ****");
            foreach (var product in products)
            {
                Console.WriteLine(string.Format("{0}. {1} ${2}", product.ItemNumber, product.Name, product.Price));
            }
            Console.WriteLine("******************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("COMMANDS");
            Console.WriteLine("R: Return Coins");
            Console.WriteLine("SR: Show Coin Return Amount");
            Console.WriteLine("P: Insert Penny");
            Console.WriteLine("N: Insert Nickle");
            Console.WriteLine("D: Insert Dime");
            Console.WriteLine("Q: Insert Quarter");
            Console.WriteLine("S: STOP APPLICATION");

            var selection = Console.ReadLine().ToUpper();

            while (selection != "S")
            {
                switch (selection)
                {
                    case "1":
                    case "2":
                    case "3":
                        vm.SelectItem(products.Find(p => p.ItemNumber.ToString() == selection));
                        break;
                    case "R":
                        vm.SelectCoinReturn();
                        break;
                    case "P":
                        vm.InsertCoin(penny);
                        break;
                    case "N":
                        vm.InsertCoin(nickel);
                        break;
                    case "D":
                        vm.InsertCoin(dime);
                        break;
                    case "Q":
                        vm.InsertCoin(quarter);
                        break;
                    case "SR":
                        Console.WriteLine(vm.DisplayCoinReturn());
                        break;
                }
                Console.WriteLine(vm.DisplayVendMessage());

                selection = Console.ReadLine().ToUpper();
            }
        }
    }

    public class VendingMachine
    {
        #region Fields
        private decimal coinReturnTotal = 0m;
        private decimal insertedCoinTotal = 0m;
        private Product selectedItem;
        private Product dispensedItem;
        private bool itemPurchased = false;
        private bool displayPrice = false;
        private string displayMessage = string.Empty;

        private List<Product> products = new List<Product>() {
            new Product { Name = "cola", Price = 1.00m, ItemNumber = 1 },
            new Product { Name = "chips", Price = 0.50m, ItemNumber = 2 },
            new Product { Name = "candy", Price = 0.65m, ItemNumber = 3 }
        };
        #endregion

        #region Properties
        private bool HasEnoughMoneyForSelectedItem
        {
            get {
                return selectedItem.Price > 0
                  && !string.IsNullOrEmpty(selectedItem.Name)
                  && insertedCoinTotal >= selectedItem.Price;
            }
        }
        #endregion

        #region Public Methods
        public void InsertCoin(Coin coin)
        {
            if (coin.Size == Size.TwentyOneMM && coin.Weight == Weight.FiveGrams)
            {
                insertedCoinTotal += .05m;
            }
            if (coin.Size == Size.SeventeenMM && coin.Weight == Weight.TwoGrams)
            {
                insertedCoinTotal += .10m;
            }

            if (coin.Size == Size.TwentyFourMM && coin.Weight == Weight.FiveAndAHalfGrams)
            {
                insertedCoinTotal += .25m;
            }

            if (coin.Size == Size.NineteenMM && coin.Weight == Weight.TwoAndAHalfGrams)
            {
                coinReturnTotal += .01m;
            }
        }

        public string DisplayCoinReturn()
        {
            return Utilities.FormatCurrency(coinReturnTotal);
        }

        public string DisplayVendMessage()
        {
            CalculateVendMessage();
            return displayMessage;
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public Product GetDispensedItem()
        {
            return dispensedItem;
        }

        public void SelectItem(Product item)
        {
            selectedItem = item;
            displayPrice = true;
            if (HasEnoughMoneyForSelectedItem)
                DispenseItem();
        }

        public void SelectCoinReturn()
        {
            coinReturnTotal += insertedCoinTotal;
            insertedCoinTotal = 0;
        }

        #endregion

        #region Private Methods
        private void DispenseItem()
        {
            itemPurchased = true;
            coinReturnTotal = insertedCoinTotal - selectedItem.Price;
            insertedCoinTotal = 0;

            dispensedItem = selectedItem;
        }

        private string DisplayItemPrice()
        {
            return "PRICE " + Utilities.FormatCurrency(selectedItem.Price);
        }

        private string DisplayCurrencyTotal()
        {
            return Utilities.FormatCurrency(insertedCoinTotal);
        }

        private void CalculateVendMessage()
        {
            if (itemPurchased)
            {
                itemPurchased = false;
                displayPrice = false;
                UpdateVendMessage("THANK YOU");
                return;
            }
            if (insertedCoinTotal == 0)
            {
                UpdateVendMessage("INSERT COIN");
                return;
            }
            if (displayPrice)
            {
                displayPrice = false;
                UpdateVendMessage(DisplayItemPrice());
                return;
            }
            UpdateVendMessage(DisplayCurrencyTotal());
        }

        private void UpdateVendMessage(string message)
        {
            displayMessage = message;
        }
        #endregion
    }

    public static class Utilities
    {
        public static string FormatCurrency(decimal amount)
        {
            return string.Format("{0:C2}", amount);
        }
    }
}
