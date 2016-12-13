namespace VendingMachineProject
{
    public enum Size
    {
        NineteenMM, // penny
        TwentyOneMM, // nickel
        SeventeenMM, // dime
        TwentyFourMM // quarter
    };

    public enum Weight
    {
        TwoAndAHalfGrams, // penny 
        FiveGrams, // nickel
        TwoGrams, // dime
        FiveAndAHalfGrams // quarter
    }

    public struct Coin
    {
        public Size Size;
        public Weight Weight;
    }

    public struct Product
    {
        public string Name;
        public decimal Price;
    }
}
