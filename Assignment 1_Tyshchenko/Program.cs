namespace Assignment_1_Tyshchenko;

public class Program
{
    static void Main()
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        
        Tokenization tokenization = new Tokenization();
        ShuntingYard shuntingYard = new ShuntingYard();
        EvaluatePostfix evaluatePostfixNotation = new EvaluatePostfix();
        Variables storage = new Variables();
        
        Console.Write("В цьому калькуляторі:\n1) Не можна вводити дробні числа (але отримати можна)\n2) Від'ємні числа вводяться в дужках (якщо не на початку і не перед дужками)\n3) Змінні можна називати тільки 1 символом\n4) Для закінчення напишіть 'exit'\n");
        while (true)
        {
            Console.Write("Введіть вираз: ");
            string input = Console.ReadLine();
            // string input = "2 * (-3)"; 
            // string input = "sin(1) + 3 * (5 + 2) - 2^2";

            if (input == "exit") break;
            
            try
            {
                if (input.Contains('='))
                {
                    string varName = input.Split('=')[0].Trim(); // [0] — все що до =
                    string expr = storage.ReplaceVariables(input.Split('=')[1].Trim()); // [1] — все що після =
                    string tokens = tokenization.TokenizationFunction(expr);
                    string postfix = shuntingYard.ToPostfix(tokens);
                    double result = evaluatePostfixNotation.EvaluatePostfixNotation(postfix);
                    storage.Set(varName, result);
                    Console.WriteLine(Math.Round(result, 4));
                }
                else
                {
                    Console.WriteLine($"Input: {input}");
                    string expr = storage.ReplaceVariables(input);
                    string tokens = tokenization.TokenizationFunction(expr);
                    string postfixNotation = shuntingYard.ToPostfix(tokens);
                    Console.WriteLine($"In postfix notation: {postfixNotation}");
                    double result = evaluatePostfixNotation.EvaluatePostfixNotation(postfixNotation);
                    Console.WriteLine($"Result: {Math.Round(result, 4)}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
        }
    }
}