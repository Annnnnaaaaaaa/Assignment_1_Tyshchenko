namespace Assignment_1_Tyshchenko;

public class ShuntingYard
{
    // SHUNTING YARD
    public string operators = "^*/+-";
    public int OperatorsPriority(string op)
    {
        return op switch
        {
            "sin" or "cos" or "max" or "min" => 4,
            "^" => 3,
            "*" or "/" => 2,
            "+" or "-" => 1,
            _ => 0
        };
    }

    public string ToPostfix(
            string infixInput) //(To postfix): приймає стрінг з токенами в інфіксному вигляді через пробіл, повертає стрінг з токенами в постфіксному вигляді через пробіл
    {
        string res = "";
        string[] tokens = infixInput.Split(" ");
        MyStack stack = new MyStack();

        foreach (string t in tokens)
        {
            // коли токен число
            if (char.IsDigit(t[0]) || (t[0] == '-' && t.Length > 1 && char.IsDigit(t[1])))
            {
                res += t + " ";
            }
            else if (t == ",")
            {
                // виштовхуємо оператори до відкриваючої дужки
                while (!stack.IsEmpty() && stack.Peek() != "(")
                    res += stack.Pop() + " ";
                // '(' залишаємо в стеку — вона ще потрібна
            }
            // коли токен оператор
            else if (operators.Contains(t))
            {
                while (!stack.IsEmpty() && 
                       stack.Peek() != "(" && 
                       (OperatorsPriority(stack.Peek()) > OperatorsPriority(t) || 
                        (OperatorsPriority(stack.Peek()) == OperatorsPriority(t) && t != "^")))
                {
                    res += stack.Pop() + " ";
                }
                stack.Push(t);
            }
            // коли токен дужка
            else if (t == "(")
            {
                stack.Push(t);
            }
            else if (t == ")")
            {
                while (stack.Peek() != "(")
                {
                    res += stack.Pop() + " ";
                }
                stack.Pop();

                if (!stack.IsEmpty() && 
                    (stack.Peek() == "sin" || stack.Peek() == "cos" || stack.Peek() == "max" || stack.Peek() == "min") )
                {
                    res += stack.Pop() + " ";
                }
            }
            // коли токен функція
            else if (t == "sin" || t == "cos" || t == "max" || t == "min")
            {
                stack.Push(t);
            }
        }

        while (!stack.IsEmpty())
        {
            res += stack.Pop() + " ";
        }
        
        return res.Trim();
    }
}