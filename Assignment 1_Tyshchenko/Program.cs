namespace Assignment_1_Tyshchenko;

public class Program
{
    static bool IsNumber(char ch) => char.IsDigit(ch) || ch == '.';

    static bool IsOperator(char ch) => ch == '+' || ch == '-' || ch == '/' || ch == '*' || ch == '^';

    static int OpPreced(char ch)
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
        string output = "";

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (c == ' ') continue;

            if (IsNumber(c) || (c == '-' && (i == 0 || input[i - 1] == '(')))
            {
                string number = "";
                if (c == '-') { number += c; i++; }

                while (i < input.Length && IsNumber(input[i]))
                {
                    number += input[i];
                    i++;
                }
                i--;
                output += number + ' ';
            }
            else if (c == '(')
            {
                stack.Push(c.ToString());
            }
            else if (c == ')')
            {
                while (!stack.IsEmpty() && stack.Peek() != "(")
                    output += stack.Pop() + ' ';

                if (stack.IsEmpty())
                    throw new Exception("Незбалансовані дужки");

                stack.Pop();
            }
            else if (IsOperator(c))
            {
                while (!stack.IsEmpty() && OpPreced(stack.Peek()[0]) >= OpPreced(c))
                    output += stack.Pop() + ' ';

                stack.Push(c.ToString());
            }
            else
            {
                throw new Exception($"Невідомий символ: {c}");
            }
        }

        while (!stack.IsEmpty())
        {
            string top = stack.Pop();
            if (top == "(") throw new Exception("Незбалансовані дужки");
            output += top + ' ';
        }

        return output.Trim();
    }

    static int EvaluatePostfixNotation(string rpn)
    {
        Stack stack = new Stack();
        string[] tokens = rpn.Split(' ');

        foreach (string token in tokens)
        {
            if (token == "") continue;

            if (int.TryParse(token, out int number))
            {
                stack.Push(token);
            }
            else if (token.Length == 1 && IsOperator(token[0]))
            {
                int b = int.Parse(stack.Pop());
                int a = int.Parse(stack.Pop());

                int result = token[0] switch
                {
                    '+' => a + b,
                    '-' => a - b,
                    '*' => a * b,
                    '/' => a / b,
                    '^' => (int)Math.Pow(a, b),
                    _ => throw new Exception($"Невідомий оператор: {token}")
                };

                stack.Push(result.ToString());
            }
        }

        return int.Parse(stack.Pop());
    }

    
    static void Main()
    {
        string input = "1+  3*(  2+ 4)";

        Console.WriteLine($"Вираз:     {input}");

        string rpn = ShuntingYard(input);
        Console.WriteLine($"RPN:       {rpn}");

        int result = EvaluatePostfixNotation(rpn);
        Console.WriteLine($"Результат: {result}");
        // → 1 + 3*(2+4) = 1 + 18 = 19
    }
}