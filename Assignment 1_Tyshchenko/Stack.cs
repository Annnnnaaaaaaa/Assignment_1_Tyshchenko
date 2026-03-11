namespace Assignment_1_Tyshchenko;

public class Stack
{
    private const int Capacity = 50;
    private string[] _array = new string[Capacity];
    private int _pointer;

    public int Count => _pointer;
    
    public bool IsEmpty() => _pointer == 0;

    public void Push(string value)
    {
        if (_pointer == _array.Length)
            throw new Exception("Stack overflowed");
        _array[_pointer] = value;
        _pointer++;
    }

    public string Pop()
    {
        if (_pointer == 0)
            throw new Exception("Stack is empty");
        _pointer--;
        return _array[_pointer];
    }

    public string Peek()
    {
        if (_pointer == 0)
            throw new Exception("Stack is empty");
        return _array[_pointer - 1];
    }
}