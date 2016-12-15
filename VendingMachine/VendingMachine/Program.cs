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
        private int coinReturnTotal = 0;
        private decimal insertedCoinTotal = 0;
        private Product selectedItem;
        private Product dispensedItem;
        private bool itemPurchased = false;
        private bool displayPrice = false;

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

        public string InsertCoin(Coin coin)
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
                coinReturnTotal++;
            }

            return DisplayCurrencyTotal();
        }

        public int DisplayCoinReturn()
        {
            return coinReturnTotal;
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public string SelectItem(Product item)
        {
            selectedItem = item;
            displayPrice = true;
            if(HasEnoughMoneyForSelectedItem)
                DispenseItem();

            return DisplayVendMessage();
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
            return string.Format("PRICE {0:C2}", selectedItem.Price);
        }

        private string DisplayCurrencyTotal()
        {
            return string.Format("{0:C2}", insertedCoinTotal);
        }

        private string DisplayVendMessage()
        {
            if (itemPurchased)
            {
                itemPurchased = false;
                return "THANK YOU";
            }
            if (insertedCoinTotal == 0)
                return "INSERT COIN";
            if (displayPrice)
            {
                displayPrice = false;
                return DisplayItemPrice();
            }
            return DisplayCurrencyTotal();
        }
    }
}
