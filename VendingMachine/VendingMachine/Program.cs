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

        }
    }

    public class VendingMachine
    {
        private decimal coinReturnTotal = 0m;
        private decimal insertedCoinTotal = 0m;
        private Product selectedItem;
        private Product dispensedItem;
        private bool isValidCoin = false;
        private bool itemPurchased = false;
        private bool displayPrice = false;
        private string displayMessage = string.Empty;

        private List<Product> products = new List<Product>() {
            new Product { Name = "cola", Price = 1.00m },
            new Product { Name = "chips", Price = 0.50m},
            new Product { Name = "candy", Price = 0.65m }
        };

        private bool HasEnoughMoneyForSelectedItem
        {
            get {
                return selectedItem.Price > 0
                  && !string.IsNullOrEmpty(selectedItem.Name)
                  && insertedCoinTotal >= selectedItem.Price;
            }
        }

        public void InsertCoin(Coin coin)
        {
            if (coin.Size == Size.TwentyOneMM && coin.Weight == Weight.FiveGrams)
            {
                isValidCoin = true;
                insertedCoinTotal += .05m;
            }
            if (coin.Size == Size.SeventeenMM && coin.Weight == Weight.TwoGrams)
            {
                isValidCoin = true;
                insertedCoinTotal += .10m;
            }

            if (coin.Size == Size.TwentyFourMM && coin.Weight == Weight.FiveAndAHalfGrams)
            {
                isValidCoin = true;
                insertedCoinTotal += .25m;
            }

            if (coin.Size == Size.NineteenMM && coin.Weight == Weight.TwoAndAHalfGrams)
            {
                isValidCoin = false;
                coinReturnTotal += .01m;
            }
        }

        public string DisplayCoinReturn()
        {
            return Utilities.FormatCurrency(coinReturnTotal);
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public void SelectItem(Product item)
        {
            selectedItem = item;
            displayPrice = true;
            if(HasEnoughMoneyForSelectedItem)
                DispenseItem();
        }

        public Product GetDispensedItem()
        {
            return dispensedItem;
        }

        private void DispenseItem()
        {
            itemPurchased = true;
            insertedCoinTotal = insertedCoinTotal - selectedItem.Price;

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

        public string DisplayVendMessage()
        {
            CalculateVendMessage();
            return displayMessage;
        }
    }

    public static class Utilities
    {
        public static string FormatCurrency(decimal amount)
        {
            return string.Format("{0:C2}", amount);
        }
    }
}
