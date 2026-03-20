namespace Assignment_1_Tyshchenko;

public class Tokenization
{
    string operatorsPlusBrackets= "+-*/^()";
    
    // чи є пробілом +
    // числом
    // дужками + помилка!!!!
    // + унарний мінус + унврний мінус перед дужками
    // функцією
    // змінною
    
    // щоб підряд не йшли операції або числа!!!!

    
    public string TokenizationFunction(string input)
    {
        string res = "";
        for (int i = 0; i < input.Length; i++)
        {
            // якщо пробіл
            if (input[i] == ' ')
            {
                continue;
            }
            // якщо кома
            else if (input[i] == ',')
            {
                res += ", ";
            }
            // для унарного мінуса
            else if (input[i] == '-' && (i == 0 || input[i-1] == '('))
            {
                res += '-'; // склеюємо з наступним числом
            }
            // якщо число
            else if (char.IsDigit(input[i]))
            {
                res += input[i];
                while (i + 1 < input.Length && (char.IsDigit(input[i + 1]) || input[i + 1] == '.'))
                {
                    i++;
                    res += input[i];
                }
                res += " ";
            }
            // якщо оператор або дужка
            else if (operatorsPlusBrackets.Contains(input[i]))
            {
                res += input[i] + " ";
            }
            else if (char.IsLetter(input[i]))
            {
                string word = "";
                while (i < input.Length && char.IsLetter(input[i]))
                {
                    word += input[i];
                    i++;
                }
                i--;
    
                if (word == "sin" || word == "cos" || word == "max" || word == "min")
                {
                    res += word + " ";
                }
                else
                {
                    throw new Exception($"Невідома функція: {word}");
                }
            }
            else
            {
                throw new Exception($"Невідомий символ: {input[i]}");
            }

        }

        // brackets check
        MyStack stack = new MyStack();
        foreach (char c in input)
        {
            if (c == '(') stack.Push("(");
            if (c == ')')
            {
                if (stack.IsEmpty()) throw new Exception("Невідкрита дужка");
                stack.Pop();
            }
        }
        if (!stack.IsEmpty()) throw new Exception("Незакрита дужка");
        
        return res.Trim();
    }
}