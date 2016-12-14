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
        private List<Product> products = new List<Product>() {
            new Product { Name = "cola", Price = 1.00m },
            new Product { Name = "chips", Price = 0.50m},
            new Product { Name = "candy", Price = 0.65m }
        };

        public decimal InsertCoin(Coin coin)
        {
            if (coin.Size == Size.TwentyOneMM && coin.Weight == Weight.FiveGrams)
                insertedCoinTotal += .05m;
            
            if (coin.Size == Size.SeventeenMM && coin.Weight == Weight.TwoGrams)
                insertedCoinTotal += .10m;

            if (coin.Size == Size.TwentyFourMM && coin.Weight == Weight.FiveAndAHalfGrams)
                insertedCoinTotal += .25m;

            if (coin.Size == Size.NineteenMM && coin.Weight == Weight.TwoAndAHalfGrams)
                coinReturnTotal++;

            return insertedCoinTotal; ;
        }

        public string DisplayTotalAmount()
        {
            if (insertedCoinTotal == 0)
                return "INSERT COIN";

            return insertedCoinTotal.ToString();
        }

        public int DisplayCoinReturn()
        {
            return coinReturnTotal;
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public void SelectItem(Product item)
        {
            selectedItem = item;
        }

        public Product DispenseItem()
        {
            return selectedItem;
        }

        public string DisplayItemPrice()
        {
            return string.Format("PRICE ${0}", selectedItem.Price);
        }

        public string DisplayVendMessage()
        {
            return "THANK YOU";
        }
    }
}
