namespace Assignment_1_Tyshchenko;

public class Variables
{
    private string[] _names = new string[50];
    private double[] _values = new double[50];
    private int _count = 0;

    private bool IsStartOfFunction(string input, int i)
    {
        if (i + 3 > input.Length) return false;
        string three = input.Substring(i, 3);
        return three == "sin" || three == "cos" || three == "max" || three == "min";
    }
    
    public void Set(string name, double value)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_names[i] == name)
            {
                _values[i] = value;
                return;
            }
        }
        _names[_count] = name;
        _values[_count] = value;
        _count++;
    }

    public double Get(string name)
    {
        for (int i = 0; i < _count; i++)
            if (_names[i] == name)
                return _values[i];

        throw new Exception($"Error: unable to locate variable {name}");
    }

    public bool Exists(string name)
    {
        for (int i = 0; i < _count; i++)
            if (_names[i] == name)
                return true;
        return false;
    }

    public string ReplaceVariables(string input)
    {
        string result = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsLetter(input[i]))
            {
                if (IsStartOfFunction(input, i))
                {
                    while (i < input.Length && char.IsLetter(input[i]))
                    {
                        result += input[i];
                        i++;
                    }
                    i--;
                }
                else
                {
                    string name = input[i].ToString();
                    if (!Exists(name))
                        throw new Exception($"Error: unable to locate variable {name}");
                    result += Get(name).ToString();
                }
            }
            else
            {
                result += input[i];
            }
        }
        return result;
    }
}