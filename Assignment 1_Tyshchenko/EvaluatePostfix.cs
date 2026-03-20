namespace Assignment_1_Tyshchenko;

public class EvaluatePostfix
{
    // виводити помилку, якщо підряд йдуть операції або числа!!!!
    
    string operators = "+-*/^";
    string functions = "sin cos max min";
    
    // EVALUATE POSTFIX
    public double EvaluatePostfixNotation(string postfix)
    {
        MyStack stack = new MyStack();
        string[] tokens = postfix.Split(' ');

        foreach (string token in tokens)
        {
            // звичайні операції
            if (operators.Contains(token)) // погана перевірка
            {
                double b = Convert.ToDouble(stack.Pop());
                double a = Convert.ToDouble(stack.Pop());
                double result = token switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    "^" => Math.Pow(a, b),
                    _ => throw new Exception($"Невідомий оператор: {token}")
                };
                stack.Push(result.ToString());
            }
            // функції
            else if (functions.Contains(token)) // погана перевірка
            {
                if (token == "sin" || token == "cos")
                {
                    double a = Convert.ToDouble(stack.Pop());  // один аргумент
                    double result = token switch
                    {
                        "sin" => Math.Sin(a),
                        "cos" => Math.Cos(a),
                        _ => throw new Exception($"Невідома функція: {token}")
                    };
                    stack.Push(result.ToString());
                }
                else // max, min
                {
                    double b = Convert.ToDouble(stack.Pop());  // два аргументи
                    double a = Convert.ToDouble(stack.Pop());
                    double result = token switch
                    {
                        "max" => Math.Max(a, b),
                        "min" => Math.Min(a, b),
                        _ => throw new Exception($"Невідома функція: {token}")
                    };
                    stack.Push(result.ToString());
                }
            }
            else
            {
                stack.Push(token);
            }
        }
        return Convert.ToDouble(stack.Pop());
    }
}