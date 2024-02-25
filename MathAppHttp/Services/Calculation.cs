namespace MathAppHttp.Services
{
    public class Calculation
    {
        public double Calc(double firstNumber, double secondNumber, string operation)
        {
            if (operation == "add") return firstNumber + secondNumber;
            if (operation == "sub") return firstNumber - secondNumber;
            if (operation == "mult") return firstNumber * secondNumber;
            if (operation == "div") return firstNumber / secondNumber;
            if (operation == "mod") return firstNumber % secondNumber;
            return 0;
        }
    }
}
