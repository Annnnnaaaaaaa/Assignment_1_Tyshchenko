namespace Assignment_1_Tyshchenko;

public class Program
{
    // SHUNTING YARD
    static bool IsNumber(char ch) => char.IsDigit(ch) || ch == '.';

    static bool IsOperator(char ch) => ch == '+' || ch == '-' || ch == '/' || ch == '*' || ch == '^';

    static int OperatorsPriority(char ch)
    {
        return ch switch
        {
            '^' => 3,
            '*' or '/' => 2,
            '+' or '-' => 1,
            _ => 0
        };
    }

    static string ShuntingYard(string input)
    {
        Stack stack = new Stack();
        string res = "";

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (c == ' ')
            {
                continue;
            }

            if (IsNumber(c) || (c == '-' && i == 0) || (c == '-' && input[i - 1] == '('))
            {
                string number = "";
                if (c == '-')
                {
                    number += c; 
                    i++; 
                }

                while (i < input.Length && IsNumber(input[i]))
                {
                    number += input[i];
                    i++;
                }
                i--; // щоб не пропустити наступні символи
                res += number + " ";
            }
            else if (c == '(')
            {
                stack.Push(c.ToString());
            }
            else if (c == ')')
            {
                while (!stack.IsEmpty() && stack.Peek() != "(")
                    res += stack.Pop() + ' ';

                if (stack.IsEmpty())
                    throw new Exception("Not open brackets");

                stack.Pop();
            }
            else if (IsOperator(c))
            {
                while (!stack.IsEmpty() && OperatorsPriority(stack.Peek()[0]) >= OperatorsPriority(c))
                    res += stack.Pop() + ' ';

                stack.Push(c.ToString());
            }
            else
            {
                throw new Exception($"Unrecognizable symbol: {c}");
            }
        }

        while (!stack.IsEmpty())
        {
            string top = stack.Pop();
            if (top == "(") throw new Exception("Not closed brackets");
            res += top + " ";
        }

        return res.Trim();
    }

    
    // EVALUATE POSTFIX
    static int EvaluatePostfixNotation(string postfix)
    {
        Stack stack = new Stack();
        string[] tokens = postfix.Split(' ');

        foreach (string token in tokens)
        {
            if (token == "") continue;

            if (token.Length == 1 && IsOperator(token[0]))
            {
                int b = Convert.ToInt32(stack.Pop());
                int a = Convert.ToInt32(stack.Pop());

                int result = token[0] switch
                {
                    '+' => a + b,
                    '-' => a - b,
                    '*' => a * b,
                    '/' => a / b,
                    '^' => (int)Math.Pow(a, b),
                    _ => throw new Exception($"Unknown operator: {token}")
                };

                stack.Push(result.ToString());
            }
            else
            {
                stack.Push(token);
            }
        }

        return Convert.ToInt32(stack.Pop());
    }

    
    static void Main()
    {
        string input = "1+  3*(  2+ 4)";

        Console.WriteLine($"Input: {input}");

        string rpn = ShuntingYard(input);
        Console.WriteLine($"In postfix notation: {rpn}");

        int result = EvaluatePostfixNotation(rpn);
        Console.WriteLine($"Result: {result}");
    }
}