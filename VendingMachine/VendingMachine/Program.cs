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
        private int insertedCoinTotal = 0;
        private Product selectedItem;
        private List<Product> products = new List<Product>() {
            new Product { Name = "cola", Price = 1.00m },
            new Product { Name = "chips", Price = .50m},
            new Product { Name = "candy", Price = .65m }
        };

        public int InsertCoin(Size coinSize, Weight coinWeight)
        {
            if (coinSize == Size.TwentyOneMM && coinWeight == Weight.FiveGrams)
                insertedCoinTotal += 5;
            
            if (coinSize == Size.SeventeenMM && coinWeight == Weight.TwoGrams)
                insertedCoinTotal += 10;

            if (coinSize == Size.TwentyFourMM && coinWeight == Weight.FiveAndAHalfGrams)
                insertedCoinTotal += 25;

            if (coinSize == Size.NineteenMM && coinWeight == Weight.TwoAndAHalfGrams)
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

        }

        public decimal DisplayItemPrice()
        {
            return selectedItem.Price;
        }
    }
}
