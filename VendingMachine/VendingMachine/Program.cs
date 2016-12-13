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
        public int InsertCoin(Size coinSize, Weight coinWeight)
        {
            if (coinSize == Size.TwentyOneMM && coinWeight == Weight.FiveGrams)
                return 5;
            if (coinSize == Size.SeventeenMM && coinWeight == Weight.TwoGrams)
                return 10;
            if (coinSize == Size.TwentyFourMM && coinWeight == Weight.FiveAndAHalfGrams)
                return 25;

            throw new InvalidOperationException("Invalid Coin");
        }

        public string DisplayTotalAmount()
        {
            return "INSERT COIN";
        }
    }
}
