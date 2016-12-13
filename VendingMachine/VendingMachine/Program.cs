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
        private List<string> products = new List<string>() { "cola", "chips", "candy" };

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

        public List<string> GetProducts()
        {
            return products;
        }
    }
}
