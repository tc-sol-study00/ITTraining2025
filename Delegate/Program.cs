namespace Delegate {
    internal class Program {
        static void Main(string[] args) {

            Func<decimal, decimal, decimal> ShohiZei = (argOriginPrice, argConsumptionTaxRate) => {
                return argOriginPrice * argConsumptionTaxRate;
            };


        }
    }
}
